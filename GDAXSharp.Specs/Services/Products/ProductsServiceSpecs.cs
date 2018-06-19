using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Orders.Types;
using CoinbasePro.Services.Products;
using CoinbasePro.Services.Products.Models;
using CoinbasePro.Services.Products.Models.Responses;
using CoinbasePro.Services.Products.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Services.Products;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Products
{
    [Subject("ProductsService")]
    public class ProductsServiceSpecs : WithSubject<ProductsService>
    {
        static IEnumerable<Product> products_result;

        static ProductsOrderBookResponse product_order_books_response;

        static IList<Candle> product_history_response;

        static ProductTicker product_ticker_result;

        static ProductStats product_stats_result;

        static IList<IList<ProductTrade>> product_trades_result;

        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_getting_all_products
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsResponseFixture.Create()));

            Because of = () =>
                products_result = Subject.GetAllProductsAsync().Result;

            It should_have_correct_products_response_count = () =>
                products_result.Count().ShouldEqual(1);

            It should_have_correct_products = () =>
            {
                products_result.First().Id.ShouldEqual(ProductType.BtcUsd);
                products_result.First().BaseCurrency.ShouldEqual(Currency.BTC);
                products_result.First().QuoteCurrency.ShouldEqual(Currency.USD);
                products_result.First().BaseMinSize.ShouldEqual(0.01M);
                products_result.First().BaseMaxSize.ShouldEqual(10000.00M);
                products_result.First().QuoteIncrement.ShouldEqual(0.01M);
            };
        }

        class when_getting_a_product_order_book_for_level_one
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsOrderBookResponseFixture.Create()));

            Because of = () =>
                product_order_books_response = Subject.GetProductOrderBookAsync(ProductType.BtcUsd).Result;

            It should_have_correct_product_order_book_response = () =>
            {
                product_order_books_response.Sequence.ShouldEqual(3M);
                product_order_books_response.Bids.First().Price.ShouldEqual(200M);
                product_order_books_response.Bids.First().Size.ShouldEqual(100M);
                product_order_books_response.Bids.First().NumberOfOrders.ShouldEqual(3);
                product_order_books_response.Bids.First().OrderId.ShouldBeNull();
                product_order_books_response.Asks.First().Price.ShouldEqual(200M);
                product_order_books_response.Asks.First().Size.ShouldEqual(100M);
                product_order_books_response.Asks.First().NumberOfOrders.ShouldEqual(3);
                product_order_books_response.Asks.First().OrderId.ShouldBeNull();
            };
        }

        class when_getting_a_product_order_book_for_level_two
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsOrderBookResponseFixture.Create()));

            Because of = () =>
                product_order_books_response = Subject.GetProductOrderBookAsync(ProductType.BtcUsd, ProductLevel.Two).Result;

            It should_have_correct_product_order_book_response = () =>
            {
                product_order_books_response.Sequence.ShouldEqual(3M);
                product_order_books_response.Bids.First().Price.ShouldEqual(200M);
                product_order_books_response.Bids.First().Size.ShouldEqual(100M);
                product_order_books_response.Bids.First().NumberOfOrders.ShouldEqual(3);
                product_order_books_response.Bids.First().OrderId.ShouldBeNull();
                product_order_books_response.Asks.First().Price.ShouldEqual(200M);
                product_order_books_response.Asks.First().Size.ShouldEqual(100M);
                product_order_books_response.Asks.First().NumberOfOrders.ShouldEqual(3);
                product_order_books_response.Asks.First().OrderId.ShouldBeNull();
            };
        }

        class when_getting_a_product_order_book_with_level_3_specified
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsOrderBookResponseFixture.CreateWithLevelThree()));

            Because of = () =>
                product_order_books_response = Subject.GetProductOrderBookAsync(ProductType.BtcUsd, ProductLevel.Three).Result;

            It should_have_correct_product_order_book_response = () =>
            {
                product_order_books_response.Sequence.ShouldEqual(3M);
                product_order_books_response.Bids.First().Price.ShouldEqual(200M);
                product_order_books_response.Bids.First().Size.ShouldEqual(100M);
                product_order_books_response.Bids.First().NumberOfOrders.ShouldBeNull();
                product_order_books_response.Bids.First().OrderId.ShouldEqual(new Guid("3b0f1225-7f84-490b-a29f-0faef9de823a"));
                product_order_books_response.Asks.First().Price.ShouldEqual(200M);
                product_order_books_response.Asks.First().Size.ShouldEqual(100M);
                product_order_books_response.Asks.First().NumberOfOrders.ShouldBeNull();
                product_order_books_response.Asks.First().OrderId.ShouldEqual(new Guid("da863862-25f4-4868-ac41-005d11ab0a5f"));
            };
        }

        class when_getting_a_product_ticker
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductTickerFixture.Create()));

            Because of = () =>
                product_ticker_result = Subject.GetProductTickerAsync(ProductType.BtcUsd).Result;

            It should_have_correct_product_ticker = () =>
            {
                product_ticker_result.TradeId.ShouldEqual(4729088);
                product_ticker_result.Price.ShouldEqual(333.99M);
                product_ticker_result.Size.ShouldEqual(0.193M);
                product_ticker_result.Bid.ShouldEqual(333.98M);
                product_ticker_result.Ask.ShouldEqual(333.99M);
                product_ticker_result.Volume.ShouldEqual(5957.11914015M);
                product_ticker_result.Time.ShouldEqual(new DateTime(2016, 12, 9));
            };
        }

        class when_getting_product_stats
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductStatsFixture.Create()));

            Because of = () =>
                product_stats_result = Subject.GetProductStatsAsync(ProductType.BtcUsd).Result;

            It should_have_correct_product_stats = () =>
            {
                product_stats_result.Open.ShouldEqual(34.19000000M);
                product_stats_result.High.ShouldEqual(95.70000000M);
                product_stats_result.Low.ShouldEqual(7.06000000M);
                product_stats_result.Volume.ShouldEqual(2.41000000M);
            };
        }

        class when_getting_product_trades
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductTradesFixture.Create()));

            Because of = () =>
                product_trades_result = Subject.GetTradesAsync(ProductType.BtcUsd).Result;

            It should_have_correct_product_trades = () =>
            {
                product_trades_result.First().First().TradeId.ShouldEqual(74);
                product_trades_result.First().First().Price.ShouldEqual(10.0M);
                product_trades_result.First().First().Size.ShouldEqual(0.01M);
                product_trades_result.First().First().Side.ShouldEqual(OrderSide.Buy);

                product_trades_result.First().Skip(1).First().TradeId.ShouldEqual(73);
                product_trades_result.First().Skip(1).First().Price.ShouldEqual(100M);
                product_trades_result.First().Skip(1).First().Size.ShouldEqual(0.01M);
                product_trades_result.First().Skip(1).First().Side.ShouldEqual(OrderSide.Sell);
            };
        }

        class when_getting_product_history
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductHistoryFixture.Create()));

            Because of = () =>
                product_history_response = Subject.GetHistoricRatesAsync(ProductType.BtcUsd, DateTime.Now.AddDays(-1), DateTime.Now, CandleGranularity.Minutes1).Result;
            
            It should_have_correct_product_stats = () =>
            {
                product_history_response[0].Time.ShouldEqual(UnixEpoch.AddSeconds(1512691200));
                product_history_response[0].Low.ShouldEqual(16777M);
                product_history_response[0].High.ShouldEqual(17777.69M);
                product_history_response[0].Open.ShouldEqual(17390.01M);
                product_history_response[0].Close.ShouldEqual(17210.99M);
                product_history_response[0].Volume.ShouldEqual(7650.386033540894M);

                product_history_response[1].Time.ShouldEqual(UnixEpoch.AddSeconds(1512633600));
                product_history_response[1].Low.ShouldEqual(14487.8M);
                product_history_response[1].High.ShouldEqual(19697M);
                product_history_response[1].Open.ShouldEqual(14487.8M);
                product_history_response[1].Close.ShouldEqual(17390.01M);
                product_history_response[1].Volume.ShouldEqual(65581.82529800163M);

                product_history_response[2].Time.ShouldEqual(UnixEpoch.AddSeconds(1512576000));
                product_history_response[2].Low.ShouldEqual(13500M);
                product_history_response[2].High.ShouldEqual(14499.89M);
                product_history_response[2].Open.ShouldEqual(14056.78M);
                product_history_response[2].Close.ShouldEqual(14487.8M);
                product_history_response[2].Volume.ShouldEqual(12303.76923928093M);
            };
        }
    }
}
