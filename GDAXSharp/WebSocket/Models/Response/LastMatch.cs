using GDAXSharp.Services.Orders.Types;
using GDAXSharp.Shared.Types;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class LastMatch : BaseMessage
    {
        public long TradeId { get; set; }

        public string MakerOrderId { get; set; } // Should this be GUID? Response was like d67dd356-c4a8-406d-a471-7e9d181e813c

        public string TakerOrderId { get; set; } // Should this be GUID? Response was like 72ed49c2-8875-4784-bbad-be6ae2b29f11

        public OrderSide Side { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        public ProductType ProductId { get; set; }

        public long Sequence { get; set; }

        public System.DateTimeOffset Time { get; set; }
    }
}
