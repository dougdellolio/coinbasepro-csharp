using System.Collections.Generic;

namespace CoinbasePro.Services.Limits.Models
{
    public class Limit
    {
        public string LimitCurrency { get; set; }

        public Dictionary<string, Dictionary<string, Details>> TransferLimits { get; set; }
    }

    public class Details
    {
        public decimal Max { get; set; }

        public decimal Remaining { get; set; }

        public int PeriodInDays { get; set; }
    }
}
