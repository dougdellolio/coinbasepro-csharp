using System;

namespace GDAXSharp.Services.Reports.Models.Responses
{
    public class ReportResponse
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

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
