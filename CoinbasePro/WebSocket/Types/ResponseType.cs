using System.Runtime.Serialization;

namespace CoinbasePro.WebSocket.Types
{
    public enum ResponseType
    {
        Unknown = 0,
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
