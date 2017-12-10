using System;

namespace GDAXClient.Services.Accounts.Models
{
    public class AccountHistory
    {
        public string Id { get; set; }

        public DateTime Created_at { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public string Type { get; set; }

        public Details Details { get; set; }
    }

    public class Details
    {
        public string Order_id { get; set; }

        public string Trade_id { get; set; }

        public string Product_id { get; set; }
    }
}
