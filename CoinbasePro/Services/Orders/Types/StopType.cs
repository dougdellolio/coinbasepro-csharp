using System.Runtime.Serialization;

namespace CoinbasePro.Services.Orders.Types
{
    public enum StopType
    {
        [EnumMember(Value = "loss")]
        Loss,
        [EnumMember(Value = "entry")]
        Entry
    }
}
