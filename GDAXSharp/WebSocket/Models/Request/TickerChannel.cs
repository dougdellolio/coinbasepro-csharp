using GDAXSharp.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;


namespace GDAXSharp.WebSocket.Models.Request
{
    public class TickerChannel
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("product_ids", ItemConverterType = typeof(StringEnumConverter))]
        public ProductType[] ProductIds { get; set; }

        [JsonProperty("channels")]
        public List<Channel> Channels { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("passphrase")]
        public string Passphrase { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    public class Channel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_ids", ItemConverterType = typeof(StringEnumConverter))]
        public ProductType[] ProductIds { get; set; }
    }
}
