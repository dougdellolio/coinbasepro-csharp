using System;

namespace GDAXClient.Services.Withdrawals.Models
{
    public class Coinbase
    {
        public decimal amount { get; set; }

        public string currency { get; set; }

        public Guid coinbase_account_id { get; set; }
    }
}
