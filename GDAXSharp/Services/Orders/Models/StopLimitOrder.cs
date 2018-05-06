﻿using System;
using GDAXSharp.Services.Orders.Types;
using GDAXSharp.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Orders.Models
{
    public class StopLimitOrder
    {
        public Guid? ClientOid { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        public decimal Size { get; set; }

        public decimal Price { get; set; }

        [JsonProperty("stop")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StopType StopType { get; set; }

        [JsonProperty("stop_price")]
        public decimal StopPrice { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce TimeInForce { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GoodTillTime CancelAfter { get; set; }

        public bool PostOnly { get; set; }


    }
}