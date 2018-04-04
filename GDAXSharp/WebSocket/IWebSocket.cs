using GDAXSharp.Shared.Types;
using GDAXSharp.WebSocket.Models.Response;
using GDAXSharp.WebSocket.Types;
using System;
using System.Collections.Generic;

namespace GDAXSharp.WebSocket
{
    public interface IWebSocket
    {
        void Start(List<ProductType> productTypes, List<ChannelType> channelTypes = null);

        event EventHandler<WebfeedEventArgs<Ticker>> OnTickerReceived;

        event EventHandler<WebfeedEventArgs<Snapshot>> OnSnapShotReceived;

        event EventHandler<WebfeedEventArgs<Level2>> OnLevel2UpdateReceived;
    }
}
