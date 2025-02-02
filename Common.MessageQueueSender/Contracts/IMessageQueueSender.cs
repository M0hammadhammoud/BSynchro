using Common.MessageQueueSender.Models.DTOs;
using Common.MessageQueueSender.Models.Requests;

namespace Common.MessageQueueSender.Contracts
{
    public interface IMessageQueueSender
    {
        Task<string> PublishMessage<T>(MessageConfigurationDTO messageConfiguration, T request) where T : BaseMessageRequest;
    }
}
