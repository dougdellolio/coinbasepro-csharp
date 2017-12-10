using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Products;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Services.Products;
using GDAXClient.Services.Products.Models;
using GDAXClient.Services.Products.Models.Responses;
using GDAXClient.Specs.JsonFixtures.Products;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Specs.Services.Payments
{
    [Subject("ProductsService")]
    public class ProductsServiceSpecs : WithSubject<ProductsService>
    {
        static Authenticator authenticator;

        static IEnumerable<Product> products_result;

        static ProductsOrderBookResponse product_order_books_response;

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

        class when_getting_a_product_order_book
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
                product_order_books_response.Bids.SelectMany(p => p).ShouldContain(200);
                product_order_books_response.Bids.SelectMany(p => p).ShouldContain(100);
                product_order_books_response.Bids.SelectMany(p => p).ShouldContain(3);
                product_order_books_response.Asks.SelectMany(p => p).ShouldContain(200);
                product_order_books_response.Asks.SelectMany(p => p).ShouldContain(100);
                product_order_books_response.Asks.SelectMany(p => p).ShouldContain(3);
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
                product_ticker_result.Time.ShouldEqual(new System.DateTime(2016, 12, 9));
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
    }
}
