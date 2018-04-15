using System;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace GDAXSharp.WebSocket
{
    public interface IWebSocketFeed
    {
        WebSocket4Net.WebSocket Create(string socketUrl);

        void Stop(WebSocket4Net.WebSocket webSocket);

        void Close(WebSocket4Net.WebSocket webSocket);

        void Dispose(WebSocket4Net.WebSocket webSocket);

        void Send(WebSocket4Net.WebSocket webSocket, string json);

        void Open(WebSocket4Net.WebSocket webSocket);

        event EventHandler<ErrorEventArgs> Error;

        event EventHandler<DataReceivedEventArgs> DataReceived;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        event EventHandler Closed;

        event EventHandler Opened;
    }
}
