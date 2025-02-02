using BSynchro.RJP.Accounts.Domain.Enums;

namespace BSynchro.RJP.Accounts.Domain.Models.DTOs.Transactions
{
    public class TransactionDTO
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get ; set; }
        public DateTime TransactedOn { get; set; }
    }
}
