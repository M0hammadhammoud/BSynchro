using BSynchro.RJP.Transactions.Domain.Documents;

namespace BSynchro.RJP.Transactions.Domain.Contracts
{
    public interface ITransactionRepository
    {
        Task<bool> AddTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetByAccountIdsAsync(List<Guid> accountIds);
    }
}
