namespace BSynchro.RJP.Accounts.Domain.Models.DTOs.Transactions
{
    public class AccountDTO
    {
        public Guid AccountId { get; set; }
        public string CustomerId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionDTO> Transactions { get; set; } = [];
    }
}
