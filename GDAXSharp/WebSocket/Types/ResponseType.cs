using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.WebSocket.Types
{
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum ResponseType
    {
        Subscriptions,
        Heartbeat,
        Ticker,
        Snapshot,
        L2Update,
        Received,
        Open,
        Done,
        Match,
        Change,
        Activate,
        Error
    }
}