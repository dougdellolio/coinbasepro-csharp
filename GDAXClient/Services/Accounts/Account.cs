using System;

namespace GDAXClient.Services.Accounts
{
    public class Account
    {
        public Guid id { get; set; }

        public string currency { get; set; }

        public decimal balance { get; set; }

        public decimal hold { get; set; }

        public decimal available { get; set; }

        public bool margin_enabled { get; set; }

        public decimal funded_amount { get; set; }

        public decimal default_amount { get; set; }
    }
}
