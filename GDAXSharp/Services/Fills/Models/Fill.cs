using System;
using GDAXSharp.Shared;
using Newtonsoft.Json;

namespace GDAXSharp.Services.Fills.Models
{
    public class Fill
    {
        [JsonProperty("order_id")]
        public Guid OrderId { get; set; }

        [JsonProperty("product_id")]
        public ProductType ProductId { get; set; }
    }
}
