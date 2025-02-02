using AutoMapper;
using BSynchro.RJP.Accounts.Application.Constants;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.Application.Models.Requests;
using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;
using BSynchro.RJP.Accounts.Domain.Enums;
using Common.MessageQueueSender.Contracts;
using Common.MessageQueueSender.Models.DTOs;
using Common.MessageQueueSender.Models.Enums;

namespace BSynchro.RJP.Accounts.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageQueueSender _messageQueueSender;
        private readonly List<MessageConfigurationDTO> _messageConfigurations;

        public AccountService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IMessageQueueSender messageQueueSender,
                              List<MessageConfigurationDTO> messageConfigurations)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messageQueueSender = messageQueueSender;
            _messageConfigurations = messageConfigurations;
        }

        public async Task<string> OpenAccountAsync(OpenAccountDTO openAccount)
        {
            var account = _mapper.Map<Account>(openAccount);
            account.AccountId = Guid.NewGuid();
            _unitOfWork.Repository<Account>().Add(account);
            await _unitOfWork.Save();

            if (openAccount.InitialCredit > 0)
            {
                //create transaction for this part
                var messageConfiguration = _messageConfigurations.First(x => x.MessageType == MessageTypeEnum.Transaction);

                var createTransactionRequest = new CreateTransactionRequest()
                {
                    AccountId = account.AccountId,
                    Amount = openAccount.InitialCredit,
                    MessageType = messageConfiguration.MessageType,
                    TransactedOn = DateTime.UtcNow,
                    TransactionType = TransactionTypeEnum.Credit
                };

                var result = await _messageQueueSender.PublishMessage(messageConfiguration, createTransactionRequest);
                account.Balance = openAccount.InitialCredit;
                await _unitOfWork.Save();
            }

            return BusinessMessages.AccountCreated;
        } 
    }
}
