using GDAXSharp.Shared.Types;
using GDAXSharp.WebSocket.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;


namespace GDAXSharp.WebSocket.Models.Request
{
    public class TickerChannel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public ActionType Type { get; set; }

        [JsonProperty("product_ids", ItemConverterType = typeof(StringEnumConverter))]
        public List<ProductType> ProductIds { get; set; }

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
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("name")]
        public ChannelType Name { get; set; }

        [JsonProperty("product_ids", ItemConverterType = typeof(StringEnumConverter))]
        public List<ProductType> ProductIds { get; set; }

        public Channel(ChannelType name, List<ProductType> productIds)
        {
            Name = name;
            ProductIds = productIds;
        }
    }
}
