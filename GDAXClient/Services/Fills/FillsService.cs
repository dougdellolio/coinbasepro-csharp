using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.Fills.Models.Responses;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Utilities;
using GDAXClient.Utilities.Extensions;

namespace GDAXClient.Services.Fills
{
    public class FillsService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        private readonly IQueryBuilder queryBuilder;

        public FillsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IQueryBuilder queryBuilder,
            IAuthenticator authenticator) 
            : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
            this.queryBuilder = queryBuilder;
        }

        public async Task<IList<IList<FillResponse>>> GetAllFillsAsync(int limit = 100)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("limit", limit.ToString()));

            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, authenticator, $"/fills" + queryString);

            return fills;
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByOrderIdAsync(string orderId, int limit = 100)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("order_id", orderId));

            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, authenticator, $"/fills" + queryString);

            return fills;
        }

        public async Task<IList<IList<FillResponse>>> GetFillsByProductIdAsync(ProductType productId, int limit = 100)
        {
            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("limit", limit.ToString()),
                new KeyValuePair<string, string>("product_id", productId.ToDasherizedUpper()));

            var fills = await SendHttpRequestMessagePagedAsync<FillResponse>(HttpMethod.Get, authenticator, $"/fills" + queryString);

            return fills;
        }
    }
}
