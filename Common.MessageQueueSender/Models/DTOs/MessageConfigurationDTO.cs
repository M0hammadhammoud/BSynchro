using Common.MessageQueueSender.Models.Enums;

namespace Common.MessageQueueSender.Models.DTOs
{
    public class MessageConfigurationDTO
    {
        public List<QueueEnum> QueueNames { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public int Priority { get; set; } = 1;
        public bool IsPersistent { get; set; }
    }
}
