using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.StablecoinConversions.Models;
using CoinbasePro.Shared.Types;
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
            Currency from,
            Currency to,
            decimal amount)
        {
            var newConversion = JsonConfig.SerializeObject(new StablecoinConversion
            {
                From = from,
                To = to,
                Amount = amount
            });

            return await SendServiceCall<StablecoinConversionResponse>(HttpMethod.Post, "/conversions", newConversion);
        }
    }
}
