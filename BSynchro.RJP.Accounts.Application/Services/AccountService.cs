using AutoMapper;
using BSynchro.RJP.Accounts.Application.Constants;
using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Models.DTOs;
using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Entities;
using Common.MessageQueueSender.Contracts;
using Common.MessageQueueSender.Models.DTOs;
using Common.MessageQueueSender.Models.Enums;
using Common.MessageQueueSender.Models.Requests;

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
            _unitOfWork.Repository<Account>().Add(account);
            await _unitOfWork.Save();

            if (openAccount.InitialCredit > 0)
            {
                //create transaction for this part
                var messageConfiguration = _messageConfigurations.First(x => x.MessageType == MessageTypeEnum.Transaction);
                var result = await _messageQueueSender.PublishMessage(messageConfiguration, new BaseMessageRequest() {  MessageType = MessageTypeEnum.Transaction});
            }

            return BusinessMessages.AccountCreated;
        }

        public async Task<List<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }
    }
}
