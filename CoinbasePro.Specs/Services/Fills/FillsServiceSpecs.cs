using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Fills;
using CoinbasePro.Services.Fills.Models.Responses;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Network.HttpResponseMessage;
using CoinbasePro.Specs.JsonFixtures.Services.Fills;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Fills
{
    [Subject("FillsService")]
    public class FillsServiceSpecs : WithSubject<FillsService>
    {
        static IList<IList<FillResponse>> fill_response;

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(HttpResponseMessageFixture.CreateWithEmptyValue()));

        class when_requesting_all_fills
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FillsResponseFixture.Create()));

            Because of = () =>
                fill_response = Subject.GetAllFillsAsync(1).Result;

            It should_return_a_response = () =>
                fill_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fill_response.First().First().TradeId.ShouldEqual(74);
                fill_response.First().First().ProductId.ShouldEqual(ProductType.BtcUsd);
                fill_response.First().First().Price.ShouldEqual(10.00M);
                fill_response.First().First().Size.ShouldEqual(0.01M);
                fill_response.First().First().OrderId.ShouldEqual(new Guid("d50ec984-77a8-460a-b958-66f114b0de9b"));
            };
        }

        class when_requesting_fills_by_order_id
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FillsResponseFixture.Create()));

            Because of = () =>
                fill_response = Subject.GetFillsByOrderIdAsync("d50ec984-77a8-460a-b958-66f114b0de9b", 1).Result;

            It should_return_a_response = () =>
                fill_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fill_response.First().First().TradeId.ShouldEqual(74);
                fill_response.First().First().ProductId.ShouldEqual(ProductType.BtcUsd);
                fill_response.First().First().Price.ShouldEqual(10.00M);
                fill_response.First().First().Size.ShouldEqual(0.01M);
                fill_response.First().First().OrderId.ShouldEqual(new Guid("d50ec984-77a8-460a-b958-66f114b0de9b"));
            };
        }

        class when_requesting_fills_by_product_id
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(FillsResponseFixture.Create()));

            Because of = () =>
                fill_response = Subject.GetFillsByProductIdAsync(ProductType.BtcUsd, 1).Result;

            It should_return_a_response = () =>
                fill_response.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                fill_response.First().First().TradeId.ShouldEqual(74);
                fill_response.First().First().ProductId.ShouldEqual(ProductType.BtcUsd);
                fill_response.First().First().Price.ShouldEqual(10.00M);
                fill_response.First().First().Size.ShouldEqual(0.01M);
                fill_response.First().First().OrderId.ShouldEqual(new Guid("d50ec984-77a8-460a-b958-66f114b0de9b"));
            };
        }
    }
}
