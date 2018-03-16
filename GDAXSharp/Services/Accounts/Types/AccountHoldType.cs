using System.Runtime.Serialization;

namespace GDAXSharp.Services.Accounts.Types
{
    public enum AccountHoldType
    {
        [EnumMember(Value = "order")]
        Order,
        [EnumMember(Value = "transfer")]
        Transfer
    }
}
