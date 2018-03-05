using System.Runtime.Serialization;

namespace GDAXSharp.Services.Accounts.Models
{
    public enum AccountEntryType
    {
        [EnumMember(Value = "transfer")]
        Transfer,
        [EnumMember(Value = "match")]
        Match,
        [EnumMember(Value = "fee")]
        Fee,
        [EnumMember(Value = "rebate")]
        Rebate
    }
}