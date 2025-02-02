using AutoMapper;
using BSynchro.RJP.Transactions.Application.Constants;
using BSynchro.RJP.Transactions.Application.Contracts;
using BSynchro.RJP.Transactions.Application.Models.DTOs;
using BSynchro.RJP.Transactions.Domain.Contracts;
using BSynchro.RJP.Transactions.Domain.Documents;

namespace BSynchro.RJP.Transactions.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository,
                                  IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, string Message)> CreateTransactionAsync(TransactionDTO transaction)
        {
            var transactionDocument = _mapper.Map<Transaction>(transaction);
            transactionDocument.Id = Guid.NewGuid();
            var isSuccess = await _transactionRepository.AddTransactionAsync(transactionDocument);

            if (isSuccess)
            {
                return (true, BusinessMessages.TransactionSuccess);
            }

            return (false, BusinessMessages.TransactionFail);
        }

        public async Task GetTransactions()
        {

        }
    }
}
