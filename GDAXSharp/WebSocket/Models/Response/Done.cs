using GDAXSharp.Services.Orders.Types;
using GDAXSharp.Shared.Types;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Done : BaseMessage
    {
        public OrderSide Side { get; set; }

        public string OrderId { get; set; } // Should this be a GUID? Format I got was like: 2aa6f96c-90d1-454a-a750-2c1323d83413

        public string Reason { get; set; } // This could be an enum? canceled or filled, right?

        public ProductType ProductId { get; set; }

        public string Price { get; set; }

        public string RemainingSize { get; set; }

        public long Sequence { get; set; }

        public System.DateTimeOffset Time { get; set; }
    }
}
