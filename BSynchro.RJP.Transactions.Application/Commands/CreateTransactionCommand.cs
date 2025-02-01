using BSynchro.RJP.Transactions.Application.Commands.Handlers;
using BSynchro.RJP.Transactions.Application.Services;
using BSynchro.RJP.Transactions.Domain.Entities;
using BSynchro.RJP.Transactions.Domain.Interfaces;
using MediatR;

namespace BSynchro.RJP.Transactions.Application.Commands
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
    {
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction(request.AccountId, request.Amount);
          //  await _transactionRepository.AddAsync(transaction);

            return new TransactionDto(transaction.Id, transaction.AccountId, transaction.Amount, transaction.Date);
        }
    }
}
