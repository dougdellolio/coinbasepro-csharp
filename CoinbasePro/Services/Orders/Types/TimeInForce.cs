using System.Runtime.Serialization;

namespace CoinbasePro.Services.Orders.Types
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
