using GDAXClient.Authentication;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var authenticator = new Authenticator("5bddc122762c1a753b40eedc5f39223c", "PB8uIvw9HN6rb2j64yo9Al/E6/qvcSi1iWmLfWtT8Q93EwvKRSx+akDosMsv8tSdnpPOmZcX5Z0H8rylVlvSrQ==", "o5wpr6uklio");

            var gdaxClient = new GDAXClient.GDAXClient(authenticator, true);

            var callTask10 = Task.Run(() => gdaxClient.ProductsService.GetAllProductsAsync());
            callTask10.Wait();
            var r10 = callTask10;

            var callTask22 = Task.Run(() => gdaxClient.ProductsService.GetProductOrderBookAsync(GDAXClient.Services.Orders.ProductType.BtcUsd));
            callTask22.Wait();
            var r22 = callTask22;
        }
    }
}
