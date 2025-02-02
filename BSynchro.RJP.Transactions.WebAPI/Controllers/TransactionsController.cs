using AutoMapper;
using BSynchro.RJP.Transactions.Application.Contracts;
using BSynchro.RJP.Transactions.Application.Models.DTOs;
using BSynchro.RJP.Transactions.WebAPI.Models.Requests.Transactions;
using BSynchro.RJP.Transactions.WebAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BSynchro.RJP.Transactions.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, 
                                      IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTransactionRequest request)
        {
            var transactionDTO = _mapper.Map<TransactionDTO>(request);
            var result = await _transactionService.CreateTransactionAsync(transactionDTO);
            var response = _mapper.Map<BaseResponse>(result);

            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
