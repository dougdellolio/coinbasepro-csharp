using GDAXClient.Authentication;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Utilities;
using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Net.Http;

namespace GDAXClient.Specs.Services.HttpRequest
{
    [Subject("HttpRequestMessageService")]
    public class HttpRequestMessageServiceSpecs : WithSubject<HttpRequestMessageService>
    {
        static Authenticator authenticator;

        static HttpRequestMessage result_http_request_message;

        Establish context = () =>
        {
            The<IClock>().WhenToldTo(p => p.GetTime()).Return(new DateTime(2017, 11, 22));

            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");
        };

        class when_making_a_request_not_on_sandbox
        {
            Establish context = () =>
                Configure(x => x.For<bool>().Use(false));

            Because of = () =>
                result_http_request_message = Subject.CreateHttpRequestMessage(HttpMethod.Get, authenticator, "/accounts");

            It should_have_a_result = () =>
                result_http_request_message.ShouldNotBeNull();

            It should_have_correct_request_uri = () =>
                result_http_request_message.RequestUri.ToString().ShouldEqual("https://api.gdax.com/accounts");

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
                result_http_request_message = Subject.CreateHttpRequestMessage(HttpMethod.Get, authenticator, "/accounts");

            It should_have_a_result = () =>
                result_http_request_message.ShouldNotBeNull();

            It should_have_correct_request_uri = () =>
                result_http_request_message.RequestUri.ToString().ShouldEqual("https://api-public.sandbox.gdax.com/accounts");

            It should_have_the_required_headers = () =>
            {
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-KEY");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-SIGN");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-TIMESTAMP");
                result_http_request_message.Headers.ToString().ShouldContain("CB-ACCESS-PASSPHRASE");
            };
        }
    }
}
