using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;

namespace GDAXSharp.Services.Currencies
{
    public class CurrenciesService : AbstractService
    {
        public CurrenciesService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<IEnumerable<Models.Currency>> GetAllCurrenciesAsync()
        {
            return await SendServiceCall<List<Models.Currency>>(HttpMethod.Get, "/currencies").ConfigureAwait(false);
        }
    }
}
