using Newtonsoft.Json;
using System;
using CoinbasePro.Services.Orders.Types;

namespace CoinbasePro.WebSocket.Models.Response
{
    public class Ticker : BaseMessage
    {
        public long Sequence { get; set; }

        public string ProductId { get; set; }

        public decimal Price { get; set; }

        [JsonProperty("open_24h")]
        public decimal Open24H { get; set; }

        [JsonProperty("volume_24h")]
        public decimal Volume24H { get; set; }

        [JsonProperty("low_24h")]
        public decimal Low24H { get; set; }

        [JsonProperty("high)24h")]
        public decimal High24H { get; set; }

        [JsonProperty("volume_30d")]
        public decimal Volume30D { get; set; }

        public decimal BestBid { get; set; }

        public decimal BestAsk { get; set; }

        public OrderSide Side { get; set; }

        public DateTimeOffset Time { get; set; }

        public long TradeId { get; set; }

        public decimal LastSize { get; set; }
    }
}
