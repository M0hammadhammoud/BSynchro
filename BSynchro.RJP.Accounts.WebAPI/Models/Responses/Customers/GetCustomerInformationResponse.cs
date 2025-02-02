using BSynchro.RJP.Accounts.Application.Models.DTOs;

namespace BSynchro.RJP.Accounts.WebAPI.Models.Responses.Customers
{
    public class GetCustomerInformationResponse : BaseResponse
    {
        public CustomerDTO Customer { get; set; }
    }
}
