using System;
using CoinbasePro.Shared;
using CoinbasePro.WebSocket.Models.Response;
using WebSocket4Net;
using SuperSocket.ClientEngine;

namespace CoinbasePro.WebSocket
{
    public class WebSocketFeed : IWebSocketFeed
    {
        private readonly WebSocket4Net.WebSocket webSocketFeed;

        public WebSocketFeed(bool sandBox)
        {
            var socketUrl = sandBox
                ? ApiUris.WebsocketUriSandbox
                : ApiUris.WebsocketUri;

            webSocketFeed = new WebSocket4Net.WebSocket(socketUrl);
        }

        public WebSocketState State => webSocketFeed.State;

        public void Stop()
        {
            Close();
        }

        public void Close()
        {
            webSocketFeed.Close();
        }

        public void Dispose()
        {
            webSocketFeed.Dispose();
        }

        public void Send(string json)
        {
            webSocketFeed.Send(json);
        }

        public void Open()
        {
            webSocketFeed.Open();

            webSocketFeed.MessageReceived += MessageReceived;
            webSocketFeed.Closed += Closed;
            webSocketFeed.Error += Error;
            webSocketFeed.Opened += Opened;
        }

        public void Invoke<T>(
            EventHandler<WebfeedEventArgs<T>> onReceived,
            object sender,
            WebfeedEventArgs<T> webfeedEventArgs)
        {
            onReceived?.Invoke(sender, webfeedEventArgs);
        }

        public event EventHandler Opened;
        public event EventHandler Closed;
        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}
