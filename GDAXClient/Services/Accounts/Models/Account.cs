using System;

namespace GDAXClient.Services.Accounts
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public decimal Balance { get; set; }

        public decimal Hold { get; set; }

        public decimal Available { get; set; }

        public bool Margin_enabled { get; set; }

        public decimal Funded_amount { get; set; }

        public decimal Default_amount { get; set; }
    }
}
