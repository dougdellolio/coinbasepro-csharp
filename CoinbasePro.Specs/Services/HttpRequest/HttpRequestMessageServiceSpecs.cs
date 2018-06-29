using System;
using System.Net.Http;
using CoinbasePro.Exceptions;
using CoinbasePro.Network.Authentication;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Shared.Utilities.Clock;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.HttpRequest
{
    [Subject("HttpRequestMessageService")]
    public class HttpRequestMessageServiceSpecs : WithSubject<HttpRequestMessageService>
    {
        static HttpRequestMessage result_http_request_message;

        Establish context = () =>
        {
            The<IClock>().WhenToldTo(p => p.GetTime()).Return(new DateTime(2017, 11, 22));

            Configure(x => x.For<IAuthenticator>().Use(new Authenticator("apiKey", new string('2', 100), "passPhrase")));
        };

        class when_making_a_request_not_on_sandbox
        {
            Establish context = () =>
                Configure(x => x.For<bool>().Use(false));

            Because of = () =>
                result_http_request_message = Subject.CreateHttpRequestMessage(HttpMethod.Get, "/accounts");

            It should_have_a_result = () =>
                result_http_request_message.ShouldNotBeNull();

            It should_have_correct_request_uri = () =>
                result_http_request_message.RequestUri.ToString().ShouldEqual("https://api.pro.coinbase.com/accounts");

            It should_have_the_required_headers = () =>
            {
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-KEY");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-SIGN");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-TIMESTAMP");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-PASSPHRASE");
            };
        }

        class when_making_a_request_using_sandbox_flag
        {
            Establish context = () =>
                Configure(x => x.For<bool>().Use(true));

            Because of = () =>
                result_http_request_message = Subject.CreateHttpRequestMessage(HttpMethod.Get, "/accounts");

            It should_have_a_result = () =>
                result_http_request_message.ShouldNotBeNull();

            It should_have_correct_request_uri = () =>
                result_http_request_message.RequestUri.ToString().ShouldEqual("https://api-public.sandbox.pro.coinbase.com/accounts");

            It should_have_the_required_headers = () =>
            {
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-KEY");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-SIGN");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-TIMESTAMP");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-PASSPHRASE");
            };
        }

        class when_making_a_request_without_authenticator_from_service
        {
            static Exception exception;

            Establish context = () =>
            {
                Configure(x => x.For<bool>().Use(false));
                Configure(x => x.For<IAuthenticator>().Use((IAuthenticator)null));
            };

            Because of = () =>
                exception = Catch.Exception(() => Subject.CreateHttpRequestMessage(HttpMethod.Get, "/accounts"));

            It should_throw_an_error = () =>
                exception.ShouldBeOfExactType<CoinbaseProHttpException>();

            It should_contain_a_message = () =>
                exception.Message.ShouldContain("Please provide an authenticator to the client to request");
        }

        class when_making_a_request_with_authenticator_from_service
        {
            static Exception exception;

            Establish context = () =>
            {
                Configure(x => x.For<bool>().Use(false));
                Configure(x => x.For<IAuthenticator>().Use((IAuthenticator)null));
            };

            Because of = () =>
                exception = Catch.Exception(() => Subject.CreateHttpRequestMessage(HttpMethod.Get, "/accounts"));

            It should_not_throw_an_error = () =>
                exception.ShouldNotBeNull();
        }
    }
}
