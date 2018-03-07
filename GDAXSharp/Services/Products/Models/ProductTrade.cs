using System;
using GDAXSharp.Services.Orders.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Products.Models
{
    public class ProductTrade
    {
        public DateTime Time { get; set; }

        [JsonProperty("trade_id")]
        public int TradeId { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }
    }
}
