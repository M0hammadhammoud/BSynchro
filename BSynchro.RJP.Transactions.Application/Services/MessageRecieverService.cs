using BSynchro.RJP.Transactions.Application.Contracts;
using BSynchro.RJP.Transactions.Application.Models.DTOs;
using BSynchro.RJP.Transactions.Application.Models.DTOs.RabbitMQ;
using Common.MessageQueueSender.Models.DTOs;
using Common.MessageQueueSender.Models.Enums;
using Common.MessageQueueSender.Models.Requests;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BSynchro.RJP.Transactions.Application.Services
{
    public class MessageReceiverService : BackgroundService
    {
        private readonly List<HostDTO> _hosts;
        private readonly ILogger<MessageReceiverService> _logger;
        private readonly RabbitMqConfigurationDTO _rabbitMqConfiguration;
        private readonly ITransactionService _transactionService;

        public MessageReceiverService(ILogger<MessageReceiverService> logger,
                                     RabbitMqConfigurationDTO rabbitMqConfiguration,
                                     ITransactionService transactionService)
        {
            _logger = logger;
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _transactionService = transactionService;

            _hosts = rabbitMqConfiguration.Hostnames.Select(hostname => new HostDTO
            {
                Name = hostname
            }).ToList();

            InitializeRabbitMqListener();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            try
            {
                foreach (var host in _hosts)
                {
                    if (!string.IsNullOrWhiteSpace(host.Name) && host.Channel != null)
                    {
                        var consumer = new EventingBasicConsumer(host.Channel);

                        consumer.Received += async (ch, ea) =>
                        {
                            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                            var eventObject = JsonConvert.DeserializeObject<BaseMessageRequest>(content);

                            (bool IsSuccess, string Message) result = (false, string.Empty);

                            switch (eventObject.MessageType)
                            {
                                case MessageTypeEnum.Transaction:
                                    var request = JsonConvert.DeserializeObject<TransactionDTO>(content);
                                    result = await _transactionService.CreateTransactionAsync(request);
                                    break;
                                default:
                                    break;
                            }

                            if (result.IsSuccess)
                            {
                                host.Channel.BasicAck(ea.DeliveryTag, false);

                                // Send the response back to the reply queue
                                var response = JsonConvert.SerializeObject(new { success = true, message = "Transaction created successfully" });
                                SendResponse(ea.BasicProperties.ReplyTo, ea.BasicProperties.CorrelationId, response);
                            }
                            else
                            {
                                //todo 
                                //check requeueing concept
                                host.Channel.BasicNack(ea.DeliveryTag, false, false);

                                // Send failure response back
                                var response = JsonConvert.SerializeObject(new { success = false, message = "Transaction creation failed" });
                                SendResponse(ea.BasicProperties.ReplyTo, ea.BasicProperties.CorrelationId, response);
                            }
                        };

                        consumer.Shutdown += OnConsumerShutdown;
                        consumer.Registered += OnConsumerRegistered;
                        consumer.Unregistered += OnConsumerUnregistered;
                        consumer.ConsumerCancelled += OnConsumerCancelled;

                        foreach (var queueName in _rabbitMqConfiguration.QueueNames)
                        {
                            host.Channel.BasicConsume($"{queueName}{_rabbitMqConfiguration.QueueNameSuffix}", false, consumer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(MessageReceiverService)}-{nameof(ExecuteAsync)}");
            }
        }

        private void SendResponse(string replyTo, string correlationId, string responseMessage)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(replyTo))
                    return;

                var properties = _hosts.First().Channel.CreateBasicProperties();
                properties.CorrelationId = correlationId;

                var responseBody = Encoding.UTF8.GetBytes(responseMessage);

                _hosts.First().Channel.BasicPublish(
                    exchange: "",
                    routingKey: replyTo,
                    basicProperties: properties,
                    body: responseBody);

                _logger.LogInformation($"Sent response to {replyTo} with CorrelationId: {correlationId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending response message.");
            }
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            foreach (var host in _hosts)
            {
                host.Channel.Close();
                host.Connection.Close();
            }

            base.Dispose();
        }

        private void InitializeRabbitMqListener()
        {
            try
            {
                Dictionary<string, object> args = new()
                {
                    { "x-max-priority", 10 }
                };

                foreach (var host in _hosts)
                {
                    if (!string.IsNullOrWhiteSpace(host.Name))
                    {
                        var factory = new ConnectionFactory
                        {
                            HostName = host.Name,
                            UserName = _rabbitMqConfiguration.UserName,
                            Password = _rabbitMqConfiguration.Password,
                            AutomaticRecoveryEnabled = true
                        };

                        var connection = factory.CreateConnection();
                        connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                        host.Connection = connection;

                        var channel = connection.CreateModel();

                        foreach (var queueName in _rabbitMqConfiguration.QueueNames)
                        {
                            channel.QueueDeclare(queue: $"{queueName}{_rabbitMqConfiguration.QueueNameSuffix}", durable: true, exclusive: false, autoDelete: false, arguments: args);
                        }

                        host.Channel = channel;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(InitializeRabbitMqListener)}");
            }
        }
    }
}
