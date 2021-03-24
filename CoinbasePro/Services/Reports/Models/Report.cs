using System;
using CoinbasePro.Services.Reports.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Reports.Models
{
    public class Report
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReportType ReportType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ProductId { get; set; }

        public string AccountId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FileFormat Format { get; set; }

        public string Email { get; set; }
    }
}
