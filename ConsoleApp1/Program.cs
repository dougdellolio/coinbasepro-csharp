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
            var authenticator = new Authenticator("1b6bc65c7e937a318987b9d60896a793", "9mdS5C/1aUhdIB9LPTod6vxdy3dQUHGI044d6qxcc9qOmJonq0kOk2+fh6Gqq8fDpHMZOP/ZRevmuVOm1mGq8Q==", "gfysa7mom4w");
            var gdaxClient = new GDAXClient.GDAXClient(authenticator, true);

            //var callTask = Task.Run(() => gdaxClient.AccountsService.GetAllAccountsAsync());
            //callTask.Wait();
            //var r = callTask;

            //var callTask2 = Task.Run(() => gdaxClient.AccountsService.GetAccountByIdAsync("73c8b43d-ff27-47b3-b2cb-5f4a198a6d7d"));
            //callTask2.Wait();
            //var r2 = callTask2;

            var callTask3 = Task.Run(() => gdaxClient.OrdersService.PlaceLimitOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .1M, 1M));
            callTask3.Wait();
            var r3 = callTask3;

            var guidToCancel = r3.Result.Id;

            //var callTask5 = Task.Run(() => gdaxClient.OrdersService.CancelOrderById(guidToCancel.ToString()));
            //callTask5.Wait();
            //var r5 = callTask5;

            //var callTask4 = Task.Run(() => gdaxClient.OrdersService.CancelAllOrdersAsync());
            //callTask4.Wait();
            //var r4 = callTask4;

            var callTask6 = Task.Run(() => gdaxClient.OrdersService.GetOrderByIdAsync(guidToCancel.ToString()));
            callTask6.Wait();
            var r6 = callTask6;
        }
    }
}
