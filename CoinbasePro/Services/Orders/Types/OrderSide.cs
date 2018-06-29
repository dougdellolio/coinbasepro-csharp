using System.Runtime.Serialization;

namespace CoinbasePro.Services.Orders.Types
{
    public enum OrderSide
    {
        [EnumMember(Value = "buy")]
        Buy,
        [EnumMember(Value = "sell")]
        Sell
    }
}
