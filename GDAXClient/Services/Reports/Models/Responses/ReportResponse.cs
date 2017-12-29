using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace GDAXClient.Services.Reports.Models.Responses
{
    public class ReportResponse
    {
        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public DateTime Created_at { get; set; }

        [DefaultValue(null)]
        public string Completed_at { get; set; }

        public DateTime Expires_at { get; set; }

        [DefaultValue(null)]
        public string File_url { get; set; }

        [JsonProperty("Params")]
        public ReportDuration ReportDuration { get; set; }
    }

    public class ReportDuration
    {
        public DateTime Start_date { get; set; }

        public DateTime End_date { get; set; }
    }
}
