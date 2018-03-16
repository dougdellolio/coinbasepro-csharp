using System.Runtime.Serialization;

namespace GDAXSharp.Services.Orders.Types
{
    public enum OrderSide
    {
        [EnumMember(Value = "buy")]
        Buy,
        [EnumMember(Value = "sell")]
        Sell
    }
}
