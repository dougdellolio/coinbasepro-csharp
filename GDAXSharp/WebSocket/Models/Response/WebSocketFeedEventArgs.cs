using System;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class TickerEventArgs : EventArgs
    {
        public TickerEventArgs(Ticker lastOrder)
        {
            LastOrder = lastOrder;
        }

        public Ticker LastOrder { get; }
    }
}