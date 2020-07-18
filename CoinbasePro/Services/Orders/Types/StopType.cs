using System.Runtime.Serialization;

namespace CoinbasePro.Services.Orders.Types
{
    public enum StopType
    {
        [EnumMember(Value = "Unknown")]
        Unknown,
        [EnumMember(Value = "loss")]
        Loss,
        [EnumMember(Value = "entry")]
        Entry,
    }
}
