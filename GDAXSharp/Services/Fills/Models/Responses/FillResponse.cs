using System;
using CoinbasePro.Services.Fills.Types;
using CoinbasePro.Services.Orders.Types;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Fills.Models.Responses
{
    public class FillResponse
    {
        public int TradeId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductId { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public Guid OrderId { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FillLiquidity Liquidity { get; set; }

        public decimal Fee { get; set; }

        public bool Settled { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }
    }
}
