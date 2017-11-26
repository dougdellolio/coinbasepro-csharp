using GDAXClient.Authentication;
using GDAXClient.Services.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var authenticator = new Authenticator("5bddc122762c1a753b40eedc5f39223c", "PB8uIvw9HN6rb2j64yo9Al/E6/qvcSi1iWmLfWtT8Q93EwvKRSx+akDosMsv8tSdnpPOmZcX5Z0H8rylVlvSrQ==", "o5wpr6uklio");

            var gdaxClient = new GDAXClient.GDAXClient(authenticator, true);

            var callTask = Task.Run(() => gdaxClient.AccountsService.GetAllAccountsAsync());
            callTask.Wait();
            var r = callTask;

            var callTask2 = Task.Run(() => gdaxClient.AccountsService.GetAccountByIdAsync("73c8b43d-ff27-47b3-b2cb-5f4a198a6d7d"));
            callTask2.Wait();
            var r2 = callTask2;

            //var callTask3 = Task.Run(() => gdaxClient.OrdersService.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .5M));
            //callTask3.Wait();
            //var r3 = callTask3;

            //var callTask4 = Task.Run(() => gdaxClient.OrdersService.PlaceLimitOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .5M, 100));
            //callTask4.Wait();
            //var r4 = callTask4;

            //var callTask5 = Task.Run(() => gdaxClient.OrdersService.GetAllOrdersAsync());
            //callTask5.Wait();
            //var r5 = callTask5;

            //var firstId = r5.Result.First().Id;

            //var callTask6 = Task.Run(() => gdaxClient.OrdersService.GetOrderByIdAsync(firstId.ToString()));
            //callTask6.Wait();
            //var r6 = callTask6;

            //var callTask7 = Task.Run(() => gdaxClient.OrdersService.CancelOrderByIdAsync(firstId.ToString()));
            //callTask7.Wait();
            //var r7 = callTask7;

            var callTask8 = Task.Run(() => gdaxClient.WithdrawalsService.WithdrawFundsAsync("e49c8d15-547b-464e-ac3d-4b9d20b360ec", 1000, GDAXClient.Services.Currency.USD));
            callTask8.Wait();
            var r8 = callTask8;
        }
    }
}
