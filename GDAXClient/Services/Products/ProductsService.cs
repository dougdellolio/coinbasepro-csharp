using System;
using GDAXClient.HttpClient;
using GDAXClient.Services;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Services.Products;
using GDAXClient.Services.Products.Models;
using GDAXClient.Services.Products.Models.Responses;
using GDAXClient.Utilities.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Utilities;

namespace GDAXClient.Products
{
    public class ProductsService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        private readonly IQueryBuilder queryBuilder;

        public ProductsService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator,
            IQueryBuilder queryBuilder)
                : base(httpClient, httpRequestMessageService, authenticator)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
            this.queryBuilder = queryBuilder;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, "/products");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var productsResponse = JsonConvert.DeserializeObject<IEnumerable<Product>>(contentBody);

            return productsResponse;
        }

        public async Task<ProductsOrderBookResponse> GetProductOrderBookAsync(ProductType productPair)
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, $"/products/{productPair.ToDasherizedUpper()}/book");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var productOrderBookResponse = JsonConvert.DeserializeObject<ProductsOrderBookResponse>(contentBody);

            return productOrderBookResponse;
        }

        public async Task<ProductTicker> GetProductTickerAsync(ProductType productPair)
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, $"/products/{productPair.ToDasherizedUpper()}/ticker");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var productTickerResponse = JsonConvert.DeserializeObject<ProductTicker>(contentBody);

            return productTickerResponse;
        }

        public async Task<ProductStats> GetProductStatsAsync(ProductType productPair)
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, $"/products/{productPair.ToDasherizedUpper()}/stats");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var productStatsResponse = JsonConvert.DeserializeObject<ProductStats>(contentBody);

            return productStatsResponse;
        }
        
        public async Task<IEnumerable<object[]>> GetHistoricRatesAsync(ProductType productPair, DateTime start, DateTime end, int granularity)
        {
            var isoStart = start.ToString("s");
            var isoEnd = end.ToString("s");

            var queryString = queryBuilder.BuildQuery(
                new KeyValuePair<string, string>("start", isoStart),
                new KeyValuePair<string, string>("end", isoEnd),
                new KeyValuePair<string, string>("granularity", granularity.ToString()));

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, $"/products/{productPair.ToDasherizedUpper()}/candles" + queryString);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var productHistoryResponse = JsonConvert.DeserializeObject<IEnumerable<object[]>>(contentBody);

            return productHistoryResponse;
        }
    }
}
