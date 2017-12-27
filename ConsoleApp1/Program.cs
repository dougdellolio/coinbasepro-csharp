using GDAXClient.Shared;
using GDAXClient.WebSocketFeed;
using System;
using GDAXClient.WebSocketFeed.Response;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new WebSocketFeed();

            ws.OnDataReceived += (sender, e) =>
                ReceivedData(e.LastOrder);

            ws.GetTickerChannel(ProductType.BtcUsd);
        }

        static void ReceivedData(FeedOrder latestData)
        {
            Console.WriteLine(latestData.Last_size);
        }
    }
}
