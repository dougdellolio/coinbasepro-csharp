using System.Runtime.Serialization;

namespace GDAXSharp.Services.Orders.Types
{
    public enum GoodTillTime
    {
        [EnumMember(Value = "min")]
        Min,
        [EnumMember(Value = "hour")]
        Hour,
        [EnumMember(Value = "day")]
        Day
    }
}
