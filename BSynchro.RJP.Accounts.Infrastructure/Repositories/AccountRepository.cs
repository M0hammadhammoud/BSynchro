using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Infrastructure.Data;

namespace BSynchro.RJP.Accounts.Infrastructure.Repositories
{
    public class AccountRepository :  IAccountRepository
    {
        private readonly CoreDBContext _coreDBContext;

        public AccountRepository(CoreDBContext coreDBContext)
        {
            _coreDBContext = coreDBContext;
        }
    }
}
