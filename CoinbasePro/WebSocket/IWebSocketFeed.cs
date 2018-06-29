using System;
using CoinbasePro.WebSocket.Models.Response;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace CoinbasePro.WebSocket
{
    public interface IWebSocketFeed
    {
        WebSocketState State { get; }

        void Stop();

        void Close();

        void Dispose();

        void Send(string json);

        void Open();

        event EventHandler Closed;

        event EventHandler Opened;

        event EventHandler<ErrorEventArgs> Error;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        void Invoke<T>(
            EventHandler<WebfeedEventArgs<T>> onReceived,
            object sender, 
            WebfeedEventArgs<T> webfeedEventArgs);
    }
}
