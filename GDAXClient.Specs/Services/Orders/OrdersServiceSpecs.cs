using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Orders;
using GDAXClient.Specs.JsonFixtures.Orders;
using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Specs.Services.Accounts
{
    [Subject("OrdersService")]
    public class OrdersServiceSpecs : WithSubject<OrdersService>
    {
        static OrderResponse order_response_result;

        static IList<IList<OrderResponse>> order_many_response_result;

        static CancelOrderResponse cancel_order_response_result;

        static Authenticator authenticator;

        static Exception exception;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_placing_a_market_order
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
                order_response_result = Subject.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .01M).Result;

            It should_have_correct_account_information = () =>
            {
                order_response_result.Id.ShouldEqual(new System.Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.Product_id.ShouldEqual("BTC-USD");
                order_response_result.Side.ShouldEqual("buy");
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.Type.ShouldEqual("limit");
                order_response_result.Time_in_force.ShouldEqual("GTC");
                order_response_result.Post_only.ShouldBeFalse();
                order_response_result.Created_at.ShouldEqual(new System.DateTime(2016, 12, 9));
                order_response_result.Fill_fees.ShouldEqual(0.0000000000000000M);
                order_response_result.Filled_size.ShouldEqual(0.00000000M);
                order_response_result.Executed_value.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual("pending");
                order_response_result.Settled.ShouldBeFalse();
            };
        }

        class when_placing_a_limit_order
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
                order_response_result = Subject.PlaceLimitOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .01M, 0.1M).Result;

            It should_have_correct_account_information = () =>
            {
                order_response_result.Id.ShouldEqual(new System.Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.Product_id.ShouldEqual("BTC-USD");
                order_response_result.Side.ShouldEqual("buy");
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.Type.ShouldEqual("limit");
                order_response_result.Time_in_force.ShouldEqual("GTC");
                order_response_result.Post_only.ShouldBeFalse();
                order_response_result.Created_at.ShouldEqual(new System.DateTime(2016, 12, 9));
                order_response_result.Fill_fees.ShouldEqual(0.0000000000000000M);
                order_response_result.Filled_size.ShouldEqual(0.00000000M);
                order_response_result.Executed_value.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual("pending");
                order_response_result.Settled.ShouldBeFalse();
            };
        }

        class when_cancelling_all_orders
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CancelOrderResponseFixture.Create()));
            };

            Because of = () =>
                cancel_order_response_result = Subject.CancelAllOrdersAsync().Result;

            It should_have_correct_number_of_cancelled_orders = () =>
                cancel_order_response_result.OrderIds.Count().ShouldEqual(2);

            It should_have_correct_list_of_cancelled_orders = () =>
            {
                cancel_order_response_result.OrderIds.First().ShouldEqual(new System.Guid("144c6f8e-713f-4682-8435-5280fbe8b2b4"));
                cancel_order_response_result.OrderIds.Skip(1).First().ShouldEqual(new System.Guid("debe4907-95dc-442f-af3b-cec12f42ebda"));
            };
        }

        class when_cancelling_order_by_id_with_success
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.Accepted)));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CancelOrderResponseFixture.Create()));
            };

            Because of = () =>
                cancel_order_response_result = Subject.CancelOrderByIdAsync("144c6f8e-713f-4682-8435-5280fbe8b2b4").Result;

            It should_have_correct_number_of_cancelled_orders = () =>
                cancel_order_response_result.OrderIds.Count().ShouldEqual(1);

            It should_have_correct_list_of_cancelled_orders = () =>
                cancel_order_response_result.OrderIds.First().ShouldEqual(new System.Guid("144c6f8e-713f-4682-8435-5280fbe8b2b4"));
        }

        class when_cancelling_order_by_id_with_failure
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CancelOrderResponseFixture.CreateEmpty()));
            };

            Because of = () =>
                exception = Catch.Exception(() => Subject.CancelOrderByIdAsync("144c6f8e-713f-4682-8435-5280fbe8b2b4").Result);

            It should_have_correct_error_response_message = () =>
            {
                exception.InnerException.ShouldBeOfExactType<HttpRequestException>();
                exception.InnerException.ShouldContainErrorMessage("order not found");
            };
        }

        class when_getting_all_orders
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(OrderResponseFixture.CreateMany()));
            };

            Because of = () =>
                order_many_response_result = Subject.GetAllOrdersAsync().Result;

            It should_have_correct_number_of_orders = () =>
                order_many_response_result.First().Count().ShouldEqual(2);

            It should_have_correct_orders = () =>
            {
                order_many_response_result.First().First().Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_many_response_result.First().First().Price.ShouldEqual(0.10000000M);
                order_many_response_result.First().First().Size.ShouldEqual(0.01000000M);
                order_many_response_result.First().First().Product_id.ShouldEqual("BTC-USD");
                order_many_response_result.First().First().Side.ShouldEqual("buy");
                order_many_response_result.First().First().Stp.ShouldEqual("dc");
                order_many_response_result.First().First().Type.ShouldEqual("limit");
                order_many_response_result.First().First().Time_in_force.ShouldEqual("GTC");
                order_many_response_result.First().First().Post_only.ShouldBeFalse();
                order_many_response_result.First().First().Created_at.ShouldEqual(new DateTime(2016, 12, 9));
                order_many_response_result.First().First().Fill_fees.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().First().Filled_size.ShouldEqual(0.00000000M);
                order_many_response_result.First().First().Executed_value.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().First().Status.ShouldEqual("pending");
                order_many_response_result.First().First().Settled.ShouldBeFalse();

                order_many_response_result.First().Skip(1).First().Id.ShouldEqual(new Guid("8b99b139-58f2-4ab2-8e7a-c11c846e3022"));
                order_many_response_result.First().Skip(1).First().Price.ShouldEqual(0.10000000M);
                order_many_response_result.First().Skip(1).First().Size.ShouldEqual(0.01000000M);
                order_many_response_result.First().Skip(1).First().Product_id.ShouldEqual("ETH-USD");
                order_many_response_result.First().Skip(1).First().Side.ShouldEqual("buy");
                order_many_response_result.First().Skip(1).First().Stp.ShouldEqual("dc");
                order_many_response_result.First().Skip(1).First().Type.ShouldEqual("limit");
                order_many_response_result.First().Skip(1).First().Time_in_force.ShouldEqual("GTC");
                order_many_response_result.First().Skip(1).First().Post_only.ShouldBeFalse();
                order_many_response_result.First().Skip(1).First().Created_at.ShouldEqual(new DateTime(2016, 12, 9));
                order_many_response_result.First().Skip(1).First().Fill_fees.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().Skip(1).First().Filled_size.ShouldEqual(0.00000000M);
                order_many_response_result.First().Skip(1).First().Executed_value.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().Skip(1).First().Status.ShouldEqual("pending");
                order_many_response_result.First().Skip(1).First().Settled.ShouldBeFalse();
            };
        }

        class when_getting_order_by_id
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
                order_response_result = Subject.GetOrderByIdAsync("d0c5340b-6d6c-49d9-b567-48c4bfca13d2").Result;

            It should_have_correct_order = () =>
            {
                order_response_result.Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.Product_id.ShouldEqual("BTC-USD");
                order_response_result.Side.ShouldEqual("buy");
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.Type.ShouldEqual("limit");
                order_response_result.Time_in_force.ShouldEqual("GTC");
                order_response_result.Post_only.ShouldBeFalse();
                order_response_result.Created_at.ShouldEqual(new System.DateTime(2016, 12, 9));
                order_response_result.Fill_fees.ShouldEqual(0.0000000000000000M);
                order_response_result.Filled_size.ShouldEqual(0.00000000M);
                order_response_result.Executed_value.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual("pending");
                order_response_result.Settled.ShouldBeFalse();
            };
        }
    }
}
