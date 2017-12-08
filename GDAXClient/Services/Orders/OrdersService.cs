using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Utilities.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Services.Orders
{
    public class OrdersService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public OrdersService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService, authenticator)

        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<OrderResponse> PlaceMarketOrderAsync(OrderSide side, ProductType productId, decimal size)
        {
            var newOrder = JsonConvert.SerializeObject(new Order
            {
                side = side.ToString().ToLower(),
                product_id = productId.ToDasherizedUpper(),
                type = OrderType.Market.ToString().ToLower(),
                size = size
            });

            var httpResponseMessage = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/orders", newOrder);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(contentBody);

            return orderResponse;
        }

        public async Task<OrderResponse> PlaceLimitOrderAsync(OrderSide side, ProductType productId, decimal size, decimal price)
        {
            var newOrder = JsonConvert.SerializeObject(new Order
            {
                side = side.ToString().ToLower(),
                product_id = productId.ToDasherizedUpper(),
                type = OrderType.Limit.ToString().ToLower(),
                price = price,
                size = size
            });

            var httpResponseMessage = await SendHttpRequestMessage(HttpMethod.Post, authenticator, "/orders", newOrder);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(contentBody);

            return orderResponse;
        }

        public async Task<CancelOrderResponse> CancelAllOrdersAsync()
        {
            var httpResponseMessage = await SendHttpRequestMessage(HttpMethod.Delete, authenticator, "/orders");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<IEnumerable<Guid>>(contentBody);

            return new CancelOrderResponse
            {
                OrderIds = orderResponse
            };
        }

        public async Task<CancelOrderResponse> CancelOrderByIdAsync(string id)
        {
            var httpRequestResponse = await SendHttpRequestMessage(HttpMethod.Delete, authenticator, $"/orders/{id}");

            if (httpRequestResponse == null)
            {
                return new CancelOrderResponse
                {
                    OrderIds = Enumerable.Empty<Guid>()
                };
            }

            return new CancelOrderResponse
            {
                OrderIds = new List<Guid> { new Guid(id) }
            };
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync()
        {
            var httpResponseMessage = await SendHttpRequestMessage(HttpMethod.Get, authenticator, "/orders");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<IEnumerable<OrderResponse>>(contentBody);

            return orderResponse;
        }

        public async Task<OrderResponse> GetOrderByIdAsync(string id)
        {
            var httpResponseMessage = await SendHttpRequestMessage(HttpMethod.Get, authenticator, $"/orders/{id}");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(contentBody);

            return orderResponse;
        }
    }
}
