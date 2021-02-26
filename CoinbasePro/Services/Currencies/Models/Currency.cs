using CoinbasePro.Shared;
using Newtonsoft.Json;

namespace CoinbasePro.Services.Currencies.Models
{
    public class Currency
    {
        [JsonConverter(typeof(StringEnumWithDefaultConverter))]
        public Shared.Types.Currency Id { get; set; }

        public string Name { get; set; }

        public decimal MinSize { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }

        public decimal MaxPrecision { get; set; }

        public string[] ConvertibleTo { get; set; }

        public Details Details { get; set; }
    }

    public class Details
    {
        public string Type { get; set; }

        public string Symbol { get; set; }

        public int NetworkConfirmations { get; set; }

        public int SortOrder { get; set; }

        public string CryptoAddressLink { get; set; }

        public string CryptoTransactionLink { get; set; }

        public string[] PushPaymentMethods { get; set; }

        public string[] GroupTypes { get; set; }

        public string DisplayName { get; set; }

        public decimal ProcessingTimeSeconds { get; set; }

        public decimal MinWithdrawalAmount { get; set; }

        public decimal MaxWithdrawalAmount { get; set; }
    }
}
