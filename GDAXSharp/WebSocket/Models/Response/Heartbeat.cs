using GDAXSharp.Shared.Types;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Heartbeat : BaseMessage
    {
        public long LastTradeId { get; set; }

        public ProductType ProductId { get; set; }

        public long Sequence { get; set; }

        public System.DateTimeOffset Time { get; set; }
    }
}
