using System;
using GDAXSharp.Shared.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Accounts.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public Guid ProfileId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        public decimal Balance { get; set; }

        public decimal Hold { get; set; }

        public decimal Available { get; set; }

        public bool MarginEnabled { get; set; }

        public decimal FundedAmount { get; set; }

        public decimal DefaultAmount { get; set; }
    }
}
