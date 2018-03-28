using GDAXSharp.Services.Orders.Types;
using System;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Ticker : BaseMessage
    {
        public long Sequence { get; set; }

        public string ProductId { get; set; }

        public decimal Price { get; set; }

        public decimal Open24H { get; set; }

        public decimal Volume24H { get; set; }

        public decimal Low24H { get; set; }

        public decimal High24H { get; set; }

        public decimal Volume30D { get; set; }

        public decimal BestBid { get; set; }

        public decimal BestAsk { get; set; }

        public OrderSide Side { get; set; }

        public DateTimeOffset Time { get; set; }

        public long TradeId { get; set; }

        public decimal LastSize { get; set; }
    }
}
