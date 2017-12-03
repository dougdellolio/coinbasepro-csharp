using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Products;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Services.Products;
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

        static IEnumerable<Product> products_response;

        static ProductsOrderBookResponse product_order_books_response;

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
                products_response = Subject.GetAllProductsAsync().Result;

            It should_have_correct_products_response_count = () =>
                products_response.Count().ShouldEqual(1);

            It should_have_correct_products = () =>
            {
                products_response.First().Id.ShouldEqual("BTC-USD");
                products_response.First().Base_currency.ShouldEqual("BTC");
                products_response.First().Quote_currency.ShouldEqual("USD");
                products_response.First().Base_min_size.ShouldEqual("0.01");
                products_response.First().Base_max_size.ShouldEqual("10000.00");
                products_response.First().Quote_increment.ShouldEqual("0.01");
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
                product_order_books_response.Ask.SelectMany(p => p).ShouldContain(200);
                product_order_books_response.Ask.SelectMany(p => p).ShouldContain(100);
                product_order_books_response.Ask.SelectMany(p => p).ShouldContain(3);
            };
        }
    }
}
