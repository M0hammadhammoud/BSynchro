namespace BSynchro.RJP.Transactions.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid AccountId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(Guid accountId, decimal amount)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Amount = amount;
            Date = DateTime.UtcNow;
        }
    }
}
