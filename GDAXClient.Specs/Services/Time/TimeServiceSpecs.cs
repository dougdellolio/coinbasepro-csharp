using System;
using System.Net.Http;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Time;
using GDAXClient.Services.Time.Models.Responses;
using GDAXClient.Specs.JsonFixtures.MarginTransfers;
using GDAXClient.Specs.JsonFixtures.Time;
using Machine.Fakes;
using Machine.Specifications;

namespace GDAXClient.Specs.Services.Time
{
    [Subject("TimeService")]
    public class TimeServiceSpecs : WithSubject<TimesService>
    {
        static TimeResponse time_result;

        static Authenticator authenticator;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_requesting_server_time
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(),
                        Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(TimeResponseFixture.Create()));
            };

            Because of = () =>
                time_result = Subject.GetServerTimeAsync().Result;

            It should_return_a_correct_response = () =>
            {
                time_result.Iso.ShouldEqual(new DateTime(2015, 01, 07, 23, 47, 25, 201));
                time_result.Epoch.ShouldEqual(1420674445.201M);
            };
        }
    }
}
