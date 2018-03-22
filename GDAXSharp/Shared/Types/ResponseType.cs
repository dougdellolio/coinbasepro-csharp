using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Shared.Types
{
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum ResponseType
    {
        Subscriptions,
        Heartbeat,
        Ticker,
        Snapshot,
        L2update,
        Received,
        Open,
        Done,
        Match,
        Change,
        Activate,
        Error
    }
}