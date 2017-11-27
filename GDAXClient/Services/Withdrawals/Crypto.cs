using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAXClient.Services.Withdrawals
{
    public class Crypto
    {
        public decimal amount { get; set; }

        public string currency { get; set; }

        public Guid crypto_address { get; set; }
    }
}
