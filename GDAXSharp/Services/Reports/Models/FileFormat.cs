using System.Runtime.Serialization;

namespace GDAXSharp.Services.Reports.Models
{
    public enum FileFormat
    {
        [EnumMember(Value = "pdf")]
        Pdf,
        [EnumMember(Value = "csv")]
        Csv
    }
}
