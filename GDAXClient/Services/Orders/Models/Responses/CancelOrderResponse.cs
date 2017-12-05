using System;
using System.Collections.Generic;

namespace GDAXClient.Services.Orders
{
    public class CancelOrderResponse
    {
        public IEnumerable<Guid> OrderIds { get; set; }
    }
}
