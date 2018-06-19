using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;

namespace CoinbasePro.Services.Currencies
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
