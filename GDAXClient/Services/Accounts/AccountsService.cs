using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Services.Accounts
{
    public class AccountsService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public AccountsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            var contentBody = await SendHttpRequestMessage(HttpMethod.Get, authenticator, "/accounts");
            var accountList = JsonConvert.DeserializeObject<List<Account>>(contentBody);

            return accountList;
        }

        public async Task<Account> GetAccountByIdAsync(string id)
        {
            var contentBody = await SendHttpRequestMessage(HttpMethod.Get, authenticator, $"/accounts/{id}");
            var account = JsonConvert.DeserializeObject<Account>(contentBody);

            return account;
        }

        public async Task<IEnumerable<CoinbaseAccount>> GetCoinbaseAccountsAsync()
        {
            var contentBody = await SendHttpRequestMessage(HttpMethod.Get, authenticator, "/coinbase-accounts");
            var accounts = JsonConvert.DeserializeObject<List<CoinbaseAccount>>(contentBody);

            return accounts;
        }
    }
}
