using System.Runtime.Serialization;

namespace GDAXSharp.Services.Reports.Models
{
    public enum ReportType
    {
        [EnumMember(Value = "fills")]
        Fills,
        [EnumMember(Value = "account")]
        Account
    }
}
