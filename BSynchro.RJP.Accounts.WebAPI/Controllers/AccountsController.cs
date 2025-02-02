using AutoMapper;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.WebAPI.Models.Requests.Accounts;
using BSynchro.RJP.Accounts.WebAPI.Models.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BSynchro.RJP.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IValidator<OpenAccountRequest> _accountValidator;

        public AccountsController(IAccountService accountService,
                                  IMapper mapper,
                                  IValidator<OpenAccountRequest> accountValidator)
        {
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
    }
}
