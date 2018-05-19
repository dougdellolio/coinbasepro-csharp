using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.Orders.Models;
using GDAXSharp.Services.Orders.Models.Responses;
using GDAXSharp.Services.Orders.Types;
using GDAXSharp.Shared.Types;
using GDAXSharp.Shared.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXSharp.Shared.Utilities;

namespace GDAXSharp.Services.Orders
{
    public class OrdersService : AbstractService
    {
        public OrdersService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<OrderResponse> PlaceMarketOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal amount,
            MarketOrderAmountType amountType = MarketOrderAmountType.Size,
            Guid? clientOid = null)
        {
            return await PlaceOrderAsync(new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Market,
                ClientOid = clientOid,
                Funds = amountType == MarketOrderAmountType.Funds
                    ? amount
                    : (decimal?)null,
                Size = amountType == MarketOrderAmountType.Size
                    ? amount
                    : (decimal?)null
            });
        }

        public async Task<OrderResponse> PlaceLimitOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal price,
            TimeInForce timeInForce = TimeInForce.Gtc,
            bool postOnly = true,
            Guid? clientOid = null)
        {
            var order = new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Limit,
                Price = price,
                Size = size,
                TimeInForce = timeInForce,
                PostOnly = postOnly,
                ClientOid = clientOid
            };

            return await PlaceOrderAsync(order);
        }

        public async Task<OrderResponse> PlaceLimitOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal price,
            GoodTillTime cancelAfter,
            bool postOnly = true,
            Guid? clientOid = null)
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
                PostOnly = postOnly,
                ClientOid = clientOid
            };

            return await PlaceOrderAsync(order);
        }

        public async Task<OrderResponse> PlaceStopLimitOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal stopPrice,
            decimal limitPrice,
            bool postOnly = false,
            Guid? clientOid = null)
        {
            var order = new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Limit,
                Price = limitPrice,
                Size = size,
                Stop = side == OrderSide.Buy
                    ? StopType.Entry
                    : StopType.Loss,
                StopPrice = stopPrice,
                ClientOid = clientOid,
                PostOnly = postOnly
            };

            return await PlaceOrderAsync(order);
        }

        public async Task<OrderResponse> PlaceStopOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal stopPrice,
            Guid? clientOid = null)
        {
            var order = new Order
            {
                Side = side,
                ProductId = productId,
                OrderType = OrderType.Stop,
                Price = stopPrice,
                Size = size,
                ClientOid = clientOid
            };

            return await PlaceOrderAsync(order);
        }

        private async Task<OrderResponse> PlaceOrderAsync(Order order)
        {
            return await SendServiceCall<OrderResponse>(HttpMethod.Post, "/orders", JsonConfig.SerializeObject(order)).ConfigureAwait(false);
        }

        public async Task<CancelOrderResponse> CancelAllOrdersAsync()
        {
            return new CancelOrderResponse
            {
                OrderIds = await SendServiceCall<IEnumerable<Guid>>(HttpMethod.Delete, "/orders").ConfigureAwait(false)
            };
        }

        public async Task<CancelOrderResponse> CancelOrderByIdAsync(string id)
        {
            return new CancelOrderResponse
            {
                OrderIds = await SendServiceCall<IEnumerable<Guid>>(HttpMethod.Delete, $"/orders/{id}")
            };
        }

        public async Task<IList<IList<OrderResponse>>> GetAllOrdersAsync(
            OrderStatus orderStatus = OrderStatus.All,
            int limit = 100,
            int numberOfPages = 0)
        {
            var httpResponseMessage = await SendHttpRequestMessagePagedAsync<OrderResponse>(HttpMethod.Get, $"/orders?limit={limit}&status={orderStatus.GetEnumMemberValue()}", numberOfPages: numberOfPages);

            return httpResponseMessage;
        }

        public async Task<OrderResponse> GetOrderByIdAsync(string id)
        {
            return await SendServiceCall<OrderResponse>(HttpMethod.Get, $"/orders/{id}").ConfigureAwait(false);
        }
    }
}
