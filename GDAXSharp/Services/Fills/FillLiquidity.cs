using System.Runtime.Serialization;

namespace GDAXSharp.Services.Fills
{
    public enum FillLiquidity
    {
        [EnumMember(Value = "M")]
        Maker,
        [EnumMember(Value = "T")]
        Taker
    }
}
