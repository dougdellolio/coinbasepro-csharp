using GDAXSharp.Network.Authentication;
using GDAXSharp.Shared.Types;
using GDAXSharp.Shared.Utilities.Clock;
using GDAXSharp.Shared.Utilities.Extensions;
using GDAXSharp.WebSocket.Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace GDAXSharp.WebSocket
{
    public class WebSocket : IWebSocket
    {
        private const string ApiUri = "wss://ws-feed.gdax.com";

        private const string SandBoxApiUri = "wss://ws-feed-public.sandbox.gdax.com";

        private readonly IAuthenticator authenticator;

        private readonly IClock clock;

        private readonly bool sandBox;

        private ProductType[] ProductTypes { get; set; }
        private WebSocket4Net.WebSocket WebSocketFeed { get; set; }

        public WebSocket(IAuthenticator authenticator, IClock clock, bool sandBox)
        {
            this.authenticator = authenticator;
            this.clock = clock;
            this.sandBox = sandBox;
        }

        public void GetTickerChannel(params ProductType[] productTypes)
        {
            ProductTypes = productTypes;
            if (ProductTypes.Length == 0)
            {
                throw new ArgumentException("You must specify at least one product type");
            }

            var socketUrl = sandBox
                ? SandBoxApiUri
                : ApiUri;

            WebSocketFeed = new WebSocket4Net.WebSocket(socketUrl);
            WebSocketFeed.Closed += WebSocket_Closed;
            WebSocketFeed.Error += WebSocket_Error;
            WebSocketFeed.MessageReceived += WebSocket_MessageReceived;
            WebSocketFeed.Opened += WebSocket_Opened;
            WebSocketFeed.Open();
        }

        private void WebSocket_Opened(object sender, EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] WebSocket Opened");
            Console.ResetColor();

            var timeStamp = clock.GetTime().ToTimeStamp();
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
                Key = authenticator.ApiKey,
                Passphrase = authenticator.Passphrase,
                Signature = authenticator.ComputeSignature(HttpMethod.Get, authenticator.UnsignedSignature, timeStamp, "/users/self/verify", null)
            });

            WebSocketFeed.Send(json);
        }

        private void WebSocket_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "GDAX");

            if (e.Message.Contains("ticker"))
            {
                path = Path.Combine(path, $"ticker_{DateTime.Now:yyyy-MM-dd HH}.txt");

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Message: {e.Message}");
                Console.ResetColor();

            }
            else if (e.Message.Contains("l2update"))
            {
                path = Path.Combine(path, $"level2_{DateTime.Now:yyyy-MM-dd HH}.txt");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Message: {e.Message}");
                Console.ResetColor();
            }
            else if (e.Message.Contains("match"))
            {
                path = Path.Combine(path, $"match_{DateTime.Now:yyyy-MM-dd HH}.txt");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Message: {e.Message}");
                Console.ResetColor();
            }

            else if (e.Message.Contains("snapshot"))
            {
                path = Path.Combine(path, $"snapshot_{DateTime.Now:yyyy-MM-dd HH}.txt");

                Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Message: {e.Message}");
            }
            else
            {
                path = Path.Combine(path, $"default_{DateTime.Now:yyyy-MM-dd HH}.txt");
            }

            using (var fileStream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] Message: {e.Message}");
            }
        }

        private void WebSocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] WebSocket error: {e.Exception.Message}");
            Console.ResetColor();
        }

        private void WebSocket_Closed(object sender, System.EventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] WebSocket Closed");
            Console.ResetColor();
        }
    }
}