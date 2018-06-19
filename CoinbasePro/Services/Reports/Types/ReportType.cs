using System.Runtime.Serialization;

namespace CoinbasePro.Services.Reports.Types
{
    public enum ReportType
    {
        [EnumMember(Value = "fills")]
        Fills,
        [EnumMember(Value = "account")]
        Account
    }
}
