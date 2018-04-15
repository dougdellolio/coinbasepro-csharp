using System;
using GDAXSharp.Exceptions;
using WebSocket4Net;
using SuperSocket.ClientEngine;

namespace GDAXSharp.WebSocket
{
    public class WebSocketFeed : IWebSocketFeed
    {
        public WebSocket4Net.WebSocket Create(string socketUrl)
        {
            return new WebSocket4Net.WebSocket(socketUrl);
        }

        public void Stop(WebSocket4Net.WebSocket webSocketFeed)
        {
            if (webSocketFeed == null || webSocketFeed.State != WebSocketState.Open)
            {
                if (webSocketFeed != null)
                {
                    throw new GDAXSharpWebSocketException(
                        $"Websocket needs to be in the opened state. The current state is {webSocketFeed.State}")
                    {
                        WebSocket = webSocketFeed,
                        StatusCode = webSocketFeed.State,
                        ErrorEvent = null
                    };
                }
            }

            if (webSocketFeed != null)
            {
                throw new GDAXSharpWebSocketException("Websocket can't be stopped")
                {
                    WebSocket = webSocketFeed,
                    StatusCode = webSocketFeed.State,
                    ErrorEvent = null
                };
            }

            webSocketFeed.Close();
        }

        public void Close(WebSocket4Net.WebSocket webSocket)
        {
            webSocket.Close();
        }

        public void Dispose(WebSocket4Net.WebSocket webSocket)
        {
            webSocket.Dispose();
        }


        public void Send(WebSocket4Net.WebSocket webSocket, string json)
        {
            webSocket.Send(json);
        }

        public void Open(WebSocket4Net.WebSocket webSocket)
        {
            webSocket.Open();
        }

        public event EventHandler<ErrorEventArgs> Error;
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler Closed;
        public event EventHandler Opened;
    }
}
