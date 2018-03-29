using GDAXSharp.Services.Orders.Types;
using GDAXSharp.Shared.Types;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Received : BaseMessage
    {
        public string OrderId { get; set; }

        public OrderType OrderType { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        public OrderSide Side { get; set; }

        public string ClientOid { get; set; } // Should this be GUID? Format I saw was cb07245e-3670-40bc-bd83-6990fce769d3

        public ProductType ProductId { get; set; }

        public long Sequence { get; set; }

        public System.DateTimeOffset Time { get; set; }
    }
}
