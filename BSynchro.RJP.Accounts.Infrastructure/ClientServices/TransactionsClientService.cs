using BSynchro.RJP.Accounts.Domain.Contracts;
using BSynchro.RJP.Accounts.Domain.Enums;
using BSynchro.RJP.Accounts.Domain.Models.DTOs.Configurations;
using BSynchro.RJP.Accounts.Domain.Models.DTOs.Transactions;
using BSynchro.RJP.Accounts.Domain.Models.Requests.Transactions;
using BSynchro.RJP.Accounts.Domain.Models.Responses.Transactions;
using Common.Utitlities.Contracts;
using Common.Utitlities.Helpers;
using System.Net;

namespace BSynchro.RJP.Accounts.Infrastructure.ClientServices
{
    public class TransactionsClientService : ITransactionsClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpClientHelper _transactionsClientHelper;
        private readonly TransactionsSettingsDTO _transactionsSettingsDTO;

        public TransactionsClientService(IHttpClientFactory httpClientFactory,
                                         TransactionsSettingsDTO transactionsSettingsDTO)
        {
            _httpClientFactory = httpClientFactory;
            _transactionsClientHelper = new HttpClientHelper(_httpClientFactory.CreateClient(nameof(HttpClientsEnum.Transactions)));
            _transactionsSettingsDTO = transactionsSettingsDTO;
        }

        public async Task<List<TransactionDTO>> GetTransactionsAsync(List<Guid> accountIds)
        {
            var getTransactionsRequest = new GetTransactionsRequest() { AccountIds = accountIds };
            var transactions = await _transactionsClientHelper.PostAsync<GetTransactionsRequest, GetTransactionsResponse>(_transactionsSettingsDTO.GetTransactions, getTransactionsRequest);

            if (transactions.HttpStatusCode == HttpStatusCode.OK)
            {
                return transactions.Transactions;
            }

            return [];
        }
    }
}
