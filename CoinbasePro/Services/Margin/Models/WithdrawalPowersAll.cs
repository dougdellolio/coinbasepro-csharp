using System;
using System.Collections.Generic;

namespace CoinbasePro.Services.Margin.Models
{
    public class WithdrawalPowersAll
    {
        public Guid ProfileId { get; set; }

        public List<MarginableWithdrawalPower> MarginableWithdrawalPowers { get; set; }
    }

    public class MarginableWithdrawalPower
    {
        public string Currency { get; set; }

        public string WithdrawalPower { get; set; }
    }
}
