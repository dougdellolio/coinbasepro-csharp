using GDAXSharp.Shared.Types;
using Newtonsoft.Json;
using System;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class FeedOrder
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("open_24h")]
        public decimal Open24H { get; set; }

        [JsonProperty("volume_24h")]
        public decimal Volume24H { get; set; }

        [JsonProperty("low_24h")]
        public decimal Low24H { get; set; }

        [JsonProperty("high_24h")]
        public decimal High24H { get; set; }

        [JsonProperty("volume_30d")]
        public decimal Volume30D { get; set; }

        [JsonProperty("best_bid")]
        public decimal BestBid { get; set; }

        [JsonProperty("best_ask")]
        public decimal BestAsk { get; set; }

        [JsonProperty("side")]
        public Side Side { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("trade_id")]
        public long TradeId { get; set; }

        [JsonProperty("last_size")]
        public string LastSize { get; set; }
    }
}