using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.CoinbaseAccounts.Models
{
    public class CoinbaseAccount
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Shared.Currency Currency { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CoinbaseAccountType CoinbaseAccountType { get; set; }

        public bool Primary { get; set; }

        public bool Active { get; set; }

        [JsonProperty("wire_deposit_information")]
        public WireDepositInformation WireDepositInformation { get; set; }

        [JsonProperty("sepa_deposit_information")]
        public SepaDepositInformation SepaDepositInformation { get; set; }
    }

    public class WireDepositInformation
    {
        [JsonProperty("account_number")]
        public string AccountNumber { get; set; }

        [JsonProperty("routing_number")]
        public string RoutingNumber { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("bank_address")]
        public string BankAddress { get; set; }

        [JsonProperty("bank_country")]
        public BankCountry BankCountry { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("account_address")]
        public string AccountAddress { get; set; }

        public string Reference { get; set; }
    }

    public class SepaDepositInformation
    {
        public string Iban { get; set; }

        public string Swift { get; set; }

        [JsonProperty("bank_name")]
        public string BankName { get; set; }

        [JsonProperty("bank_address")]
        public string BankAddress { get; set; }

        [JsonProperty("bank_country_name")]
        public string BankCountryName { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("account_address")]
        public string AccountAddress { get; set; }

        public string Reference { get; set; }
    }

    public class BankCountry
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
