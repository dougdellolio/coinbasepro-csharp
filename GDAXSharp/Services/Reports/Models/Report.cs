using System;
using GDAXSharp.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Reports.Models
{
    public class Report
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ReportType Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType? ProductId { get; set; }

        public string AccountId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FileFormat Format { get; set; }

        public string Email { get; set; }
    }
}
