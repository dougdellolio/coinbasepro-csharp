using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Fills.Models.Responses;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Shared;
using GDAXClient.Utilities.Extensions;

namespace GDAXClient.Services.Fills
{
    public class FillsService : AbstractService
    {
        private readonly IAuthenticator authenticator;

        public FillsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService)
        {
            this.authenticator = authenticator;
        }

        public async Task<IList<IList<FillResponse>>> GetAllFillsAsync(
            int limit = 100, 
            int numberOfPages = 0)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, authenticator, $"/fills?limit={limit}", numberOfPages: numberOfPages);

            return fills;
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByOrderIdAsync(
            string orderId, 
            int limit = 100, 
            int numberOfPages = 0)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, authenticator, $"/fills?limit={limit}&order_id={orderId}", numberOfPages: numberOfPages);

            return fills;
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByProductIdAsync(
            ProductType productId, 
            int limit = 100, 
            int numberOfPages = 0)
        {
            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, authenticator, $"/fills?limit={limit}&product_id={productId.ToDasherizedUpper()}", numberOfPages: numberOfPages);

            return fills;
        }
    }
}
