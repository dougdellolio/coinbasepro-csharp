using System.Runtime.Serialization;

namespace GDAXSharp.WebSocket.Types
{
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
        [EnumMember(Value = "last_match")]
        LastMatch,
        Change,
        Activate,
        Error
    }
}
