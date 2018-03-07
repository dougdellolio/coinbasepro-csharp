using System.Runtime.Serialization;

namespace GDAXSharp.Services.Orders.Models
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
