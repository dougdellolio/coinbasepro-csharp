using GDAXClient.Shared;
using GDAXClient.Utilities.Extensions;
using GDAXClient.WebSocketFeed.Request;
using GDAXClient.WebSocketFeed.Response;
using Newtonsoft.Json;
using System;
using System.Linq;
using WebSocketSharp;

namespace GDAXClient.WebSocket
{
    public class WebSocketFeed
    {
        public void GetTickerChannel(params ProductType[] productTypes)
        {
            if (productTypes.Length == 0)
            {
                throw new ArgumentException("You must specify at least one product type");
            }

            using (var ws = new WebSocketSharp.WebSocket("wss://ws-feed.gdax.com"))
            {
                ws.OnMessage += (sender, e) =>
                    Create(sender, e, ws);

                ws.Connect();

                var json = JsonConvert.SerializeObject(new TickerChannel
                {
                    type = "subscribe",
                    product_ids = productTypes.Select(productType => productType.ToDasherizedUpper()).ToList(),
                    channels = new[]
                    {
                        new {
                            name = "ticker",
                            product_ids = productTypes.Select(productType => productType.ToDasherizedUpper()).ToList()
                        }
                    }
                });

                ws.Send(json);
                //Console.ReadKey(true);
            }
        }

        private void Create(object sender, MessageEventArgs e, WebSocketSharp.WebSocket ws)
        {
            var lastOrder = JsonConvert.DeserializeObject<FeedOrder>(e.Data);

            OnDataReceived(sender, new WebSocketFeedEventArgs(lastOrder));
        }

        public event EventHandler<WebSocketFeedEventArgs> OnDataReceived;
    }
}
