using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Specs.JsonFixtures.Orders;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Specs.Services.Accounts
{
    [Subject("OrdersService")]
    public class OrdersServiceSpecs : WithSubject<OrdersService>
    {
        static OrderResponse result;

        static Authenticator authenticator;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_placing_an_order
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(OrderResponseFixture.Create()));
            };

            Because of = () =>
                result = Subject.PlaceOrderAsync(OrderSide.Buy, ProductType.BtcUsd, "market", .01M, .01M).Result;

            It should_have_correct_account_information = () =>
            {
                result.Id.ShouldEqual(new System.Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                result.Price.ShouldEqual(0.10000000M);
                result.Size.ShouldEqual(0.01000000M);
                result.Product_id.ShouldEqual("BTC-USD");
                result.Side.ShouldEqual("buy");
                result.Stp.ShouldEqual("dc");
                result.Type.ShouldEqual("limit");
                result.Time_in_force.ShouldEqual("GTC");
                result.Post_only.ShouldBeFalse();
                result.Created_at.ShouldEqual(new System.DateTime(2016, 12, 9));
                result.Fill_fees.ShouldEqual(0.0000000000000000M);
                result.Filled_size.ShouldEqual(0.00000000M);
                result.Executed_value.ShouldEqual(0.0000000000000000M);
                result.Status.ShouldEqual("pending");
                result.Settled.ShouldBeFalse();
            };
        }
    }
}
