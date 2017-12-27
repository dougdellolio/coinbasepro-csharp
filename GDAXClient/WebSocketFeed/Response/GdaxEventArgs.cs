using System;

namespace GDAXClient.WebSocketFeed.Response
{
    public class GdaxEventArgs : EventArgs
    {
        public Price LatestPrice { get; set; }
    }
}
