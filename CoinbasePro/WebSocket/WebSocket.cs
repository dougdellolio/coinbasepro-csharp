using CoinbasePro.Shared.Utilities.Extensions;
using Serilog;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using CoinbasePro.Exceptions;
using CoinbasePro.Network.Authentication;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities;
using CoinbasePro.Shared.Utilities.Clock;
using CoinbasePro.WebSocket.Models.Request;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using WebSocket4Net;

namespace CoinbasePro.WebSocket
{
    public class WebSocket
    {
        private readonly Func<IWebSocketFeed> createWebSocketFeed;

        private readonly IAuthenticator authenticator;

        private readonly IClock clock;

        private bool stopWebSocket;

        private List<ProductType> productTypes;

        private List<ChannelType> channelTypes;

        private IWebSocketFeed webSocketFeed;

        public WebSocketState State => webSocketFeed.State;

        public WebSocket(
            Func<IWebSocketFeed> createWebSocketFeed,
            IAuthenticator authenticator,
            IClock clock)
        {
            this.createWebSocketFeed = createWebSocketFeed;
            this.authenticator = authenticator;
            this.clock = clock;
        }

        ~WebSocket()
        {
            webSocketFeed?.Close();
            webSocketFeed?.Dispose();
        }

        public void Start(
            List<ProductType> productTypes,
            List<ChannelType> channelTypes = null)
        {
            if (productTypes.Count == 0)
            {
                throw new ArgumentException($"You must specify at least one {nameof(productTypes)}");
            }

            stopWebSocket = false;

            this.productTypes = productTypes;
            this.channelTypes = channelTypes;

            webSocketFeed = createWebSocketFeed();
            webSocketFeed.Closed += WebSocket_Closed;
            webSocketFeed.Error += WebSocket_Error;
            webSocketFeed.MessageReceived += WebSocket_MessageReceived;
            webSocketFeed.Opened += WebSocket_Opened;
            webSocketFeed.Open();

            Log.Information("WebSocket started with {@ProductTypes} {@ChannelTypes}", this.productTypes, this.channelTypes);
        }

        public void Stop()
        {
            if (webSocketFeed.State != WebSocketState.Open)
            {
                throw new CoinbaseProWebSocketException(
                    $"Websocket needs to be in the opened state. The current state is {webSocketFeed.State}")
                {
                    WebSocketFeed = webSocketFeed,
                    StatusCode = webSocketFeed.State,
                    ErrorEvent = null
                };
            }

            stopWebSocket = true;

            var json = JsonConfig.SerializeObject(new TickerChannel
            {
                Type = ActionType.Unsubscribe
            });

            webSocketFeed.Send(json);
            webSocketFeed.Close();

            Log.Information("WebSocket stopped");
        }

        public void ChangeChannels(List<ChannelType> channels)
        {
            if (channelTypes == null || !channelTypes.Any())
            {
                throw new ArgumentException($"You must specify at least one {nameof(channelTypes)}");
            }

            var json = JsonConfig.SerializeObject(new
            {
                type = "unsubscribe",
                channels = channelTypes.Select(x => x.ToString().ToLower()).ToArray()
            });

            channelTypes = channels;

            webSocketFeed.Send(json);
        }

        public void WebSocket_Opened(object sender, EventArgs e)
        {
            if (productTypes.Count == 0)
            {
                throw new ArgumentException($"You must specify at least one {nameof(productTypes)}");
            }

            Subscribe();

            webSocketFeed.Invoke(OnWebSocketOpenAndSubscribed, sender, new WebfeedEventArgs<EventArgs>(e));
        }

        private void Subscribe()
        {
            var channels = GetChannels();

            var timeStamp = clock.GetTime().ToTimeStamp();

            var withAuthentication = authenticator != null;

            var json = SubscribeMessage(withAuthentication, channels, timeStamp);

            webSocketFeed.Send(json);
        }

        public void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (stopWebSocket)
            {
                return;
            }

            var json = e.Message;
            if (!json.TryDeserializeObject<BaseMessage>(out var response))
            {
                Log.Error("Could not deserialize response because the type doesn't exist {@ResponseJson}.", json);
            }

            switch (response?.Type)
            {
                case ResponseType.Subscriptions:
                    var subscription = JsonConfig.DeserializeObject<Subscription>(json);
                    if (subscription.Channels == null || !subscription.Channels.Any())
                    {
                        Subscribe();
                    }
                    else
                    {
                        webSocketFeed.Invoke(OnSubscriptionReceived, sender, new WebfeedEventArgs<Subscription>(subscription));
                    }
                    break;
                case ResponseType.Ticker:
                    var ticker = JsonConfig.DeserializeObject<Ticker>(json);
                    webSocketFeed.Invoke(OnTickerReceived, sender, new WebfeedEventArgs<Ticker>(ticker));
                    break;
                case ResponseType.Snapshot:
                    var snapshot = JsonConfig.DeserializeObject<Snapshot>(json);
                    webSocketFeed.Invoke(OnSnapShotReceived, sender, new WebfeedEventArgs<Snapshot>(snapshot));
                    break;
                case ResponseType.L2Update:
                    var level2 = JsonConfig.DeserializeObject<Level2>(json);
                    webSocketFeed.Invoke(OnLevel2UpdateReceived, sender, new WebfeedEventArgs<Level2>(level2));
                    break;
                case ResponseType.Heartbeat:
                    var heartbeat = JsonConfig.DeserializeObject<Heartbeat>(json);
                    webSocketFeed.Invoke(OnHeartbeatReceived, sender, new WebfeedEventArgs<Heartbeat>(heartbeat));
                    break;
                case ResponseType.Received:
                    var received = JsonConfig.DeserializeObject<Received>(json);
                    webSocketFeed.Invoke(OnReceivedReceived, sender, new WebfeedEventArgs<Received>(received));
                    break;
                case ResponseType.Open:
                    var open = JsonConfig.DeserializeObject<Open>(json);
                    webSocketFeed.Invoke(OnOpenReceived, sender, new WebfeedEventArgs<Open>(open));
                    break;
                case ResponseType.Done:
                    var done = JsonConfig.DeserializeObject<Done>(json);
                    webSocketFeed.Invoke(OnDoneReceived, sender, new WebfeedEventArgs<Done>(done));
                    break;
                case ResponseType.Match:
                    var match = JsonConfig.DeserializeObject<Match>(json);
                    webSocketFeed.Invoke(OnMatchReceived, sender, new WebfeedEventArgs<Match>(match));
                    break;
                case ResponseType.LastMatch:
                    var lastMatch = JsonConfig.DeserializeObject<LastMatch>(json);
                    webSocketFeed.Invoke(OnLastMatchReceived, sender, new WebfeedEventArgs<LastMatch>(lastMatch));
                    break;
                case ResponseType.Error:
                    var error = JsonConfig.DeserializeObject<Error>(json);
                    webSocketFeed.Invoke(OnErrorReceived, sender, new WebfeedEventArgs<Error>(error));
                    break;
                case ResponseType.Change:
                    var change = JsonConfig.DeserializeObject<Change>(json);
                    webSocketFeed.Invoke(OnChangeReceived, sender, new WebfeedEventArgs<Change>(change));
                    break;
                case ResponseType.Activate:
                    var activate = JsonConfig.DeserializeObject<Activate>(json);
                    webSocketFeed.Invoke(OnActivateReceived, sender, new WebfeedEventArgs<Activate>(activate));
                    break;
                default:
                    Log.Error("Unknown ResponseType {@ResponseJson}. Ignoring message received.", json);
                    break;
            }
        }

        public void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            if (OnWebSocketError != null)
            {
                webSocketFeed.Invoke(OnWebSocketError, sender, new WebfeedEventArgs<ErrorEventArgs>(e));
            }
            else
            {
                throw new CoinbaseProWebSocketException($"WebSocket Feed Error: {e.Exception.Message}")
                {
                    WebSocketFeed = webSocketFeed,
                    StatusCode = webSocketFeed.State,
                    ErrorEvent = e
                };
            }
        }

        public void WebSocket_Closed(object sender, EventArgs e)
        {
            webSocketFeed.Invoke(OnWebSocketClose, sender, new WebfeedEventArgs<EventArgs>(e));

            webSocketFeed.Dispose();

            if (!stopWebSocket)
            {
                Start(productTypes, channelTypes);
            }
        }

        private List<Channel> GetChannels()
        {
            var channels = new List<Channel>();

            if (channelTypes == null || !channelTypes.Any())
            {
                foreach (ChannelType channelType in Enum.GetValues(typeof(ChannelType)))
                {
                    channels.Add(new Channel(channelType, productTypes));
                }
            }
            else
            {
                foreach (var channelType in channelTypes)
                {
                    channels.Add(new Channel(channelType, productTypes));
                }
            }

            return channels;
        }

        private string SubscribeMessage(bool withAuthentication, List<Channel> channels, double timeStamp)
        {
            if (withAuthentication)
            {
                return JsonConfig.SerializeObject(new TickerChannel
                {
                    Type = ActionType.Subscribe,
                    ProductIds = productTypes,
                    Channels = channels,
                    Timestamp = timeStamp.ToString("F0", CultureInfo.InvariantCulture),
                    Key = authenticator.ApiKey,
                    Passphrase = authenticator.Passphrase,
                    Signature = authenticator.ComputeSignature(HttpMethod.Get, authenticator.UnsignedSignature, timeStamp,
                        "/users/self/verify")
                });
            }

            return JsonConfig.SerializeObject(new TickerChannel
            {
                Type = ActionType.Subscribe,
                ProductIds = productTypes,
                Channels = channels,
                Timestamp = timeStamp.ToString("F0", CultureInfo.InvariantCulture),
            });
        }

        public event EventHandler<WebfeedEventArgs<Ticker>> OnTickerReceived;
        public event EventHandler<WebfeedEventArgs<Subscription>> OnSubscriptionReceived;
        public event EventHandler<WebfeedEventArgs<Snapshot>> OnSnapShotReceived;
        public event EventHandler<WebfeedEventArgs<Level2>> OnLevel2UpdateReceived;
        public event EventHandler<WebfeedEventArgs<Heartbeat>> OnHeartbeatReceived;
        public event EventHandler<WebfeedEventArgs<Received>> OnReceivedReceived;
        public event EventHandler<WebfeedEventArgs<Open>> OnOpenReceived;
        public event EventHandler<WebfeedEventArgs<Change>> OnChangeReceived;
        public event EventHandler<WebfeedEventArgs<Activate>> OnActivateReceived;
        public event EventHandler<WebfeedEventArgs<Done>> OnDoneReceived;
        public event EventHandler<WebfeedEventArgs<Match>> OnMatchReceived;
        public event EventHandler<WebfeedEventArgs<LastMatch>> OnLastMatchReceived;
        public event EventHandler<WebfeedEventArgs<Error>> OnErrorReceived;
        public event EventHandler<WebfeedEventArgs<ErrorEventArgs>> OnWebSocketError;
        public event EventHandler<WebfeedEventArgs<EventArgs>> OnWebSocketClose;
        public event EventHandler<WebfeedEventArgs<EventArgs>> OnWebSocketOpenAndSubscribed;
    }
}
