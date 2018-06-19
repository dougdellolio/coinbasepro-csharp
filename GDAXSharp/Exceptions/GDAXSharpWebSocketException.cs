using SuperSocket.ClientEngine;
using System;
using CoinbasePro.WebSocket;

namespace CoinbasePro.Exceptions
{
    public class GDAXSharpWebSocketException : Exception
    {
        public WebSocket4Net.WebSocketState StatusCode { get; set; }

        public IWebSocketFeed WebSocketFeed { get; set; }

        public ErrorEventArgs ErrorEvent { get; set; }

        public GDAXSharpWebSocketException()
        {
        }

        public GDAXSharpWebSocketException(string message)
            : base(message)
        {
        }

        public GDAXSharpWebSocketException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
