using GDAXSharp.Shared.Types;
using System.Collections.Generic;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class Snapshot : BaseMessage
    {
        public ProductType ProductId { get; set; }

        public List<decimal[]> Bids { get; set; }

        public List<decimal[]> Asks { get; set; }
    }
}