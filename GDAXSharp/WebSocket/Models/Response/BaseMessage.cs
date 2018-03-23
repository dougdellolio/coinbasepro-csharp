using GDAXSharp.Shared.Types;
using Newtonsoft.Json;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class BaseMessage
    {
        [JsonProperty("type")]
        public ResponseType Type { get; set; }
    }
}