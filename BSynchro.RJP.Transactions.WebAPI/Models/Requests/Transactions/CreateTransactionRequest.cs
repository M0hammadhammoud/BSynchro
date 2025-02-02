using BSynchro.RJP.Transactions.Domain.Enums;

namespace BSynchro.RJP.Transactions.WebAPI.Models.Requests.Transactions
{
    public class CreateTransactionRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime TransactedOn { get; set; }
    }
}
