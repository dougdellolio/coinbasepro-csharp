using GDAXSharp.Services.Orders.Types;
using GDAXSharp.Shared.Types;
using System;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Received : BaseMessage
    {
        public Guid OrderId { get; set; }

        public OrderType OrderType { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        public OrderSide Side { get; set; }

        public Guid ClientOid { get; set; }

        public ProductType ProductId { get; set; }

        public long Sequence { get; set; }

        public System.DateTimeOffset Time { get; set; }
    }
}
