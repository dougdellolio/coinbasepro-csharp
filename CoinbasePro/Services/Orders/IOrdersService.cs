using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Orders.Models.Responses;
using CoinbasePro.Services.Orders.Types;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.Orders
{
    public interface IOrdersService
    {
        Task<OrderResponse> PlaceMarketOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal amount,
            MarketOrderAmountType amountType = MarketOrderAmountType.Size,
            Guid? clientOid = null);

        Task<OrderResponse> PlaceLimitOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal price,
            TimeInForce timeInForce = TimeInForce.Gtc,
            bool postOnly = true,
            Guid? clientOid = null);

        Task<OrderResponse> PlaceLimitOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal price,
            GoodTillTime cancelAfter,
            bool postOnly = true,
            Guid? clientOid = null);

        Task<OrderResponse> PlaceStopOrderAsync(
            OrderSide side,
            ProductType productId,
            decimal size,
            decimal limitPrice,
            decimal stopPrice,
            Guid? clientOid = null);

        Task<IList<IList<OrderResponse>>> GetAllOrdersAsync(
            OrderStatus orderStatus = OrderStatus.All,
            int limit = 100,
            int numberOfPages = 0);

        Task<IList<IList<OrderResponse>>> GetAllOrdersAsync(
            OrderStatus[] orderStatus,
            int limit = 100,
            int numberOfPages = 0);

        Task<OrderResponse> GetOrderByIdAsync(string id);

        Task<CancelOrderResponse> CancelAllOrdersAsync();

        Task<CancelOrderResponse> CancelOrderByIdAsync(string id);
    }
}
