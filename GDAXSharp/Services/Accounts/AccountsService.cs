using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.Accounts.Models;
using GDAXSharp.Services.HttpRequest;

namespace GDAXSharp.Services.Accounts
{
    public class AccountsService : AbstractService
    {
        public AccountsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService, authenticator)
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
