using SuperSocket.ClientEngine;
using System;

namespace GDAXSharp.Exceptions
{
    public class GDAXSharpWebSocketException : Exception
    {
        public WebSocket4Net.WebSocketState StatusCode { get; set; }

        public WebSocket4Net.WebSocket WebSocket { get; set; }

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
