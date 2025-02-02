using Common.MessageQueueSender.Contracts;
using Common.MessageQueueSender.Models.DTOs;
using Common.MessageQueueSender.Models.Requests;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Common.MessageQueueSender.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class MessageQueueSender : IMessageQueueSender
    {
        #region Properties & Constructor
        private readonly RabbitMqConfigurationDTO _rabbitMqConfiguration;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingRequests = new();
        private readonly IConnection _connection;
        private readonly string _replyQueueName;
        private readonly IModel _channel;

        public MessageQueueSender(RabbitMqConfigurationDTO rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _connection = CreateConnection()!;
            _channel = _connection.CreateModel();

            // Declare response queue
            _replyQueueName = _channel.QueueDeclare().QueueName;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += OnResponseReceived;
            _channel.BasicConsume(queue: _replyQueueName, autoAck: true, consumer: consumer);
        }
        #endregion

        #region Public Methods
        public async Task<string> PublishMessage<T>(MessageConfigurationDTO messageConfiguration, T request) where T : BaseMessageRequest
        {
            if (messageConfiguration is null)
                return string.Empty;

            string response = string.Empty;
            List<Task<string>> tasks = new();

            foreach (var queue in messageConfiguration.QueueNames)
            {
                var queueName = !string.IsNullOrWhiteSpace(_rabbitMqConfiguration.QueueNameSuffix) ?
                                string.Concat(queue.ToString(), _rabbitMqConfiguration.QueueNameSuffix) : queue.ToString();

                var task = SendMessage(queueName, request, messageConfiguration);
                tasks.Add(task);
            }

            var results = await Task.WhenAll(tasks);
            response = string.Join(" | ", results);  // Combine responses if multiple queues
            return response;
        }
        #endregion

        #region Private Methods
        private IConnection? CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    UserName = _rabbitMqConfiguration.UserName,
                    Password = _rabbitMqConfiguration.Password,
                    AutomaticRecoveryEnabled = true,
                };

                var endpoints = new List<AmqpTcpEndpoint>();
                _rabbitMqConfiguration.Hostnames.ForEach(hostname =>
                {
                    endpoints.Add(new AmqpTcpEndpoint(hostname));
                });

                return factory.CreateConnection(endpoints);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<string> SendMessage<T>(string queueName, T request, MessageConfigurationDTO eventConfiguration)
        {
            string correlationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<string>();
            _pendingRequests[correlationId] = tcs;

            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: new Dictionary<string, object> { { "x-max-priority", 10 } });

            var properties = channel.CreateBasicProperties();
            properties.Persistent = eventConfiguration.IsPersistent;
            properties.Priority = Convert.ToByte(eventConfiguration.Priority);
            properties.CorrelationId = correlationId;
            properties.ReplyTo = _replyQueueName;

            string json = JsonConvert.SerializeObject(request);
            byte[] body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);

            // Wait for the response asynchronously
            var response = await tcs.Task;
            _pendingRequests.TryRemove(correlationId, out _);  // Clean up
            return response;
        }

        private void OnResponseReceived(object model, BasicDeliverEventArgs ea)
        {
            string correlationId = ea.BasicProperties.CorrelationId;

            if (_pendingRequests.TryRemove(correlationId, out var tcs))
            {
                string response = Encoding.UTF8.GetString(ea.Body.ToArray());
                tcs.SetResult(response);
            }
        }
        #endregion
    }
}
