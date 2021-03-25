using System;

namespace CoinbasePro.Services.Profiles.Models
{
    public class ProfileTransfer
    {
        public Guid From { get; set; }

        public Guid To { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
    }
}
