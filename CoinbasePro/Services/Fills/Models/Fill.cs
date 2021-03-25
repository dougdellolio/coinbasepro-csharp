using System;

namespace CoinbasePro.Services.Fills.Models
{
    public class Fill
    {
        public Guid OrderId { get; set; }

        public string ProductId { get; set; }
    }
}
