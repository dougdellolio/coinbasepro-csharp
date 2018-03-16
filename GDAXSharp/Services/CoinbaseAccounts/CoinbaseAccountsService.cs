using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Infrastructure.Authentication;
using GDAXSharp.Infrastructure.HttpClient;
using GDAXSharp.Infrastructure.HttpRequest;
using GDAXSharp.Services.CoinbaseAccounts.Models;

namespace GDAXSharp.Services.CoinbaseAccounts
{
    public class CoinbaseAccountsService : AbstractService
    {
        public CoinbaseAccountsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService, authenticator)
        {
        }

        public async Task<IEnumerable<CoinbaseAccount>> GetAllAccountsAsync()
        {
            return await SendServiceCall<List<CoinbaseAccount>>(HttpMethod.Get, "/coinbase-accounts");
        }
    }
}
