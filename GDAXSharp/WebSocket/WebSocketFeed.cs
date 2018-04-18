using System;
using GDAXSharp.Exceptions;
using GDAXSharp.Shared;
using WebSocket4Net;
using SuperSocket.ClientEngine;

namespace GDAXSharp.WebSocket
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
            if (webSocketFeed == null || webSocketFeed.State != WebSocketState.Open)
            {
                if (webSocketFeed != null)
                {
                    throw new GDAXSharpWebSocketException(
                        $"Websocket needs to be in the opened state. The current state is {webSocketFeed.State}")
                    {
                        WebSocketFeed = this,
                        StatusCode = webSocketFeed.State,
                        ErrorEvent = null
                    };
                }
            }

            if (webSocketFeed != null)
            {
                throw new GDAXSharpWebSocketException("Websocket can't be stopped")
                {
                    WebSocketFeed = this,
                    StatusCode = webSocketFeed.State,
                    ErrorEvent = null
                };
            }

            webSocketFeed.Close();
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
        }

        public void SetEvents()
        {
            webSocketFeed.MessageReceived += MessageReceived;
            webSocketFeed.Closed += Closed;
            webSocketFeed.Error += Error;
            webSocketFeed.Opened += Opened;
        }

        public event EventHandler<ErrorEventArgs> Error;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler Closed;

        public event EventHandler Opened;
    }
}
