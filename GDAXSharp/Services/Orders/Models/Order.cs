using GDAXSharp.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Orders.Models
{
    public class Order
    {
        [JsonProperty("side")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }

        [JsonProperty("product_id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductId { get; set; }

        [JsonProperty("time_in_force")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce TimeInForce { get; set; }

        [JsonProperty("cancel_after")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GoodTillTime CancelAfter { get; set; }

        [JsonProperty("post_only")]
        public bool PostOnly { get; set; }
    }
}
