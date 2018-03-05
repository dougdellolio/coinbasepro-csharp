using System;
using Newtonsoft.Json;

namespace GDAXSharp.Services.Products.Models
{
    public class ProductTicker
    {
        [JsonProperty("trade_id")]
        public int TradeId { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }

        public decimal Volume { get; set; }

        public DateTime Time { get; set; }
    }
}
