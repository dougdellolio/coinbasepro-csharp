using System.Runtime.Serialization;

namespace CoinbasePro.Services.Accounts.Types
{
    public enum AccountHoldType
    {
        [EnumMember(Value = "order")]
        Order,
        [EnumMember(Value = "transfer")]
        Transfer
    }
}
