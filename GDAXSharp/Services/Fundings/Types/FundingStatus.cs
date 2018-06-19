using System.Runtime.Serialization;

namespace CoinbasePro.Services.Fundings.Types
{
    public enum FundingStatus
    {
        [EnumMember(Value = "outstanding")]
        Outstanding,
        [EnumMember(Value = "settled")]
        Settled,
        [EnumMember(Value = "rejected")]
        Rejected
    }
}
