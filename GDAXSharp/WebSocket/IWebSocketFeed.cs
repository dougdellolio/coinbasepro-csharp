using System;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace GDAXSharp.WebSocket
{
    public interface IWebSocketFeed
    {
        WebSocketState State { get; }

        void Stop();

        void Close();

        void Dispose();

        void Send(string json);

        void Open();

        void SetEvents();

        event EventHandler<ErrorEventArgs> Error;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        event EventHandler Closed;

        event EventHandler Opened;
    }
}
