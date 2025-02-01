using AutoMapper;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.WebAPI.Models.Requests;
using BSynchro.RJP.Accounts.WebAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BSynchro.RJP.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IValidator<OpenAccountRequest> _accountValidator;

        public AccountController(ILogger<AccountController> logger,
                                 IAccountService accountService,
                                 IMapper mapper,
                                 IValidator<OpenAccountRequest> accountValidator)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
            _accountValidator = accountValidator;
        }

        [HttpPost("open")]
        public async Task<IActionResult> OpenAccount(OpenAccountRequest request)
        {
            // Validate the request
            var validationResult = await _accountValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var accountDto = _mapper.Map<OpenAccountDTO>(request);
            var account = await _accountService.OpenAccountAsync(accountDto);

            var result = await _accountService.OpenAccount();
            var response = _mapper.Map<SearchResponse>(result);

            return StatusCode((int)response.HttpStatusCod, response);
        }

        //[HttpGet("{customerId}")]
        //public async Task<IActionResult> GetCustomerAccounts(Guid customerId)
        //{
        //    var query = new GetCustomerAccountsQuery(customerId);
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}
    }
}
