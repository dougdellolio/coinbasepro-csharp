using System;

namespace GDAXSharp.Services.Withdrawals.Models
{
    public class Crypto
    {
        public decimal amount { get; set; }

        public string currency { get; set; }

        public Guid crypto_address { get; set; }
    }
}
