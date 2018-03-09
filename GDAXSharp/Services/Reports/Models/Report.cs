﻿using System;
using GDAXSharp.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GDAXSharp.Services.Reports.Models
{
    public class Report
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReportType ReportType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProductType? ProductId { get; set; }

        public string AccountId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FileFormat Format { get; set; }

        public string Email { get; set; }
    }
}
