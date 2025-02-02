using BSynchro.RJP.Transactions.Domain.Enums;

namespace BSynchro.RJP.Transactions.Application.Models.DTOs
{
    public class TransactionDTO
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime TransactedOn { get; set; }
    }
}
