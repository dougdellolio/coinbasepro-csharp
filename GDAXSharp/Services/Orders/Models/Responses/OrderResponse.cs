using System;
using GDAXSharp.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Orders.Models.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        [JsonProperty("Product_id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        public string Stp { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }

        [JsonProperty("time_in_force")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce TimeInForce { get; set; }

        [JsonProperty("post_only")]
        public bool PostOnly { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("done_at")]
        public DateTime DoneAt { get; set; }

        [JsonProperty("done_reason")]
        public string DoneReason { get; set; }

        [JsonProperty("fill_fees")]
        public decimal FillFees { get; set; }

        [JsonProperty("filled_size")]
        public decimal FilledSize { get; set; }

        [JsonProperty("executed_value")]
        public decimal ExecutedValue { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        public bool Settled { get; set; }

        [JsonProperty("specified_funds")]
        public decimal SpecifiedFunds { get; set; }
    }
}
