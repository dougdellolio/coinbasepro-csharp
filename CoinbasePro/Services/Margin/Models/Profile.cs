
using CoinbasePro.Shared;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using System;

namespace CoinbasePro.Services.Margin.Models
{
    public class Profile
    {
        public Guid ProfileId { get; set; }

        public decimal MarginInitialEquity { get; set; }

        public decimal MarginWarningEquity { get; set; }

        public decimal MarginCallEquity { get; set; }

        public decimal EquityPercentage { get; set; }

        public decimal SellingPower { get; set; }

        public decimal BuyingPower { get; set; }

        public decimal BorrowPower { get; set; }

        public decimal InterestRate { get; set; }

        public decimal InterestPaid { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumWithDefaultConverter))]
        public Currency[] CollateralCurrencies { get; set; }

        public decimal CollateralHoldValue { get; set; }

        public DateTime LastLiquidationAt { get; set; }

        public AvailableBorrowLimits AvailableBorrowLimits { get; set; }

        public decimal BorrowLimit { get; set; }

        public TopUpAmounts TopUpAmounts { get; set; }
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
