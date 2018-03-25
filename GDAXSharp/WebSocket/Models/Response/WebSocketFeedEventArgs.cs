using System;

namespace GDAXSharp.WebSocket.Models.Response
{
    public class WebfeedEventArgs<T> : EventArgs
    {
        public WebfeedEventArgs(T lastOrder)
        {
            LastOrder = lastOrder;
        }

        public T LastOrder { get; }
    }
}