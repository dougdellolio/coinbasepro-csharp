using System;
using System.Collections.Generic;
using CoinbasePro.Shared.Types;
using CoinbasePro.WebSocket.Models.Response;
using CoinbasePro.WebSocket.Types;
using SuperSocket.ClientEngine;
using WebSocket4Net;

namespace CoinbasePro.WebSocket
{
    public interface IWebSocket
    {
        WebSocketState State { get; }

        void ChangeChannels(List<ChannelType> channels);
        void Start(List<ProductType> productTypes, List<ChannelType> channelTypes = null, int? autoSendPingInterval = null);
        void Stop();

        void WebSocket_Opened(object sender, EventArgs e);
        void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e);
        void WebSocket_Error(object sender, ErrorEventArgs e);
        void WebSocket_Closed(object sender, EventArgs e);

        event EventHandler<WebfeedEventArgs<Ticker>> OnTickerReceived;
        event EventHandler<WebfeedEventArgs<Subscription>> OnSubscriptionReceived;
        event EventHandler<WebfeedEventArgs<Snapshot>> OnSnapShotReceived;
        event EventHandler<WebfeedEventArgs<Level2>> OnLevel2UpdateReceived;
        event EventHandler<WebfeedEventArgs<Heartbeat>> OnHeartbeatReceived;
        event EventHandler<WebfeedEventArgs<Received>> OnReceivedReceived;
        event EventHandler<WebfeedEventArgs<Open>> OnOpenReceived;
        event EventHandler<WebfeedEventArgs<Change>> OnChangeReceived;
        event EventHandler<WebfeedEventArgs<Activate>> OnActivateReceived;
        event EventHandler<WebfeedEventArgs<Done>> OnDoneReceived;
        event EventHandler<WebfeedEventArgs<Match>> OnMatchReceived;
        event EventHandler<WebfeedEventArgs<LastMatch>> OnLastMatchReceived;
        event EventHandler<WebfeedEventArgs<Error>> OnErrorReceived;
        event EventHandler<WebfeedEventArgs<ErrorEventArgs>> OnWebSocketError;
        event EventHandler<WebfeedEventArgs<EventArgs>> OnWebSocketClose;
        event EventHandler<WebfeedEventArgs<EventArgs>> OnWebSocketOpenAndSubscribed;
    }
}
