using GDAXSharp.Shared.Types;
using GDAXSharp.WebSocket.Models.Response;
using System;

namespace GDAXSharp.WebSocket
{
    public interface IWebSocket
    {
        void GetTickerChannel(params ProductType[] productTypes);

        event EventHandler<WebfeedEventArgs<Ticker>> OnTickerReceived;
        event EventHandler<WebfeedEventArgs<Snapshot>> OnSnapShotReceived;
        event EventHandler<WebfeedEventArgs<Level2>> OnLevel2UpdateReceived;
    }
}