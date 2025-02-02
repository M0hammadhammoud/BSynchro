using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;
using BSynchro.RJP.Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BSynchro.RJP.Accounts.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CoreDBContext _coreDBContext;

        public AccountRepository(CoreDBContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }

        public async Task<List<Account>> GetAccountsAsync(int customerId)
        {
            return await _coreDBContext.Set<Account>().Where(x => x.CustomerId == customerId).ToListAsync();
        }
    }
}
