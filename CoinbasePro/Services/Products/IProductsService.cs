using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Products.Models;
using CoinbasePro.Services.Products.Models.Responses;
using CoinbasePro.Services.Products.Types;

namespace CoinbasePro.Services.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetSingleProductAsync(string productId);

        Task<ProductsOrderBookResponse> GetProductOrderBookAsync(
            string productId,
            ProductLevel productLevel = ProductLevel.One);

        Task<IList<IList<ProductTrade>>> GetTradesAsync(
            string productId,
            int limit = 100,
            int numberOfPages = 0);

        Task<IList<Candle>> GetHistoricRatesAsync(
            string productPair,
            DateTime start,
            DateTime end,
            CandleGranularity granularity);

        Task<ProductTicker> GetProductTickerAsync(string productId);

        Task<ProductStats> GetProductStatsAsync(string productId);
    }
}
