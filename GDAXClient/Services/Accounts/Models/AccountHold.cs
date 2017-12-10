using System;

namespace GDAXClient.Services.Accounts.Models
{
    public class AccountHold
    {
        public string Id { get; set; }

        public string Account_id { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime Updated_at { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

        public string @Ref { get; set; }
    }
}
