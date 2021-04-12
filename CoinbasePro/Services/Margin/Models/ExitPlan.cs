using System;
using System.Collections.Generic;

namespace CoinbasePro.Services.Margin.Models
{
    public class ExitPlan
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public Guid ProfileId { get; set; }

        public List<AccountsList> AccountsList { get; set; }

        public decimal EquityPercentage { get; set; }

        public decimal TotalAssetsUsd { get; set; }

        public decimal TotalLiabilitiesUsd { get; set; }

        public List<StrategiesList> StrategiesList { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class AccountsList
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }
    }

    //clean up
    public class StrategiesList
    {
        public string Type { get; set; }

        public string Amount { get; set; }

        public string Product { get; set; }

        public string Strategy { get; set; }

        public string AccountId { get; set; }

        public string OrderId { get; set; }
    }
}
