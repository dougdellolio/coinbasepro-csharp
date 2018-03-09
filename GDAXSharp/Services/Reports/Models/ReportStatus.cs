using System.Runtime.Serialization;

namespace GDAXSharp.Services.Reports.Models
{
    public enum ReportStatus
    {
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "creating")]
        Creating,
        [EnumMember(Value = "ready")]
        Ready
    }
}
