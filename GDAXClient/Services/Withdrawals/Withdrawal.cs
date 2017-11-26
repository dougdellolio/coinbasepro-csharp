using System;

namespace GDAXClient.Services.Withdrawals
{
    public class Withdrawal
    {
        public decimal amount { get; set; }

        public string currency { get; set; }

        public Guid payment_method_id { get; set; }
    }
}
