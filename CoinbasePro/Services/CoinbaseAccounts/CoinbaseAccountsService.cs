using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.CoinbaseAccounts.Models;

namespace CoinbasePro.Services.CoinbaseAccounts
{
    public class CoinbaseAccountsService : AbstractService
    {
        public CoinbaseAccountsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<IEnumerable<CoinbaseAccount>> GetAllAccountsAsync()
        {
            return await SendServiceCall<List<CoinbaseAccount>>(HttpMethod.Get, "/coinbase-accounts");
        }
    }
}
