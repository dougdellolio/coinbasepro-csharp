using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Accounts.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Shared.Currency Currency { get; set; }

        public decimal Balance { get; set; }

        public decimal Hold { get; set; }

        public decimal Available { get; set; }

        public bool MarginEnabled { get; set; }

        public decimal FundedAmount { get; set; }

        public decimal DefaultAmount { get; set; }
    }
}
