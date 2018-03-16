using System;
using GDAXSharp.Shared.Types;

namespace GDAXSharp.Services.Fills.Models
{
    public class Fill
    {
        public Guid OrderId { get; set; }

        public ProductType ProductId { get; set; }
    }
}
