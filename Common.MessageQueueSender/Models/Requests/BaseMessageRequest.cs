using Common.MessageQueueSender.Models.Enums;

namespace Common.MessageQueueSender.Models.Requests
{
    public class BaseMessageRequest
    {
        public MessageTypeEnum MessageType { get; set; }
    }
}
