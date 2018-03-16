using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Utilities.Extensions;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.Fills.Models.Responses;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Shared;

namespace GDAXSharp.Services.Fills
{
    public class FillsService : AbstractService
    {
        public FillsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService, authenticator)
        {
        }

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
