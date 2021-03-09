using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Limits.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Limits
{
    public class LimitsService : AbstractService, ILimitsService
    {
        public LimitsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<Limit> GetCurrentExchangeLimitsAsync()
        {
            var fees = await SendServiceCall<Limit>(HttpMethod.Get, "/users/self/exchange-limits");

            return fees;
        }
    }
}
