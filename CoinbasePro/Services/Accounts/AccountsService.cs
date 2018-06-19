using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Accounts.Models;

namespace CoinbasePro.Services.Accounts
{
    public class AccountsService : AbstractService
    {
        public AccountsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await SendServiceCall<List<Account>>(HttpMethod.Get, "/accounts");
        }

        public async Task<Account> GetAccountByIdAsync(string id)
        {
            return await SendServiceCall<Account>(HttpMethod.Get, $"/accounts/{id}");
        }

        public async Task<IList<IList<AccountHistory>>> GetAccountHistoryAsync(string id, int limit = 100, int numberOfPages = 0)
        {
            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<AccountHistory>(HttpMethod.Get, $"/accounts/{id}/ledger?limit={limit}", numberOfPages: numberOfPages);

            return httpResponseMessage;
        }

        public async Task<IList<IList<AccountHold>>> GetAccountHoldsAsync(string id, int limit = 100, int numberOfPages = 0)
        {
            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<AccountHold>(HttpMethod.Get, $"/accounts/{id}/holds?limit={limit}", numberOfPages: numberOfPages);

            return httpResponseMessage;
        }
    }
}
