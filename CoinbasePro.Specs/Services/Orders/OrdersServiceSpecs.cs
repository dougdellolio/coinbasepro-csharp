using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Exceptions;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Orders;
using CoinbasePro.Services.Orders.Models.Responses;
using CoinbasePro.Services.Orders.Types;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Services.Orders;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Orders
{
    [Subject("OrdersService")]
    public class OrdersServiceSpecs : WithSubject<OrdersService>
    {
        static OrderResponse order_response_result;

        static IList<IList<OrderResponse>> order_many_response_result;

        static CancelOrderResponse cancel_order_response_result;

        static Exception exception;

        Establish context = () =>
        {
            The<IHttpRequestMessageService>().
                WhenToldTo(p =>
                    p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<string>()
                        , Param.IsAny<string>())).
                Return<HttpMethod, string, string>(
                    (httpMethod, uri, content) => OrderRequestFixture.CreateRequest(content));

            The<IHttpClient>().
                WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>())).
                Return(Task.FromResult(new HttpResponseMessage()));
        };
        

        class when_placing_a_market_order
        {
            Establish context = () =>
                The<IHttpClient>().
                    WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).
                    Return(Task.FromResult(OrderResponseFixture.CreateMarketOrder()));

            Because of = () =>
                order_response_result = Subject.PlaceMarketOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .01M).Result;

            It should_send_the_correct_request = () =>
                   The<IHttpClient>().
                       WasToldTo(p => p.SendAsync(Param<HttpRequestMessage>.Matches(r =>
                           r.Content.ReadAsStringAsync().Result == OrderRequestFixture.CreateMarketOrderRequest())));

            It should_have_correct_order_information = () =>
            {
                order_response_result.Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.ProductId.ShouldEqual(ProductType.BtcUsd);
                order_response_result.Side.ShouldEqual(OrderSide.Buy);
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.OrderType.ShouldEqual(OrderType.Market);
                order_response_result.TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_response_result.PostOnly.ShouldBeFalse();
                order_response_result.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_response_result.FillFees.ShouldEqual(0.0000000000000000M);
                order_response_result.FilledSize.ShouldEqual(0.00000000M);
                order_response_result.ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual(OrderStatus.Pending);
                order_response_result.Settled.ShouldBeFalse();
            };
        }

        class when_placing_a_limit_order
        {
            Establish context = () =>
                The<IHttpClient>().
                    WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).
                    Return(Task.FromResult(OrderResponseFixture.CreateLimitOrder()));

            Because of = () =>
                order_response_result = Subject.PlaceLimitOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .01M, 0.1M, GoodTillTime.Min).Result;

            It should_send_the_correct_request = () =>
                The<IHttpClient>().
                    WasToldTo(p => p.SendAsync(Param<HttpRequestMessage>.Matches(r =>
                        r.Content.ReadAsStringAsync().Result == OrderRequestFixture.CreateLimitOrderRequest())));

            It should_have_correct_order_information = () =>
            {
                order_response_result.Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.ProductId.ShouldEqual(ProductType.BtcUsd);
                order_response_result.Side.ShouldEqual(OrderSide.Buy);
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.OrderType.ShouldEqual(OrderType.Limit);
                order_response_result.TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_response_result.PostOnly.ShouldBeFalse();
                order_response_result.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_response_result.FillFees.ShouldEqual(0.0000000000000000M);
                order_response_result.FilledSize.ShouldEqual(0.00000000M);
                order_response_result.ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual(OrderStatus.Pending);
                order_response_result.Settled.ShouldBeFalse();
            };
        }

        class when_placing_a_stop_order
        {
            Establish context = () =>
                The<IHttpClient>().
                    WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).
                    Return(Task.FromResult(OrderResponseFixture.CreateStopOrder()));

            Because of = () =>
                order_response_result = Subject.PlaceStopOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .01M, .1M).Result;

            It should_send_the_correct_request = () =>
                The<IHttpClient>().
                    WasToldTo(p => p.SendAsync(Param<HttpRequestMessage>.Matches(r =>
                        r.Content.ReadAsStringAsync().Result == OrderRequestFixture.CreateStopOrderRequest())));

            It should_have_correct_order_information = () =>
            {
                order_response_result.Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.ProductId.ShouldEqual(ProductType.BtcUsd);
                order_response_result.Side.ShouldEqual(OrderSide.Buy);
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.OrderType.ShouldEqual(OrderType.Stop);
                order_response_result.TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_response_result.PostOnly.ShouldBeFalse();
                order_response_result.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_response_result.FillFees.ShouldEqual(0.0000000000000000M);
                order_response_result.FilledSize.ShouldEqual(0.00000000M);
                order_response_result.ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual(OrderStatus.Pending);
                order_response_result.Settled.ShouldBeFalse();
            };
        }

        class when_placing_a_stop_limit_order
        {
            Establish context = () =>
                The<IHttpClient>().
                    WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>())).
                    Return(Task.FromResult(OrderResponseFixture.CreateStopLimitOrder()));

            Because of = () =>
                order_response_result = Subject.PlaceStopLimitOrderAsync(OrderSide.Buy, ProductType.BtcUsd, .01M, .1M, .1M).Result;

            It should_send_the_correct_request = () =>
                The<IHttpClient>().
                    WasToldTo(p => p.SendAsync(Param<HttpRequestMessage>.Matches(r =>
                        r.Content.ReadAsStringAsync().Result == OrderRequestFixture.CreateStopLimitOrderRequest())));

            It should_have_correct_order_information = () =>
            {
                order_response_result.Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.ProductId.ShouldEqual(ProductType.BtcUsd);
                order_response_result.Side.ShouldEqual(OrderSide.Buy);
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.OrderType.ShouldEqual(OrderType.Limit);
                order_response_result.TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_response_result.PostOnly.ShouldBeFalse();
                order_response_result.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_response_result.FillFees.ShouldEqual(0.0000000000000000M);
                order_response_result.FilledSize.ShouldEqual(0.00000000M);
                order_response_result.ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual(OrderStatus.Pending);
                order_response_result.Settled.ShouldBeFalse();
            };
        }

        class when_cancelling_all_orders
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CancelOrderResponseFixture.Create()));

            Because of = () =>
                cancel_order_response_result = Subject.CancelAllOrdersAsync().Result;

            It should_have_correct_number_of_cancelled_orders = () =>
                cancel_order_response_result.OrderIds.Count().ShouldEqual(2);

            It should_have_correct_list_of_cancelled_orders = () =>
            {
                cancel_order_response_result.OrderIds.First().ShouldEqual(new Guid("144c6f8e-713f-4682-8435-5280fbe8b2b4"));
                cancel_order_response_result.OrderIds.Skip(1).First().ShouldEqual(new Guid("debe4907-95dc-442f-af3b-cec12f42ebda"));
            };
        }

        class when_cancelling_order_by_id_with_success
        {
            Establish context = () =>
            {
                The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted)));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CancelOrderResponseFixture.CreateOne()));
            };

            Because of = () =>
                cancel_order_response_result = Subject.CancelOrderByIdAsync("144c6f8e-713f-4682-8435-5280fbe8b2b4").Result;

            It should_have_correct_number_of_cancelled_orders = () =>
                cancel_order_response_result.OrderIds.Count().ShouldEqual(1);

            It should_have_correct_list_of_cancelled_orders = () =>
                cancel_order_response_result.OrderIds.First().ShouldEqual(new Guid("144c6f8e-713f-4682-8435-5280fbe8b2b4"));
        }

        class when_cancelling_order_by_id_with_failure
        {
            Establish context = () =>
            {
                The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(CancelOrderResponseFixture.CreateEmpty()));
            };

            Because of = () =>
                exception = Catch.Exception(() => Subject.CancelOrderByIdAsync("144c6f8e-713f-4682-8435-5280fbe8b2b4").Result);

            It should_have_correct_error_response_message = () =>
            {
                exception.InnerException.ShouldBeOfExactType<CoinbaseProHttpException>();
                ((CoinbaseProHttpException)exception.InnerException)?.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
                exception.InnerException.ShouldContainErrorMessage("order not found");
            };
        }

        class when_getting_all_orders
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(OrderResponseFixture.CreateLimitOrderMany()));

            Because of = () =>
                order_many_response_result = Subject.GetAllOrdersAsync().Result;

            It should_have_correct_number_of_orders = () =>
                order_many_response_result.First().Count.ShouldEqual(2);

            It should_have_correct_orders = () =>
            {
                order_many_response_result.First().First().Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_many_response_result.First().First().Price.ShouldEqual(0.10000000M);
                order_many_response_result.First().First().Size.ShouldEqual(0.01000000M);
                order_many_response_result.First().First().ProductId.ShouldEqual(ProductType.BtcUsd);
                order_many_response_result.First().First().Side.ShouldEqual(OrderSide.Buy);
                order_many_response_result.First().First().Stp.ShouldEqual("dc");
                order_many_response_result.First().First().OrderType.ShouldEqual(OrderType.Limit);
                order_many_response_result.First().First().TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_many_response_result.First().First().PostOnly.ShouldBeFalse();
                order_many_response_result.First().First().CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_many_response_result.First().First().FillFees.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().First().FilledSize.ShouldEqual(0.00000000M);
                order_many_response_result.First().First().ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().First().Status.ShouldEqual(OrderStatus.Pending);
                order_many_response_result.First().First().Settled.ShouldBeFalse();

                order_many_response_result.First().Skip(1).First().Id.ShouldEqual(new Guid("8b99b139-58f2-4ab2-8e7a-c11c846e3022"));
                order_many_response_result.First().Skip(1).First().Price.ShouldEqual(0.10000000M);
                order_many_response_result.First().Skip(1).First().Size.ShouldEqual(0.01000000M);
                order_many_response_result.First().Skip(1).First().ProductId.ShouldEqual(ProductType.EthUsd);
                order_many_response_result.First().Skip(1).First().Side.ShouldEqual(OrderSide.Buy);
                order_many_response_result.First().Skip(1).First().Stp.ShouldEqual("dc");
                order_many_response_result.First().Skip(1).First().OrderType.ShouldEqual(OrderType.Limit);
                order_many_response_result.First().Skip(1).First().TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_many_response_result.First().Skip(1).First().PostOnly.ShouldBeFalse();
                order_many_response_result.First().Skip(1).First().CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_many_response_result.First().Skip(1).First().FillFees.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().Skip(1).First().FilledSize.ShouldEqual(0.00000000M);
                order_many_response_result.First().Skip(1).First().ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().Skip(1).First().Status.ShouldEqual(OrderStatus.Pending);
                order_many_response_result.First().Skip(1).First().Settled.ShouldBeFalse();
            };
        }

        class when_getting_all_orders_and_specifying_order_status
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(OrderResponseFixture.CreateLimitOrderMany(OrderStatus.Active)));

            Because of = () =>
                order_many_response_result = Subject.GetAllOrdersAsync(OrderStatus.Active).Result;

            It should_have_correct_number_of_orders = () =>
                order_many_response_result.First().Count.ShouldEqual(2);

            It should_have_correct_orders = () =>
            {
                order_many_response_result.First().First().Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_many_response_result.First().First().Price.ShouldEqual(0.10000000M);
                order_many_response_result.First().First().Size.ShouldEqual(0.01000000M);
                order_many_response_result.First().First().ProductId.ShouldEqual(ProductType.BtcUsd);
                order_many_response_result.First().First().Side.ShouldEqual(OrderSide.Buy);
                order_many_response_result.First().First().Stp.ShouldEqual("dc");
                order_many_response_result.First().First().OrderType.ShouldEqual(OrderType.Limit);
                order_many_response_result.First().First().TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_many_response_result.First().First().PostOnly.ShouldBeFalse();
                order_many_response_result.First().First().CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_many_response_result.First().First().FillFees.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().First().FilledSize.ShouldEqual(0.00000000M);
                order_many_response_result.First().First().ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().First().Status.ShouldEqual(OrderStatus.Active);
                order_many_response_result.First().First().Settled.ShouldBeFalse();

                order_many_response_result.First().Skip(1).First().Id.ShouldEqual(new Guid("8b99b139-58f2-4ab2-8e7a-c11c846e3022"));
                order_many_response_result.First().Skip(1).First().Price.ShouldEqual(0.10000000M);
                order_many_response_result.First().Skip(1).First().Size.ShouldEqual(0.01000000M);
                order_many_response_result.First().Skip(1).First().ProductId.ShouldEqual(ProductType.EthUsd);
                order_many_response_result.First().Skip(1).First().Side.ShouldEqual(OrderSide.Buy);
                order_many_response_result.First().Skip(1).First().Stp.ShouldEqual("dc");
                order_many_response_result.First().Skip(1).First().OrderType.ShouldEqual(OrderType.Limit);
                order_many_response_result.First().Skip(1).First().TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_many_response_result.First().Skip(1).First().PostOnly.ShouldBeFalse();
                order_many_response_result.First().Skip(1).First().CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_many_response_result.First().Skip(1).First().FillFees.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().Skip(1).First().FilledSize.ShouldEqual(0.00000000M);
                order_many_response_result.First().Skip(1).First().ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_many_response_result.First().Skip(1).First().Status.ShouldEqual(OrderStatus.Active);
                order_many_response_result.First().Skip(1).First().Settled.ShouldBeFalse();
            };
        }

        class when_getting_order_by_id
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(OrderResponseFixture.CreateLimitOrder()));

            Because of = () =>
                order_response_result = Subject.GetOrderByIdAsync("d0c5340b-6d6c-49d9-b567-48c4bfca13d2").Result;

            It should_have_correct_order = () =>
            {
                order_response_result.Id.ShouldEqual(new Guid("d0c5340b-6d6c-49d9-b567-48c4bfca13d2"));
                order_response_result.Price.ShouldEqual(0.10000000M);
                order_response_result.Size.ShouldEqual(0.01000000M);
                order_response_result.ProductId.ShouldEqual(ProductType.BtcUsd);
                order_response_result.Side.ShouldEqual(OrderSide.Buy);
                order_response_result.Stp.ShouldEqual("dc");
                order_response_result.OrderType.ShouldEqual(OrderType.Limit);
                order_response_result.TimeInForce.ShouldEqual(TimeInForce.Gtc);
                order_response_result.PostOnly.ShouldBeFalse();
                order_response_result.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
                order_response_result.FillFees.ShouldEqual(0.0000000000000000M);
                order_response_result.FilledSize.ShouldEqual(0.00000000M);
                order_response_result.ExecutedValue.ShouldEqual(0.0000000000000000M);
                order_response_result.Status.ShouldEqual(OrderStatus.Pending);
                order_response_result.Settled.ShouldBeFalse();
            };
        }
    }
}
