using System.Runtime.Serialization;

namespace CoinbasePro.WebSocket.Types
{
    public enum ActionType
    {
        [EnumMember(Value = "subscribe")]
        Subscribe,
        [EnumMember(Value = "unsubscribe ")]
        Unsubscribe
    }
}
