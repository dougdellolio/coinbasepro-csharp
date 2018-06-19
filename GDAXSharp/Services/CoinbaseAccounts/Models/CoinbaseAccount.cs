using System;
using CoinbasePro.Services.CoinbaseAccounts.Types;
using CoinbasePro.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinbasePro.Services.CoinbaseAccounts.Models
{
    public class CoinbaseAccount
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CoinbaseAccountType CoinbaseAccountType { get; set; }

        public bool Primary { get; set; }

        public bool Active { get; set; }

        public WireDepositInformation WireDepositInformation { get; set; }

        public SepaDepositInformation SepaDepositInformation { get; set; }
    }

    public class WireDepositInformation
    {
        public string AccountNumber { get; set; }

        public string RoutingNumber { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        public BankCountry BankCountry { get; set; }

        public string AccountName { get; set; }

        public string AccountAddress { get; set; }

        public string Reference { get; set; }
    }

    public class SepaDepositInformation
    {
        public string Iban { get; set; }

        public string Swift { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        public string BankCountryName { get; set; }

        public string AccountName { get; set; }

        public string AccountAddress { get; set; }

        public string Reference { get; set; }
    }

    public class BankCountry
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
