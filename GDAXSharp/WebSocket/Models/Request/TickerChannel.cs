using GDAXSharp.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace GDAXSharp.WebSocket.Models.Request
{
    public class TickerChannel
    {
        public string Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public List<ProductType> ProductIds { get; set; }

        public List<Channel> Channels { get; set; }

        public string Signature { get; set; }

        public string Key { get; set; }

        public string Passphrase { get; set; }

        public string Timestamp { get; set; }
    }

    public class Channel
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]

        public List<ProductType> ProductIds { get; set; }
    }
}