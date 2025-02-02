using BSynchro.RJP.Transactions.Domain.Enums;

namespace BSynchro.RJP.Transactions.Domain.Documents
{
    public class Transaction : BaseDocument
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime TransactedOn { get; set; }
    }
}
