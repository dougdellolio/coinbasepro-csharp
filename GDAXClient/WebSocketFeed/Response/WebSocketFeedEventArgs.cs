using System;

namespace GDAXClient.WebSocketFeed.Response
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
