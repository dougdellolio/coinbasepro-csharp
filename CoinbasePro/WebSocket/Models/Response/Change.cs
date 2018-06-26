using System;
using CoinbasePro.Services.Orders.Types;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Change : BaseMessage
    {
        public Guid OrderId { get; set; }

        public decimal NewSize { get; set; }

        public decimal OldSize { get; set; }

        public decimal OldFunds { get; set; }

        public decimal NewFunds { get; set; }

        public decimal Price { get; set; }

        public OrderSide Side { get; set; }

        public ProductType ProductId { get; set; }

        public long Sequence { get; set; }

        public DateTime Time { get; set; }
    }
}