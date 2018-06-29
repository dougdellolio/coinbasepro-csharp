using System.Runtime.Serialization;

namespace CoinbasePro.Services.Reports.Types
{
    public enum FileFormat
    {
        [EnumMember(Value = "pdf")]
        Pdf,
        [EnumMember(Value = "csv")]
        Csv
    }
}
