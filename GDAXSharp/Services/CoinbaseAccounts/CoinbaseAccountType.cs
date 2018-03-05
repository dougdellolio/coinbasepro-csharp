using System.Runtime.Serialization;

namespace GDAXSharp.Services.CoinbaseAccounts
{
    // see https://developers.coinbase.com/api/v2#account-resource

    public enum CoinbaseAccountType
    {
        [EnumMember(Value = "multisig_vault")]
        MultisigVault,
        [EnumMember(Value = "vault")]
        Vault,
        [EnumMember(Value = "multisig")]
        Multisig,
        [EnumMember(Value = "fiat")]
        Fiat,
        [EnumMember(Value = "wallet")]
        Wallet
    }
}