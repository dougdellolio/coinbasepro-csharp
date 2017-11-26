using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.HttpRequest;
using GDAXClient.Services.Payments;
using GDAXClient.Specs.JsonFixtures.Payments;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GDAXClient.Specs.Services.Payments
{
    [Subject("PaymentsService")]
    public class PaymentsServiceSpecs : WithSubject<PaymentsService>
    {
        static Authenticator authenticator;

        static IEnumerable<PaymentMethod> payment_methods;

        Establish context = () =>
            authenticator = new Authenticator("apiKey", new string('2', 100), "passPhrase");

        class when_requesting_for_all_payment_methods
        {
            Establish context = () =>
            {
                The<IHttpRequestMessageService>().WhenToldTo(p => p.CreateHttpRequestMessage(Param.IsAny<HttpMethod>(), Param.IsAny<Authenticator>(), Param.IsAny<string>(), Param.IsAny<string>()))
                    .Return(new HttpRequestMessage());

                The<IHttpClient>().WhenToldTo(p => p.SendASync(Param.IsAny<HttpRequestMessage>()))
                    .Return(Task.FromResult(new HttpResponseMessage()));

                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(PaymentMethodsResponseFixture.Create()));
            };

            Because of = () =>
                payment_methods = Subject.GetAllPaymentMethodsAsync().Result;

            It should_have_correct_payment_methods_count = () =>
                payment_methods.Count().ShouldEqual(1);
        }
    }
}
