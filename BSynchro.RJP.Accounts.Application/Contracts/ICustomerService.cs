using BSynchro.RJP.Accounts.Application.Models.DTOs;

namespace BSynchro.RJP.Accounts.Application.Contracts
{
    public interface ICustomerService
    {
        Task<List<CustomerDTO>> GetAllCustomersAsync();
    }
}
