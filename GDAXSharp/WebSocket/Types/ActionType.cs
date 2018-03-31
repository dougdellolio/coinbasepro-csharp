using System.Runtime.Serialization;

namespace GDAXSharp.WebSocket.Types
{
    public enum ActionType
    {
        [EnumMember(Value = "subscribe")]
        Subscribe,
        [EnumMember(Value = "unsubscribe ")]
        Unsubscribe
    }
}
