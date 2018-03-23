using GDAXSharp.Network.Authentication;
using GDAXSharp.Shared.Types;
using GDAXSharp.Shared.Utilities.Clock;
using GDAXSharp.Shared.Utilities.Extensions;
using GDAXSharp.WebSocket.Models.Request;
using GDAXSharp.WebSocket.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace GDAXSharp.WebSocket
{
    public class WebSocket : IWebSocket
    {
        private const string ApiUri = "wss://ws-feed.gdax.com";

        private const string SandBoxApiUri = "wss://ws-feed-public.sandbox.gdax.com";

        private readonly IAuthenticator _authenticator;

        private readonly IClock _clock;

        private readonly bool _sandBox;

        private ProductType[] ProductTypes { get; set; }
        private WebSocket4Net.WebSocket WebSocketFeed { get; set; }

        public WebSocket(IAuthenticator authenticator, IClock clock, bool sandBox)
        {
            _authenticator = authenticator;
            _clock = clock;
            _sandBox = sandBox;
        }

        public void GetTickerChannel(params ProductType[] productTypes)
        {
            ProductTypes = productTypes;
            if (ProductTypes.Length == 0)
            {
                throw new ArgumentException("You must specify at least one product type");
            }

            var socketUrl = _sandBox ? SandBoxApiUri : ApiUri;

            WebSocketFeed = new WebSocket4Net.WebSocket(socketUrl);
            WebSocketFeed.Closed += WebSocket_Closed;
            WebSocketFeed.Error += WebSocket_Error;
            WebSocketFeed.MessageReceived += WebSocket_MessageReceived;
            WebSocketFeed.Opened += WebSocket_Opened;
            WebSocketFeed.Open();
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            var timeStamp = _clock.GetTime().ToTimeStamp();
            var convProductTypes = ProductTypes.Select(productType => productType.ToDasherizedUpper()).ToList();
            var json = JsonConvert.SerializeObject(new TickerChannel
            {
                Type = "subscribe",
                ProductIds = convProductTypes,
                Channels = new List<Channel>()
                {
                    new Channel()
                    {
                        Name = "ticker",
                        ProductIds = convProductTypes
                    },
                    new Channel()
                    {
                        Name = "user",
                        ProductIds = convProductTypes
                    }
                },
                Timestamp = timeStamp.ToString("F0", CultureInfo.InvariantCulture),
                Key = _authenticator.ApiKey,
                Passphrase = _authenticator.Passphrase,
                Signature = _authenticator.ComputeSignature(HttpMethod.Get, _authenticator.UnsignedSignature, timeStamp, "/users/self/verify", null)
            });

            WebSocketFeed.Send(json);
        }

        private void WebSocket_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            var json = e.Message;
            var response = JsonConvert.DeserializeObject<BaseMessage>(json);

            switch (response.Type)
            {
                case ResponseType.Subscriptions:
                    break;
                case ResponseType.Ticker:
                    var ticker = JsonConvert.DeserializeObject<Ticker>(json);
                    OnTickerReceived?.Invoke(sender, new TickerEventArgs(ticker));
                    break;
            }
        }

        private void WebSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            new NotImplementedException($"WebSocket Feed Error: {e.Exception.Message}");
        }

        private void WebSocket_Closed(object sender, EventArgs e)
        {
            new NotImplementedException("WebSocket Feed Closed");
        }

        public event EventHandler<TickerEventArgs> OnTickerReceived;
    }
}