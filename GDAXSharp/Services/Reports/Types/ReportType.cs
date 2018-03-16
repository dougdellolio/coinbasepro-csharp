using System.Runtime.Serialization;

namespace GDAXSharp.Services.Reports.Types
{
    public enum ReportType
    {
        [EnumMember(Value = "fills")]
        Fills,
        [EnumMember(Value = "account")]
        Account
    }
}
