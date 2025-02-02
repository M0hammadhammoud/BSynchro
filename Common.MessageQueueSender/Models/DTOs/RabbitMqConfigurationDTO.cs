namespace Common.MessageQueueSender.Models.DTOs
{
    public class RabbitMqConfigurationDTO
    {
        public List<string> Hostnames { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueNameSuffix { get; set; }
        public List<string> QueueNames { get; set; }
    }
}
