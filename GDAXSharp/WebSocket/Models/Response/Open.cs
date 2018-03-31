using GDAXSharp.Services.Orders.Types;
using GDAXSharp.Shared.Types;
using System;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Open : BaseMessage
    {
        public OrderSide Side { get; set; }

        public decimal Price { get; set; }

        public Guid OrderId { get; set; }

        public decimal RemainingSize { get; set; }

        public ProductType ProductId { get; set; }

        public long Sequence { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
