using System.Runtime.Serialization;

namespace GDAXSharp.Services.Orders.Models
{
    public enum OrderSide
    {
        [EnumMember(Value = "buy")]
        Buy,
        [EnumMember(Value = "sell")]
        Sell
    }
}
