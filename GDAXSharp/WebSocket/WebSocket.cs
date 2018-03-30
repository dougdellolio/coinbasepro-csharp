using GDAXSharp.Exceptions;
using GDAXSharp.Network;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Shared.Types;
using GDAXSharp.Shared.Utilities;
using GDAXSharp.Shared.Utilities.Clock;
using GDAXSharp.Shared.Utilities.Extensions;
using GDAXSharp.WebSocket.Models.Request;
using GDAXSharp.WebSocket.Models.Response;
using GDAXSharp.WebSocket.Types;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using WebSocket4Net;

namespace GDAXSharp.WebSocket
{
    public class WebSocket : AbstractRequest, IWebSocket
    {
        private const string ApiUri = "wss://ws-feed.gdax.com";

        private const string SandBoxApiUri = "wss://ws-feed-public.sandbox.gdax.com";

        internal WebSocket(
            IAuthenticator authenticator,
            IClock clock,
            bool sandBox = false)
                : base(authenticator, clock, sandBox)
        {
        }

        ~WebSocket()
        {
            WebSocketFeed.Close();
            WebSocketFeed.Dispose();
        }

        private bool StopWebSocket { get; set; }

        private List<ProductType> ProductTypes { get; set; }

        private List<ChannelType> ChannelTypes { get; set; }

        private WebSocket4Net.WebSocket WebSocketFeed { get; set; }

        public void Start(List<ProductType> productTypes, List<ChannelType> channelTypes = null)
        {
            if (WebSocketFeed != null && WebSocketFeed.State != WebSocketState.Closed)
            {
                throw new GDAXSharpHttpException(
                    $"Websocket needs to be in the closed state, current state is {WebSocketFeed.State}");
            }

            if (productTypes.Count == 0)
            {
                throw new ArgumentException("You must specify at least one product type");
            }

            StopWebSocket = false;

            ProductTypes = productTypes;
            ChannelTypes = channelTypes;

            var socketUrl = SandBox
                ? SandBoxApiUri
                : ApiUri;

            WebSocketFeed = new WebSocket4Net.WebSocket(socketUrl);
            WebSocketFeed.Closed += WebSocket_Closed;
            WebSocketFeed.Error += WebSocket_Error;
            WebSocketFeed.MessageReceived += WebSocket_MessageReceived;
            WebSocketFeed.Opened += WebSocket_Opened;
            WebSocketFeed.Open();
        }

        public void Stop()
        {
            if (WebSocketFeed == null || WebSocketFeed.State != WebSocketState.Open)
            {
                throw new GDAXSharpWebSocketException($"Websocket needs to be in the opened state, current state is {WebSocketFeed.State}")
                {
                    WebSocket = WebSocketFeed,
                    StatusCode = WebSocketFeed.State,
                    ErrorEvent = null
                };
            }

            if (StopWebSocket)
            {
                throw new GDAXSharpWebSocketException("Websocket can't be stopped")
                {
                    WebSocket = WebSocketFeed,
                    StatusCode = WebSocketFeed.State,
                    ErrorEvent = null
                };
            }

            StopWebSocket = true;
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            var channels = new List<Channel>();
            if (ChannelTypes == null || ChannelTypes.Count == 0)
            {
                foreach (ChannelType channelType in Enum.GetValues(typeof(ChannelType)))
                {
                    channels.Add(new Channel(channelType, ProductTypes));
                }
            }
            else
            {
                foreach (var channelType in ChannelTypes)
                {
                    channels.Add(new Channel(channelType, ProductTypes));
                }
            }

            var timeStamp = Clock.GetTime().ToTimeStamp();
            var json = JsonConfig.SerializeObject(new TickerChannel
            {
                Type = ActionType.Subscribe,
                ProductIds = ProductTypes,
                Channels = channels,
                Timestamp = timeStamp.ToString("F0", CultureInfo.InvariantCulture),
                Key = Authenticator.ApiKey,
                Passphrase = Authenticator.Passphrase,
                Signature = ComputeSignature(HttpMethod.Get, Authenticator.UnsignedSignature, timeStamp,
                    "/users/self/verify", null)
            });

            WebSocketFeed.Send(json);
        }

        private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (StopWebSocket)
            {
                WebSocketFeed.Close();
                return;
            }

            var json = e.Message;
            var response = JsonConfig.DeserializeObject<BaseMessage>(json);

            switch (response.Type)
            {
                case ResponseType.Subscriptions:
                    break;
                case ResponseType.Ticker:
                    var ticker = JsonConfig.DeserializeObject<Ticker>(json);
                    OnTickerReceived?.Invoke(sender, new WebfeedEventArgs<Ticker>(ticker));
                    break;
                case ResponseType.Snapshot:
                    var snapshot = JsonConfig.DeserializeObject<Snapshot>(json);
                    OnSnapShotReceived?.Invoke(sender, new WebfeedEventArgs<Snapshot>(snapshot));
                    break;
                case ResponseType.L2Update:
                    var level2 = JsonConfig.DeserializeObject<Level2>(json);
                    OnLevel2UpdateReceived?.Invoke(sender, new WebfeedEventArgs<Level2>(level2));
                    break;
                case ResponseType.Heartbeat:
                    var heartbeat = JsonConfig.DeserializeObject<Heartbeat>(json);
                    OnHeartbeatReceived?.Invoke(sender, new WebfeedEventArgs<Heartbeat>(heartbeat));
                    break;
                case ResponseType.Received:
                    var received = JsonConfig.DeserializeObject<Received>(json);
                    OnReceivedReceived?.Invoke(sender, new WebfeedEventArgs<Received>(received));
                    break;
                case ResponseType.Open:
                    var open = JsonConfig.DeserializeObject<Open>(json);
                    OnOpenReceived?.Invoke(sender, new WebfeedEventArgs<Open>(open));
                    break;
                case ResponseType.Done:
                    var done = JsonConfig.DeserializeObject<Done>(json);
                    OnDoneReceived?.Invoke(sender, new WebfeedEventArgs<Done>(done));
                    break;
                case ResponseType.Match:
                    var match = JsonConfig.DeserializeObject<Match>(json);
                    OnMatchReceived?.Invoke(sender, new WebfeedEventArgs<Match>(match));
                    break;
                case ResponseType.LastMatch:
                    var lastMatch = JsonConfig.DeserializeObject<LastMatch>(json);
                    OnLastMatchReceived?.Invoke(sender, new WebfeedEventArgs<LastMatch>(lastMatch));
                    break;
                case ResponseType.Change:
                    // Throw an error? Didn't receive anything with this response type
                    break;
                case ResponseType.Activate:
                    // Throw an error? Didn't receive anything with this response type
                    break;
                case ResponseType.Error:
                    var error = JsonConfig.DeserializeObject<Error>(json);
                    OnErrorReceived?.Invoke(sender, new WebfeedEventArgs<Error>(error));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            throw new GDAXSharpWebSocketException($"WebSocket Feed Error: {e.Exception.Message}")
            {
                WebSocket = WebSocketFeed,
                StatusCode = WebSocketFeed.State,
                ErrorEvent = e

            };
        }

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            if (WebSocketFeed.State != WebSocketState.Closed || StopWebSocket) return;

            WebSocketFeed.Dispose();
            Start(ProductTypes, ChannelTypes);
        }

        public event EventHandler<WebfeedEventArgs<Ticker>> OnTickerReceived;
        public event EventHandler<WebfeedEventArgs<Snapshot>> OnSnapShotReceived;
        public event EventHandler<WebfeedEventArgs<Level2>> OnLevel2UpdateReceived;
        public event EventHandler<WebfeedEventArgs<Heartbeat>> OnHeartbeatReceived;
        public event EventHandler<WebfeedEventArgs<Received>> OnReceivedReceived;
        public event EventHandler<WebfeedEventArgs<Open>> OnOpenReceived;
        public event EventHandler<WebfeedEventArgs<Done>> OnDoneReceived;
        public event EventHandler<WebfeedEventArgs<Match>> OnMatchReceived;
        public event EventHandler<WebfeedEventArgs<LastMatch>> OnLastMatchReceived;
        public event EventHandler<WebfeedEventArgs<Error>> OnErrorReceived;
    }
}
