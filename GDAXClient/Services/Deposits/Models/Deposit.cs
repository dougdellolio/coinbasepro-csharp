using System;

namespace GDAXClient.Services.Deposits.Models
{
    public class Deposit
    {
        public decimal amount { get; set; }

        public string currency { get; set; }

        public Guid payment_method_id { get; set; }
    }
}
