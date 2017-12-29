using System;

namespace GDAXClient.Services.MarginTransfer.Models
{
    public class MarginTransfer
    {
        public Guid margin_profile_id { get; set; }

        public string type { get; set; }

        public Currency currency { get; set; }

        public decimal amount { get; set; }
    }
}
