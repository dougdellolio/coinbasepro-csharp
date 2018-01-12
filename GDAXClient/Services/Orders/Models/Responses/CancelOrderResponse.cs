using System;
using System.Collections.Generic;

namespace GDAXClient.Services.Orders.Models.Responses
{
    public class CancelOrderResponse
    {
        public IEnumerable<Guid> OrderIds { get; set; }
    }
}
