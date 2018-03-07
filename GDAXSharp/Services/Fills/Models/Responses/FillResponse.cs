using System;
using GDAXSharp.Services.Orders.Models;
using GDAXSharp.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Fills.Models.Responses
{
    public class FillResponse
    {
        [JsonProperty("trade_id")]
        public int TradeId { get; set; }

        [JsonProperty("product_id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductId { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        [JsonProperty("order_id")]
        public Guid OrderId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FillLiquidity Liquidity { get; set; }

        public decimal Fee { get; set; }

        public bool Settled { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }
    }
}
