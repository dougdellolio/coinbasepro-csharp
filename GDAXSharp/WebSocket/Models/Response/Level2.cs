using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Level2 : BaseMessage
    {
        [JsonProperty("product_id")]
        public string ProductId { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("changes")]
        public List<string[]> Changes { get; set; }
    }
}