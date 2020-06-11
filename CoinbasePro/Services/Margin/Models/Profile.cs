
using System;

namespace CoinbasePro.Services.Margin.Models
{
    public class Profile
    {
        public Guid ProfileId { get; set; }

        //public string MarginInitialEquity { get; set; }

        //public string MarginWarningEquity { get; set; }

        //public string MarginCallEquity { get; set; }

        //public double EquityPercentage { get; set; }

        //public double SellingPower { get; set; }

        //public double BuyingPower { get; set; }

        //public double BorrowPower { get; set; }

        //public decimal InterestRate { get; set; }

        //public string InterestPaid { get; set; }

        //public string[] CollateralCurrencies { get; set; }

        //public string CollateralHoldValue { get; set; }

        //public DateTime LastLiquidationAt { get; set; }

        //public AvailableBorrowLimits AvailableBorrowLimits { get; set; }

        //public decimal BorrowLimit { get; set; }

        //public TopUpAmounts TopUpAmounts { get; set; }
    }

    public class AvailableBorrowLimits
    {
        public decimal MarginableLimit { get; set; }

        public decimal NonmarginableLimit { get; set; }
    }

    public class TopUpAmounts
    {
        public decimal BorrowableUsd { get; set; }

        public decimal NonBorrowableUsd { get; set; }
    }
}
