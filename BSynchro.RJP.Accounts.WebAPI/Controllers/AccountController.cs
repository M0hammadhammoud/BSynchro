using AutoMapper;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.WebAPI.Models.Requests.Account;
using BSynchro.RJP.Accounts.WebAPI.Models.Responses;
using BSynchro.RJP.Accounts.WebAPI.Models.Responses.Account;
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

        [HttpPost("Open")]
        public async Task<IActionResult> OpenAccount(OpenAccountRequest request)
        {
            // Validate the request
            var validationResult = await _accountValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var accountDto = _mapper.Map<OpenAccountDTO>(request);
            var result = await _accountService.OpenAccountAsync(accountDto);
            var response = _mapper.Map<BaseResponse>(result);

            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountService.GetAllCustomersAsync();
            var response = _mapper.Map<GetAllCustomersResponse>(result);

            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpPost("GetUserInformation")]
        public async Task<IActionResult> GetUserInformation(GetUserInformationRequest request)
        {
            return Ok("result");
        }
    }
}
