using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Fills.Models.Responses;

namespace CoinbasePro.Services.Fills
{
    public class FillsService : AbstractService, IFillsService
    {
        public FillsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByOrderIdAsync(
            string orderId,
            int limit = 100,
            int numberOfPages = 0)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, $"/fills?limit={limit}&order_id={orderId}", numberOfPages: numberOfPages);

            return fills;
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByProductIdAsync(
            string productId,
            int limit = 100,
            int numberOfPages = 0)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, $"/fills?limit={limit}&product_id={productId}", numberOfPages: numberOfPages);

            return fills;
        }
    }
}
