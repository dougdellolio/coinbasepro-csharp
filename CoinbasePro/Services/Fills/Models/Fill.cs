using System;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.Fills.Models
{
    public class Fill
    {
        public Guid OrderId { get; set; }

        public ProductType ProductId { get; set; }
    }
}
