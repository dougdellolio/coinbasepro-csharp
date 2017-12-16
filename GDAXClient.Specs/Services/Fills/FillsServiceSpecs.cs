using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using Machine.Fakes;
using Machine.Specifications;
using GDAXClient.Services.Fills;
using GDAXClient.Services.Fills.Models.Responses;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Specs.JsonFixtures.Fills;
using GDAXClient.Specs.JsonFixtures.HttpResponseMessage;
using GDAXClient.Utilities.Extensions;

namespace GDAXClient.Specs.Services
{
    [Subject("FillsService")]
    public class FillsServiceSpecs : WithSubject<FillsService>
    {
        static Authenticator authenticator;

        static IList<IList<FillResponse>> fill_response;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_requesting_all_fills
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FillsResponseFixture.Create()));
            };

            Because of = () =>
                fill_response = Subject.GetAllFillsAsync(1).Result;

            It should_return_a_response = () =>
                fill_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fill_response.First().First().Trade_id.ShouldEqual(74);
                fill_response.First().First().Product_id.ShouldEqual(ProductType.BtcUsd.ToDasherizedUpper());
                fill_response.First().First().Price.ShouldEqual(10.00M);
                fill_response.First().First().Size.ShouldEqual(0.01M);
                fill_response.First().First().Order_id.ShouldEqual(new Guid("d50ec984-77a8-460a-b958-66f114b0de9b"));
            };
        }

        class when_requesting_fills_by_order_id
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FillsResponseFixture.Create()));
            };

            Because of = () =>
                fill_response = Subject.GetFillsByOrderIdAsync("d50ec984-77a8-460a-b958-66f114b0de9b", 1).Result;

            It should_return_a_response = () =>
                fill_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fill_response.First().First().Trade_id.ShouldEqual(74);
                fill_response.First().First().Product_id.ShouldEqual(ProductType.BtcUsd.ToDasherizedUpper());
                fill_response.First().First().Price.ShouldEqual(10.00M);
                fill_response.First().First().Size.ShouldEqual(0.01M);
                fill_response.First().First().Order_id.ShouldEqual(new Guid("d50ec984-77a8-460a-b958-66f114b0de9b"));
            };
        }

        class when_requesting_fills_by_product_id
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FillsResponseFixture.Create()));
            };

            Because of = () =>
                fill_response = Subject.GetFillsByProductIdAsync(ProductType.BtcUsd, 1).Result;

            It should_return_a_response = () =>
                fill_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fill_response.First().First().Trade_id.ShouldEqual(74);
                fill_response.First().First().Product_id.ShouldEqual(ProductType.BtcUsd.ToDasherizedUpper());
                fill_response.First().First().Price.ShouldEqual(10.00M);
                fill_response.First().First().Size.ShouldEqual(0.01M);
                fill_response.First().First().Order_id.ShouldEqual(new Guid("d50ec984-77a8-460a-b958-66f114b0de9b"));
            };
        }
    }
}
