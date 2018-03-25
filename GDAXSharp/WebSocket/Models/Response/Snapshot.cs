using GDAXSharp.Shared.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Snapshot : BaseMessage
    {
        [JsonProperty("product_id")]
        public ProductType ProductId { get; set; }

        [JsonProperty("bids")]
        public List<decimal[]> Bids { get; set; }

        [JsonProperty("asks")]
        public List<decimal[]> Asks { get; set; }
    }
}