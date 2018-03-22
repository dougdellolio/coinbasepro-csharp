using System;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class WebSocketFeedEventArgs : EventArgs
    {
        public WebSocketFeedEventArgs(FeedOrder lastOrder)
        {
            LastOrder = lastOrder;
        }

        public FeedOrder LastOrder { get; }
    }
}