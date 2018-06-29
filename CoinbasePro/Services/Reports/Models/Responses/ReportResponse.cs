using System;
using CoinbasePro.Services.Reports.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.Reports.Models.Responses
{
    public class ReportResponse
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ReportType Type { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ReportStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public object CompletedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public object FileUrl { get; set; }

        public Params Params { get; set; }
    }

    public class Params
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
