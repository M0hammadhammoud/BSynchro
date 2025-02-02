using BSynchro.RJP.Transactions.Application.Models.DTOs;

namespace BSynchro.RJP.Transactions.Application.Contracts
{
    public interface ITransactionService
    {
        Task<(bool IsSuccess, string Message)> CreateTransactionAsync(TransactionDTO transaction);
        Task<List<TransactionDTO>> GetTransactions(List<Guid> accountIds);
    }
}
