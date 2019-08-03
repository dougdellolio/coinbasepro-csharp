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
        ZrxBtc,
        [EnumMember(Value = "BAT-USDC")]
        BatUsdc,
        [EnumMember(Value = "ZEC-USDC")]
        ZecUsdc,
        [EnumMember(Value = "MANA-USDC")]
        ManaUsdc,
        [EnumMember(Value = "DNT-USDC")]
        DntUsdc,
        [EnumMember(Value = "CVC-USDC")]
        CvcUsdc,
        [EnumMember(Value = "LOOM-USDC")]
        LoomUsdc,
        [EnumMember(Value = "GNT-USDC")]
        GntUsdc,
        [EnumMember(Value = "DAI-USDC")]
        DaiUsdc,
        [EnumMember(Value = "MKR-USDC")]
        MkrUsdc,
        [EnumMember(Value = "MKR-BTC")]
        MkrBtc,
        [EnumMember(Value = "ZIL-USDC")]
        ZilUsdc,
        [EnumMember(Value = "XRP-EUR")]
        XrpEur,
        [EnumMember(Value = "XRP-BTC")]
        XrpBtc,
        [EnumMember(Value = "XRP-USD")]
        XrpUsd,
        [EnumMember(Value = "XLM-USD")]
        XlmUsd,
        [EnumMember(Value = "XLM-BTC")]
        XlmBtc,
        [EnumMember(Value = "XLM-EUR")]
        XlmEur,
        [EnumMember(Value = "XTZ-USD")]
        XtzUsd,
        [EnumMember(Value = "XTZ-BTC")]
        XtzBtc,
        [EnumMember(Value = "EOS-USD")]
        EosUsd,
        [EnumMember(Value = "EOS-EUR")]
        EosEur,
        [EnumMember(Value = "EOS-BTC")]
        EosBtc,
        [EnumMember(Value = "REP-USD")]
        RepUsd,
        [EnumMember(Value = "REP-EUR")]
        RepEur,
        [EnumMember(Value = "REP-BTC")]
        RepBtc,
    }
}
