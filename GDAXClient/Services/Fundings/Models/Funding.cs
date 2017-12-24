using System;

namespace GDAXClient.Services.Fundings.Models
{
    public class Funding
    {
        public Guid Id { get; set; }

        public string Order_id { get; set; }

        public string Profile_id { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public DateTime Created_at { get; set; }

        public string Currency { get; set; }

        public decimal Repaid_amount { get; set; }

        public decimal Default_amount { get; set; }

        public bool Repaid_default { get; set; }
    }
}
