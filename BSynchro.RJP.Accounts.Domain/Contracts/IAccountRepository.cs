using BSynchro.RJP.Accounts.Domain.Entities;

namespace BSynchro.RJP.Accounts.Domain.Contracts
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAccountsAsync(int customerId);
    }
}
