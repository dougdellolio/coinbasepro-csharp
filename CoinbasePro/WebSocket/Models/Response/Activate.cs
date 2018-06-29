using System;
using CoinbasePro.Services.Orders.Types;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Activate : BaseMessage
    {
        public Guid OrderId { get; set; }

        public StopType OrderType { get; set; }

        public decimal Size { get; set; }

        public decimal Funds { get; set; }

        public decimal TakerFeeRate { get; set; }

        public bool Private { get; set; }

        public decimal StopPrice { get; set; }

        public string UserId { get; set; }

        public Guid ProfileId { get; set; }

        public OrderSide Side { get; set; }

        public ProductType ProductId { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }
}