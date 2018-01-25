using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Products;
using GDAXClient.Services.Products.Models;
using GDAXClient.Services.Products.Models.Responses;
using GDAXClient.Shared;
using GDAXClient.Specs.JsonFixtures.Products;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXClient.Specs.Services.Products
{
    [Subject("ProductsService")]
    public class ProductsServiceSpecs : WithSubject<ProductsService>
    {
        static Authenticator authenticator;

        static IEnumerable<Product> products_result;

        static ProductsOrderBookResponse product_order_books_response;

        static IEnumerable<object[]> product_history_response;

        static ProductTicker product_ticker_result;

        static ProductStats product_stats_result;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_getting_all_products
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsResponseFixture.Create()));
            };

            Because of = () =>
                products_result = Subject.GetAllProductsAsync().Result;

            It should_have_correct_products_response_count = () =>
                products_result.Count().ShouldEqual(1);

            It should_have_correct_products = () =>
            {
                products_result.First().Id.ShouldEqual("BTC-USD");
                products_result.First().Base_currency.ShouldEqual("BTC");
                products_result.First().Quote_currency.ShouldEqual("USD");
                products_result.First().Base_min_size.ShouldEqual("0.01");
                products_result.First().Base_max_size.ShouldEqual("10000.00");
                products_result.First().Quote_increment.ShouldEqual("0.01");
            };
        }

        class when_getting_a_product_order_book_for_level_one
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsOrderBookResponseFixture.Create()));
            };

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
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsOrderBookResponseFixture.Create()));
            };

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
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductsOrderBookResponseFixture.CreateWithLevelThree()));
            };

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
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductTickerFixture.Create()));
            };

            Because of = () =>
                product_ticker_result = Subject.GetProductTickerAsync(ProductType.BtcUsd).Result;

            It should_have_correct_product_ticker = () =>
            {
                product_ticker_result.Trade_id.ShouldEqual(4729088);
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
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductStatsFixture.Create()));
            };

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

        class when_getting_product_history
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProductHistoryFixture.Create()));
            };

            Because of = () =>
                product_history_response = Subject.GetHistoricRatesAsync(ProductType.BtcUsd, DateTime.Now.AddDays(-1), DateTime.Now, 57600).Result;

            It should_have_correct_product_stats = () =>
            {
                product_history_response.ToList()[0][0].ToString().ShouldEqual(1512691200.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[0][1].ToString().ShouldEqual(16777.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[0][2].ToString().ShouldEqual(17777.69.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[0][3].ToString().ShouldEqual(17390.01.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[0][4].ToString().ShouldEqual(17210.99.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[0][5].ToString().ShouldEqual(7650.386033540894.ToString(CultureInfo.InvariantCulture));

                product_history_response.ToList()[1][0].ToString().ShouldEqual(1512633600.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[1][1].ToString().ShouldEqual(14487.8.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[1][2].ToString().ShouldEqual(19697.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[1][3].ToString().ShouldEqual(14487.8.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[1][4].ToString().ShouldEqual(17390.01.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[1][5].ToString().ShouldEqual(65581.82529800163.ToString(CultureInfo.InvariantCulture));

                product_history_response.ToList()[2][0].ToString().ShouldEqual(1512576000.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[2][1].ToString().ShouldEqual(13500.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[2][2].ToString().ShouldEqual(14499.89.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[2][3].ToString().ShouldEqual(14056.78.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[2][4].ToString().ShouldEqual(14487.8.ToString(CultureInfo.InvariantCulture));
                product_history_response.ToList()[2][5].ToString().ShouldEqual(12303.76923928093.ToString(CultureInfo.InvariantCulture));
            };
        }
    }
}
