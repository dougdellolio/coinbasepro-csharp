using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Payments;
using CoinbasePro.Services.Payments.Models;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Services.Payments;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.Payments
{
    [Subject("PaymentsService")]
    public class PaymentsServiceSpecs : WithSubject<PaymentsService>
    {
        static IEnumerable<PaymentMethod> payment_methods;

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_requesting_for_all_payment_methods
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(PaymentMethodsResponseFixture.Create()));

            Because of = () =>
                payment_methods = Subject.GetAllPaymentMethodsAsync().Result;

            It should_have_correct_payment_methods_count = () =>
                payment_methods.Count().ShouldEqual(1);

            It should_have_correct_payment_methods = () =>
            {
                payment_methods.First().Id.ShouldEqual(new Guid("bc6d7162-d984-5ffa-963c-a493b1c1370b"));
                payment_methods.First().Name.ShouldEqual("Bank of America - eBan... ********7134");
                payment_methods.First().Currency.ShouldEqual(Currency.USD);
                payment_methods.First().AllowBuy.ShouldBeTrue();
                payment_methods.First().AllowSell.ShouldBeTrue();
                payment_methods.First().AllowDeposit.ShouldBeTrue();
                payment_methods.First().AllowWithdraw.ShouldBeTrue();
                payment_methods.First().Limits.Buy.Count().ShouldEqual(1);
                payment_methods.First().Limits.InstantBuy.Count().ShouldEqual(1);
                payment_methods.First().Limits.Sell.Count().ShouldEqual(1);
                payment_methods.First().Limits.Deposit.Count().ShouldEqual(1);
            };
        }
    }
}
