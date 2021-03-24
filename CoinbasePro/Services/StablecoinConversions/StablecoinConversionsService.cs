using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.StablecoinConversions.Models;
using CoinbasePro.Shared.Utilities;

namespace CoinbasePro.Services.StablecoinConversions
{
    public class StablecoinConversionsService : AbstractService, IStablecoinConversionsService
    {
        public StablecoinConversionsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<StablecoinConversionResponse> CreateConversion(
            string fromCurrency,
            string toCurrency,
            decimal amount)
        {
            var newConversion = JsonConfig.SerializeObject(new StablecoinConversion
            {
                FromCurrency = fromCurrency,
                ToCurrency = toCurrency,
                Amount = amount
            });

            return await SendServiceCall<StablecoinConversionResponse>(HttpMethod.Post, "/conversions", newConversion);
        }
    }
}
