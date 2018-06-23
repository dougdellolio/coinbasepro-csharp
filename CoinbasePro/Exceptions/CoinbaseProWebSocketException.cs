using SuperSocket.ClientEngine;
using System;
using CoinbasePro.WebSocket;

namespace CoinbasePro.Exceptions
{
    public class CoinbaseProWebSocketException : Exception
    {
        public WebSocket4Net.WebSocketState StatusCode { get; set; }

        public IWebSocketFeed WebSocketFeed { get; set; }

        public ErrorEventArgs ErrorEvent { get; set; }

        public CoinbaseProWebSocketException()
        {
        }

        public CoinbaseProWebSocketException(string message)
            : base(message)
        {
        }

        public CoinbaseProWebSocketException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
