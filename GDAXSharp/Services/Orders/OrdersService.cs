using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Authentication;
using GDAXSharp.HttpClient;
using GDAXSharp.Services.HttpRequest;
using GDAXSharp.Services.Orders.Models;
using GDAXSharp.Services.Orders.Models.Responses;
using GDAXSharp.Shared;
using Newtonsoft.Json;

namespace GDAXSharp.Services.Orders
{
    public class OrdersService : AbstractService
    {
        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public OrdersService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService,
            IAuthenticator authenticator)
                : base(httpClient, httpRequestMessageService)

        {
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<OrderResponse> PlaceMarketOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size)
        {
            var order = new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Market,
                Size = size
            };

            return await PlaceOrderAsync(order);
        }

        public async Task<OrderResponse> PlaceLimitOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal price,
            TimeInForce timeInForce = TimeInForce.Gtc,
            bool postOnly = true)
        {
            var order = new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Limit,
                Price = price,
                Size = size,
                TimeInForce = timeInForce,
                PostOnly = postOnly
            };

            return await PlaceOrderAsync(order);
        }

        public async Task<OrderResponse> PlaceLimitOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal price,
            GoodTillTime cancelAfter,
            bool postOnly = true)
        {
            var order = new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Limit,
                Price = price,
                Size = size,
                TimeInForce = TimeInForce.Gtt,
                CancelAfter = cancelAfter,
                PostOnly = postOnly
            };

            return await PlaceOrderAsync(order);
        }

        public async Task<OrderResponse> PlaceStopOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal stopPrice)
        {
            var order = new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Stop,
                Price = stopPrice,
                Size = size
            };

            return await PlaceOrderAsync(order);
        }

        private async Task<OrderResponse> PlaceOrderAsync(Order order)
        {
            var newOrder = JsonConvert.SerializeObject(order);

            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Post, authenticator, "/orders", newOrder);
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(contentBody);

            return orderResponse;
        }

        public async Task<CancelOrderResponse> CancelAllOrdersAsync()
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Delete, authenticator, "/orders");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<IEnumerable<Guid>>(contentBody);

            return new CancelOrderResponse
            {
                OrderIds = orderResponse
            };
        }

        public async Task<CancelOrderResponse> CancelOrderByIdAsync(string id)
        {
            var httpRequestResponse = await SendHttpRequestMessageAsync(HttpMethod.Delete, authenticator, $"/orders/{id}");

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

        public async Task<IList<IList<OrderResponse>>> GetAllOrdersAsync(
            OrderStatus orderStatus = OrderStatus.All, 
            int limit = 100, 
            int numberOfPages = 0)
        {
            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<OrderResponse>(HttpMethod.Get, authenticator, $"/orders?limit={limit}&status={orderStatus.ToString().ToLower()}", numberOfPages: numberOfPages);

            return httpResponseMessage;
        }

        public async Task<OrderResponse> GetOrderByIdAsync(string id)
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, $"/orders/{id}");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var orderResponse = JsonConvert.DeserializeObject<OrderResponse>(contentBody);

            return orderResponse;
        }
    }
}
