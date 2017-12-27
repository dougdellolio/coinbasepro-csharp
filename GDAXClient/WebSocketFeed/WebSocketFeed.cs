using GDAXClient.Shared;
using GDAXClient.Utilities.Extensions;
using GDAXClient.WebSocketFeed.Request;
using Newtonsoft.Json;
using System;
using System.Linq;
using WebSocketSharp;

namespace GDAXClient.WebSocketFeed
{
    public class WebSocketFeed
    {
        public event EventHandler<MessageEventArgs> OnDataReceived;

        public void Get(params ProductType[] productTypes)
        {
            if (productTypes.Length == 0)
            {
                throw new ArgumentException("You must specify at least one product type");
            }

            using (var ws = new WebSocket("wss://ws-feed.gdax.com"))
            {
                ws.OnMessage += OnDataReceived;

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
                Console.ReadKey(true);
            }
        }
    }
}
