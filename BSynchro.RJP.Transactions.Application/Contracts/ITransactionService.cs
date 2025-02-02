using BSynchro.RJP.Transactions.Application.Models.DTOs;

namespace BSynchro.RJP.Transactions.Application.Contracts
{
    public interface ITransactionService
    {
        Task<string> CreateTransactionAsync(TransactionDTO transaction);
    }
}
