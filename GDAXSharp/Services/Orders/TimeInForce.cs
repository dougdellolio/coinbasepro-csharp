using System.Runtime.Serialization;

namespace GDAXSharp.Services.Orders
{
    public enum TimeInForce
    {
        [EnumMember(Value = "GTC")]
        Gtc,
        [EnumMember(Value = "GTT")]
        Gtt,
        [EnumMember(Value = "IOC")]
        Ioc,
        [EnumMember(Value = "FOK")]
        Fok
    }
}
