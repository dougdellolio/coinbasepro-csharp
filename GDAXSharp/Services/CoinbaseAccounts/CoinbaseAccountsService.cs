using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.CoinbaseAccounts.Models;
using GDAXSharp.Services.HttpRequest;

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
            return await MakeServiceCall<List<CoinbaseAccount>>(HttpMethod.Get, "/coinbase-accounts");
        }
    }
}
