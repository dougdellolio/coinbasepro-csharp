using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using CoinbasePro.Shared.Types;
using CoinbasePro.WebSocket.Types;


namespace CoinbasePro.WebSocket.Models.Request
{
    public class TickerChannel
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ActionType Type { get; set; }

        [JsonProperty("product_ids", ItemConverterType = typeof(StringEnumConverter))]
        public List<ProductType> ProductIds { get; set; }

        public List<Channel> Channels { get; set; }

        public string Signature { get; set; }

        public string Key { get; set; }

        public string Passphrase { get; set; }

        public string Timestamp { get; set; }
    }

    public class Channel
    {
        [JsonConverter(typeof(StringEnumConverter))]
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
