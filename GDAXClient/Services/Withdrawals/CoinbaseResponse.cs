using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAXClient.Services.Withdrawals
{
    public class CoinbaseResponse
    {
        public Guid Id { get; set; }

        public decimal amount { get; set; }

        public string currency { get; set; }
    }
}
