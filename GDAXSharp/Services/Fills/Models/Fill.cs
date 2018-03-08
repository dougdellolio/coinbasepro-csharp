using System;
using GDAXSharp.Shared;
using Newtonsoft.Json;

namespace GDAXSharp.Services.Fills.Models
{
    public class Fill
    {
        public Guid OrderId { get; set; }

        public ProductType ProductId { get; set; }
    }
}
