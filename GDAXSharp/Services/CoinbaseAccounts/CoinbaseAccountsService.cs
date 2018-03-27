using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.CoinbaseAccounts.Models;

namespace GDAXSharp.Services.CoinbaseAccounts
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
