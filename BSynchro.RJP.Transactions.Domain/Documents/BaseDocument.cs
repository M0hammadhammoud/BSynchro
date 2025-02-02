namespace BSynchro.RJP.Transactions.Domain.Documents
{
    public abstract class BaseDocument
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
