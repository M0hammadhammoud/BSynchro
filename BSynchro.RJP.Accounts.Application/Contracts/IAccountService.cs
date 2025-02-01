using BSynchro.RJP.Accounts.Application.Models.DTOs;

namespace BSynchro.RJP.Accounts.Application.Contracts
{
    public interface IAccountService
    {
        Task<string> OpenAccountAsync(OpenAccountDTO openAccount);
    }
}
