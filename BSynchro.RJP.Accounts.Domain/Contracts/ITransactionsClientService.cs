using BSynchro.RJP.Accounts.Domain.Models.DTOs.Transactions;

namespace BSynchro.RJP.Accounts.Domain.Contracts
{
    public interface ITransactionsClientService
    {
        Task<List<TransactionDTO>> GetTransactionsAsync(List<Guid> accountIds);
    }
}
