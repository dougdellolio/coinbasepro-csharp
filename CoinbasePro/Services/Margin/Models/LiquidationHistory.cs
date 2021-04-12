using CoinbasePro.Services.Orders.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CoinbasePro.Services.Margin.Models
{
    public class LiquidationHistory
    {
        public Guid EventId { get; set; }

        public DateTime EventTime { get; set; }

        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public Guid Id { get; set; }

        public decimal Size { get; set; }

        public string ProductId { get; set; }

        public Guid ProfileId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }

        public bool PostOnly { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DoneAt { get; set; }

        public string DoneReason { get; set; }

        public decimal FillFees { get; set; }

        public string FilledSize { get; set; }

        public string ExecutedValue { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        public bool Settled { get; set; }
    }
}
