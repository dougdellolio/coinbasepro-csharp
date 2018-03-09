using System;
using GDAXSharp.Shared;

namespace GDAXSharp.Services.Fills.Models
{
    public class Fill
    {
        public Guid OrderId { get; set; }

        public ProductType ProductId { get; set; }
    }
}
