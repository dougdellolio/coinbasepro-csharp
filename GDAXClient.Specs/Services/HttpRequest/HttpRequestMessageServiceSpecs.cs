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

            authenticator = new Authenticator("apiKey", "unsignedSignature", "passPhrase");
        };

        Because of = () =>
        {
            result_http_request_message = Subject.CreateHttpRequestMessage(HttpMethod.Get, authenticator, "/accounts");
        };

        It should_have_a_result = () =>
        {
            result_http_request_message.ShouldNotBeNull();
        };
    }
}
