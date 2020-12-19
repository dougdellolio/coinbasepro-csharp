using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinbasePro.Services.Products.Models;
using CoinbasePro.Services.Products.Models.Responses;
using CoinbasePro.Services.Products.Types;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Services.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> GetSingleProductAsync(ProductType productId);

        Task<ProductsOrderBookResponse> GetProductOrderBookAsync(
            ProductType productId,
            ProductLevel productLevel = ProductLevel.One);

        Task<IList<IList<ProductTrade>>> GetTradesAsync(
            ProductType productId,
            int limit = 100,
            int numberOfPages = 0);

        Task<IList<Candle>> GetHistoricRatesAsync(
            ProductType productPair,
            DateTime start,
            DateTime end,
            CandleGranularity granularity);

        Task<ProductTicker> GetProductTickerAsync(ProductType productId);

        Task<ProductStats> GetProductStatsAsync(ProductType productId);
    }
}
