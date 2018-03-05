using System.Runtime.Serialization;

namespace GDAXSharp.Services.Fundings
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
