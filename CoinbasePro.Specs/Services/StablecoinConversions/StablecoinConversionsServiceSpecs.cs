using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.StablecoinConversions;
using CoinbasePro.Services.StablecoinConversions.Models;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Services.StablecoinConversions;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.StablecoinConversions
{
    [Subject("StablecoinConversionsService")]
    public class StablecoinConversionsServiceSpecs : WithSubject<StablecoinConversionsService>
    {
        static StablecoinConversionResponse stablecoin_conversion_response;

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_creating_a_conversion
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(StablecoinConversionsResponseFixture.Create()));

            Because of = () =>
                stablecoin_conversion_response = Subject.CreateConversion(Currency.USD, Currency.USDC, 1000m).Result;

            It should_return_correct_response = () =>
            {
                stablecoin_conversion_response.Id.ShouldEqual(new Guid("8942caee-f9d5-4600-a894-4811268545db"));
                stablecoin_conversion_response.Amount.ShouldEqual(10000M);
                stablecoin_conversion_response.FromAccountId.ShouldEqual("7849cc79-8b01-4793-9345-bc6b5f08acce");
                stablecoin_conversion_response.ToAccountId.ShouldEqual("105c3e58-0898-4106-8283-dc5781cda07b");
                stablecoin_conversion_response.FromCurrency.ShouldEqual(Currency.USD);
                stablecoin_conversion_response.ToCurrency.ShouldEqual(Currency.USDC);
            };
        }
    }
}
