using RabbitMQ.Client;

namespace BSynchro.RJP.Transactions.Application.Models.DTOs.RabbitMQ
{
    public class HostDTO
    {
        public string Name { get; set; }
        public IModel Channel { get; set; } = null;
        public IConnection Connection { get; set; } = null;
    }
}
