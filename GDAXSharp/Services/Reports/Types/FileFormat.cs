using System.Runtime.Serialization;

namespace GDAXSharp.Services.Reports.Types
{
    public enum FileFormat
    {
        [EnumMember(Value = "pdf")]
        Pdf,
        [EnumMember(Value = "csv")]
        Csv
    }
}
