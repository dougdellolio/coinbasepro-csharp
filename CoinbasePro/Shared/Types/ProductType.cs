using System.Runtime.Serialization;

namespace CoinbasePro.Shared.Types
{
    public enum ProductType
    {
        [EnumMember(Value = "Unknown-Unknown")]
        Unknown,
        [EnumMember(Value = "BTC-USD")]
        BtcUsd,
        [EnumMember(Value = "BTC-EUR")]
        BtcEur,
        [EnumMember(Value = "BTC-GBP")]
        BtcGbp,
        [EnumMember(Value = "BTC-USDC")]
        BtcUsdc,
        [EnumMember(Value = "ETH-USD")]
        EthUsd,
        [EnumMember(Value = "ETH-EUR")]
        EthEur,
        [EnumMember(Value = "ETH-BTC")]
        EthBtc,
        [EnumMember(Value = "ETH-GBP")]
        EthGbp,
        [EnumMember(Value = "ETH-USDC")]
        EthUsdc,
        [EnumMember(Value = "LTC-USD")]
        LtcUsd,
        [EnumMember(Value = "LTC-EUR")]
        LtcEur,
        [EnumMember(Value = "LTC-BTC")]
        LtcBtc,
        [EnumMember(Value = "LTC-GBP")]
        LtcGbp,
        [EnumMember(Value = "BCH-USD")]
        BchUsd,
        [EnumMember(Value = "BCH-EUR")]
        BchEur,
        [EnumMember(Value = "BCH-BTC")]
        BchBtc,
        [EnumMember(Value = "BCH-GBP")]
        BchGbp,
        [EnumMember(Value = "ETC-USD")]
        EtcUsd,
        [EnumMember(Value = "ETC-EUR")]
        EtcEur,
        [EnumMember(Value = "ETC-BTC")]
        EtcBtc,
        [EnumMember(Value = "ETC-GBP")]
        EtcGbp,
        [EnumMember(Value = "ZRX-USD")]
        ZrxUsd,
        [EnumMember(Value = "ZRX-EUR")]
        ZrxEur,
        [EnumMember(Value = "ZRX-BTC")]
        ZrxBtc
    }
}
