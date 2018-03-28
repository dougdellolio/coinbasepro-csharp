using GDAXSharp.Network;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Shared.Types;
using GDAXSharp.Shared.Utilities.Clock;
using GDAXSharp.Shared.Utilities.Extensions;
using GDAXSharp.WebSocket.Models.Request;
using GDAXSharp.WebSocket.Models.Response;
using GDAXSharp.WebSocket.Types;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using WebSocket4Net;

namespace GDAXSharp.WebSocket
{
    public class WebSocket : AbstractRequest, IWebSocket
    {
        private const string ApiUri = "wss://ws-feed.gdax.com";

        private const string SandBoxApiUri = "wss://ws-feed-public.sandbox.gdax.com";

        private CancellationToken CancellationToken { get; set; }
        private ProductType[] ProductTypes { get; set; }
        private WebSocket4Net.WebSocket WebSocketFeed { get; set; }

        public WebSocket(IAuthenticator authenticator, IClock clock, CancellationToken cancellationToken, bool sandBox = false) : base(authenticator, clock, sandBox)
        {
            CancellationToken = cancellationToken;
        }

        public void GetTickerChannel(params ProductType[] productTypes)
        {
            ProductTypes = productTypes;
            if (ProductTypes.Length == 0)
            {
                throw new ArgumentException("You must specify at least one product type");
            }

            var socketUrl = SandBox ? SandBoxApiUri : ApiUri;

            WebSocketFeed = new WebSocket4Net.WebSocket(socketUrl);
            WebSocketFeed.Closed += WebSocket_Closed;
            WebSocketFeed.Error += WebSocket_Error;
            WebSocketFeed.MessageReceived += WebSocket_MessageReceived;
            WebSocketFeed.Opened += WebSocket_Opened;
            WebSocketFeed.Open();
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            var timeStamp = Clock.GetTime().ToTimeStamp();
            var json = JsonConvert.SerializeObject(new TickerChannel
            {
                Type = "subscribe",
                ProductIds = ProductTypes,
                Channels = new List<Channel>
                {
                    new Channel
                    {
                        Name = ChannelType.Ticker,
                        ProductIds = ProductTypes
                    },
                    new Channel
                    {
                        Name = ChannelType.Level2,
                        ProductIds = ProductTypes
                    },
                    new Channel
                    {
                        Name = ChannelType.User,
                        ProductIds = ProductTypes
                    }
                },
                Timestamp = timeStamp.ToString("F0", CultureInfo.InvariantCulture),
                Key = Authenticator.ApiKey,
                Passphrase = Authenticator.Passphrase,
                Signature = ComputeSignature(HttpMethod.Get, Authenticator.UnsignedSignature, timeStamp, "/users/self/verify", null)
            });

            WebSocketFeed.Send(json);
        }

        private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (CancellationToken.IsCancellationRequested)
            {
                WebSocketFeed.Close();
                return;
            }

            var json = e.Message;
            var response = JsonConvert.DeserializeObject<BaseMessage>(json);

            switch (response.Type)
            {
                case ResponseType.Subscriptions:
                    // TODO: Implement this and more?
                    break;
                case ResponseType.Ticker:
                    var ticker = JsonConvert.DeserializeObject<Ticker>(json);
                    OnTickerReceived?.Invoke(sender, new WebfeedEventArgs<Ticker>(ticker));
                    break;
                case ResponseType.Snapshot:
                    var snapshot = JsonConvert.DeserializeObject<Snapshot>(json);
                    OnSnapShotReceived?.Invoke(sender, new WebfeedEventArgs<Snapshot>(snapshot));
                    break;
                case ResponseType.L2Update:
                    var level2 = JsonConvert.DeserializeObject<Level2>(json);
                    OnLevel2UpdateReceived?.Invoke(sender, new WebfeedEventArgs<Level2>(level2));
                    break;
            }
        }

        private void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            new Exception($"WebSocket Feed Error: {e.Exception.Message}");
        }

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            if (WebSocketFeed.State != WebSocketState.Closed || CancellationToken.IsCancellationRequested) return;

            WebSocketFeed.Dispose();
            GetTickerChannel(ProductTypes);
        }

        public event EventHandler<WebfeedEventArgs<Ticker>> OnTickerReceived;
        public event EventHandler<WebfeedEventArgs<Snapshot>> OnSnapShotReceived;
        public event EventHandler<WebfeedEventArgs<Level2>> OnLevel2UpdateReceived;
    }
}