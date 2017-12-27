using GDAXClient.Shared;
using GDAXClient.WebSocketFeed;
using System;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = new WebSocketFeed();

            ws.OnDataReceived += (sender, e) =>
                            ReceivedData(e.Data);


            ws.Get(ProductType.BtcUsd);
        }

        static void ReceivedData(string latestData)
        {
            // data which received by library....
            Console.WriteLine(latestData);
            //Console.ReadKey(true);
        }
    }
}
