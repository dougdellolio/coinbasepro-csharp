using System;

namespace GDAXClient.Services.Reports.Models
{
    public class Report
    {
        public string type { get; set; }

        public DateTime start_date { get; set; }

        public DateTime end_date { get; set; }

        public string format { get; set; }

        public string email { get; set; }
    }
}
