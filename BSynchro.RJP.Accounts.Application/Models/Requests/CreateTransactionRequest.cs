using BSynchro.RJP.Accounts.Domain.Enums;
using Common.MessageQueueSender.Models.Requests;

namespace BSynchro.RJP.Accounts.Application.Models.Requests
{
    public class CreateTransactionRequest : BaseMessageRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime TransactedOn { get; set; }
    }
}
