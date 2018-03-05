using System.Runtime.Serialization;

namespace GDAXSharp.Services.Accounts.Models
{
    public enum AccountHoldType
    {
        [EnumMember(Value = "order")]
        Order,
        [EnumMember(Value = "transfer")]
        Transfer
    }
}