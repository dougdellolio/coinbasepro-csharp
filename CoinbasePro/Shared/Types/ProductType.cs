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
        [EnumMember(Value = "ZEC-BTC")]
        ZecBtc,
        [EnumMember(Value = "ZEC-USD")]
        ZecUsd,
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
        [EnumMember(Value = "DAI-USD")]
        DaiUsd,
        [EnumMember(Value = "DAI-USDC")]
        DaiUsdc,
        [EnumMember(Value = "MKR-USD")]
        MkrUsd,
        [EnumMember(Value = "MKR-BTC")]
        MkrBtc,
        [EnumMember(Value = "XRP-EUR")]
        XrpEur,
        [EnumMember(Value = "XRP-BTC")]
        XrpBtc,
        [EnumMember(Value = "XRP-USD")]
        XrpUsd,
        [EnumMember(Value = "XRP-GBP")]
        XrpGbp,
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
        [EnumMember(Value = "XTZ-EUR")]
        XtzEur,
        [EnumMember(Value = "XTZ-GBP")]
        XtzGbp,
        [EnumMember(Value = "EOS-USD")]
        EosUsd,
        [EnumMember(Value = "EOS-EUR")]
        EosEur,
        [EnumMember(Value = "EOS-BTC")]
        EosBtc,
        [EnumMember(Value = "REP-USD")]
        RepUsd,
        [EnumMember(Value = "REP-BTC")]
        RepBtc,
        [EnumMember(Value = "ALGO-USD")]
        AlgoUsd,
        [EnumMember(Value = "ALGO-EUR")]
        AlgoEur,
        [EnumMember(Value = "ALGO-GBP")]
        AlgoGbp,
        [EnumMember(Value = "ALGO-BTC")]
        AlgoBtc,
        [EnumMember(Value = "BAT-ETH")]
        BatEth,
        [EnumMember(Value = "ETH-DAI")]
        EthDai,
        [EnumMember(Value = "LINK-BTC")]
        LinkBtc,
        [EnumMember(Value = "LINK-ETH")]
        LinkEth,
        [EnumMember(Value = "LINK-USD")]
        LinkUsd,
        [EnumMember(Value = "LINK-EUR")]
        LinkEur,
        [EnumMember(Value = "LINK-GBP")]
        LinkGbp,
        [EnumMember(Value = "DASH-USD")]
        DashUsd,
        [EnumMember(Value = "DASH-BTC")]
        DashBtc,
        [EnumMember(Value = "OXT-USD")]
        OxtUsd,
        [EnumMember(Value = "ATOM-USD")]
        AtomUsd,
        [EnumMember(Value = "ATOM-BTC")]
        AtomBtc,
        [EnumMember(Value = "OMG-USD")]
        OmgUsd,
        [EnumMember(Value = "OMG-EUR")]
        OmgEur,
        [EnumMember(Value = "OMG-GBP")]
        OmgGbp,
        [EnumMember(Value = "OMG-BTC")]
        OmgBtc,
        [EnumMember(Value = "KNC-USD")]
        KncUsd,
        [EnumMember(Value = "KNC-BTC")]
        KncBtc,
        [EnumMember(Value = "COMP-BTC")]
        CompBtc,
        [EnumMember(Value = "COMP-USD")]
        CompUsd,
        [EnumMember(Value = "BAND-USD")]
        BandUsd,
        [EnumMember(Value = "BAND-EUR")]
        BandEur,
        [EnumMember(Value = "BAND-GBP")]
        BandGbp,
        [EnumMember(Value = "BAND-BTC")]
        BandBtc,
        [EnumMember(Value = "NMR-USD")]
        NmrUsd,
        [EnumMember(Value = "NMR-EUR")]
        NmrEur,
        [EnumMember(Value = "NMR-GBP")]
        NmrGbp,
        [EnumMember(Value = "NMR-BTC")]
        NmrBtc,
        [EnumMember(Value = "UMA-USD")]
        UmaUsd,
        [EnumMember(Value = "UMA-BTC")]
        UmaBtc,
        [EnumMember(Value = "UMA-EUR")]
        UmaEur,
        [EnumMember(Value = "UMA-GBP")]
        UmaGbp,
        [EnumMember(Value = "LRC-USD")]
        LrcUsd,
        [EnumMember(Value = "LRC-BTC")]
        LrcBtc,
        [EnumMember(Value = "YFI-USD")]
        YfiUsd,
        [EnumMember(Value = "YFI-BTC")]
        YfiBtc,
        [EnumMember(Value = "UNI-USD")]
        UniUsd,
        [EnumMember(Value = "UNI-BTC")]
        UniBtc,
        [EnumMember(Value = "BAL-USD")]
        BalUsd,
        [EnumMember(Value = "BAL-BTC")]
        BalBtc,
        [EnumMember(Value = "REN-USD")]
        RenUsd,
        [EnumMember(Value = "REN-BTC")]
        RenBtc,
        [EnumMember(Value = "CGLD-BTC")]
        CgldBtc,
        [EnumMember(Value = "CGLD-USD")]
        CgldUsd,
        [EnumMember(Value = "CGLD-EUR")]
        CgldEur,
        [EnumMember(Value = "CGLD-GBP")]
        CgldGbp,
        [EnumMember(Value = "WBTC-BTC")]
        WbtcBtc,
        [EnumMember(Value = "WBTC-USD")]
        WbtcUsd,
        [EnumMember(Value = "NU-USD")]
        NuUsd,
        [EnumMember(Value = "NU-EUR")]
        NuEur,
        [EnumMember(Value = "NU-GBP")]
        NuGbp,
        [EnumMember(Value = "NU-BTC")]
        NuBtc,
        [EnumMember(Value = "FIL-USD")]
        FilUsd,
        [EnumMember(Value = "FIL-EUR")]
        FilEur,
        [EnumMember(Value = "FIL-GBP")]
        FilGbp,
        [EnumMember(Value = "FIL-BTC")]
        FilBtc,
        [EnumMember(Value = "AAVE-USD")]
        AaveUsd,
        [EnumMember(Value = "AAVE-BTC")]
        AaveBtc,
        [EnumMember(Value = "AAVE-EUR")]
        AaveEur,
        [EnumMember(Value = "AAVE-GBP")]
        AaveGbp,
        [EnumMember(Value = "BNT-USD")]
        BntUsd,
        [EnumMember(Value = "BNT-BTC")]
        BntBtc,
        [EnumMember(Value = "BNT-EUR")]
        BntEur,
        [EnumMember(Value = "BNT-GBP")]
        BntGbp,
        [EnumMember(Value = "SNX-USD")]
        SnxUsd,
        [EnumMember(Value = "SNX-BTC")]
        SnxBtc,
        [EnumMember(Value = "SNX-EUR")]
        SnxEur,
        [EnumMember(Value = "SNX-GBP")]
        SnxGbp,
        [EnumMember(Value = "GRT-USD")]
        GrtUsd,
        [EnumMember(Value = "GRT-BTC")]
        GrtBtc,
        [EnumMember(Value = "GRT-EUR")]
        GrtEur,
        [EnumMember(Value = "GRT-GBP")]
        GrtGbp
    }
}
