using System.Runtime.Serialization;

namespace CoinbasePro.Services.Orders.Types
{
    public enum OrderType
    {
        [EnumMember(Value = "limit")]
        Limit,
        [EnumMember(Value = "market")]
        Market,
        [EnumMember(Value = "stop")]
        Stop
    }
}
