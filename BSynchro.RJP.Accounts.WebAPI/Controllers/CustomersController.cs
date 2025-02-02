using AutoMapper;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.WebAPI.Models.Requests.Customers;
using BSynchro.RJP.Accounts.WebAPI.Models.Responses.Customers;
using Microsoft.AspNetCore.Mvc;

namespace BSynchro.RJP.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService,
                                   IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetAllCustomersAsync();
            var response = _mapper.Map<GetAllCustomersResponse>(result);

            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpGet("GetUserInformation")]
        public async Task<IActionResult> GetUserInformation(GetCustomerInformationRequest request)
        {
            return Ok("result");
        }
    }
}
