using GDAXSharp.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Level2 : BaseMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType ProductId { get; set; }

        public DateTimeOffset Time { get; set; }

        public List<string[]> Changes { get; set; }
    }
}
