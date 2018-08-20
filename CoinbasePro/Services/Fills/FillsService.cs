using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Fills.Models.Responses;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities.Extensions;

namespace CoinbasePro.Services.Fills
{
    public class FillsService : AbstractService
    {
        public FillsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        [Obsolete("Requests without either order_id or product_id will be rejected after 8/23/18.")]
        public async Task<IList<IList<FillResponse>>> GetAllFillsAsync(
            int limit = 100,
            int numberOfPages = 0)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, $"/fills?limit={limit}", numberOfPages: numberOfPages);

            return fills;
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
            ProductType productId,
            int limit = 100,
            int numberOfPages = 0)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, $"/fills?limit={limit}&product_id={productId.GetEnumMemberValue()}", numberOfPages: numberOfPages);

            return fills;
        }
    }
}
