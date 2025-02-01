using BSynchro.RJP.Transactions.Application.Services;
using MediatR;

namespace BSynchro.RJP.Transactions.Application.Commands.Handlers
{
    public class CreateTransactionCommand : IRequest<TransactionDto>
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
