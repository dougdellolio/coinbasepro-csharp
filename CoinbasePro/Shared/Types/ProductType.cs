using System.Runtime.Serialization;

namespace CoinbasePro.Shared.Types
{
    public enum ProductType
    {
        [EnumMember(Value = "BTC-USD")]
        BtcUsd,
        [EnumMember(Value = "BTC-EUR")]
        BtcEur,
        [EnumMember(Value = "BTC-GBP")]
        BtcGbp,
        [EnumMember(Value = "ETH-USD")]
        EthUsd,
        [EnumMember(Value = "ETH-EUR")]
        EthEur,
        [EnumMember(Value = "ETH-BTC")]
        EthBtc,
        [EnumMember(Value = "LTC-USD")]
        LtcUsd,
        [EnumMember(Value = "LTC-EUR")]
        LtcEur,
        [EnumMember(Value = "LTC-BTC")]
        LtcBtc,
        [EnumMember(Value = "BCH-USD")]
        BchUsd,
        [EnumMember(Value = "BCH-EUR")]
        BchEur,
        [EnumMember(Value = "BCH-BTC")]
        BchBtc
    }
}
