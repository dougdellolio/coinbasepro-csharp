using System.Runtime.Serialization;

namespace CoinbasePro.Services.Fills.Types
{
    public enum FillLiquidity
    {
        [EnumMember(Value = "M")]
        Maker,
        [EnumMember(Value = "T")]
        Taker
    }
}
