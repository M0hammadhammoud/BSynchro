using AutoMapper;
using BSynchro.RJP.Accounts.Application.Constants;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;

namespace BSynchro.RJP.Accounts.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> OpenAccountAsync(OpenAccountDTO openAccount)
        {
            var account = _mapper.Map<Account>(openAccount);
            _unitOfWork.Repository<Account>().Add(account);
            await _unitOfWork.Save();

            if (openAccount.InitialCredit > 0)
            {
                //create transaction for this part
            }

            return BusinessMessages.AccountCreated;
        }
    }
}
