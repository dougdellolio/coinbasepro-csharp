using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDAXClient.Services.Withdrawals
{
    public class Coinbase
    {
        public decimal amount { get; set; }

        public string currency { get; set; }

        public Guid coinbase_account_id { get; set; }
    }
}
