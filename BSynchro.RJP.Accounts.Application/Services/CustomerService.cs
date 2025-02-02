using AutoMapper;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;
using BSynchro.RJP.Accounts.Domain.Models.DTOs.Transactions;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;

namespace BSynchro.RJP.Accounts.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDataProtector _dataProtector;
        private readonly ITransactionsClientService _transactionsClientService;
        private readonly IAccountRepository _accountRepository;

        public CustomerService(IUnitOfWork unitOfWork,
                               IMapper mapper,
                               IDataProtector dataProtector,
                               ITransactionsClientService transactionsClientService,
                               IAccountRepository accountRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dataProtector = dataProtector;
            _transactionsClientService = transactionsClientService;
            _accountRepository = accountRepository;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> GetCustomerInformationAsync(string customerId)
        {
            var unprotectedCustomerId = int.Parse(_dataProtector.Unprotect(customerId));
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(unprotectedCustomerId);
            var result = _mapper.Map<CustomerDTO>(customer);
            var accounts = await _accountRepository.GetAccountsAsync(unprotectedCustomerId);

            if (accounts != null && accounts.Count > 0)
            {
                result.Accounts = _mapper.Map<List<AccountDTO>>(accounts);

                var accountIds = accounts.Select(x => x.AccountId).ToList();
                var transactions = await _transactionsClientService.GetTransactionsAsync(accountIds);

                if (transactions.Count > 0)
                {
                    result.Accounts.ForEach(account =>
                    {
                        account.Transactions = transactions.Where(x => x.AccountId == account.AccountId).ToList();
                    });
                }
            }

            return result;
        }
    }
}
