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

            It should_have_correct_payment_methods = () =>
            {
                payment_methods.First().Id.ShouldEqual(new System.Guid("bc6d7162-d984-5ffa-963c-a493b1c1370b"));
                payment_methods.First().Name.ShouldEqual("Bank of America - eBan... ********7134");
                payment_methods.First().Currency.ShouldEqual("USD");
                payment_methods.First().Allow_buy.ShouldBeTrue();
                payment_methods.First().Allow_sell.ShouldBeTrue();
                payment_methods.First().Allow_deposit.ShouldBeTrue();
                payment_methods.First().Allow_withdraw.ShouldBeTrue();
                payment_methods.First().Limits.Buy.Count().ShouldEqual(1);
                payment_methods.First().Limits.Instant_buy.Count().ShouldEqual(1);
                payment_methods.First().Limits.Sell.Count().ShouldEqual(1);
                payment_methods.First().Limits.Deposit.Count().ShouldEqual(1);
            };
        }
    }
}
