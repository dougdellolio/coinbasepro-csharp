using System;

namespace CoinbasePro.Services.Margin.Models
{
    public class WithdrawalPowers
    {
        public Guid ProfileId { get; set; }

        public decimal WithdrawalPower { get; set; }
    }
}
