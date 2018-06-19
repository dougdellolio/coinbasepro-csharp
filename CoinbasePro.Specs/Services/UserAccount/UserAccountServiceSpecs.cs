using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.UserAccount;
using CoinbasePro.Services.UserAccount.Models;
using CoinbasePro.Shared.Types;
using CoinbasePro.Specs.JsonFixtures.Services.UserAccount;
using Machine.Fakes;
using Machine.Specifications;

namespace CoinbasePro.Specs.Services.UserAccount
{
    [Subject("UserAccountService")]
    public class UserAccountServiceSpecs : WithSubject<UserAccountService>
    {
        static IEnumerable<TrailingVolume> result;

        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_requesting_for_trailing_volume
        {
            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(TrailingVolumeFixture.Create()));

            Because of = () =>
                result = Subject.GetTrailingVolumeAsync().Result;

            It should_return_a_response = () =>
                result.ShouldNotBeNull();

            It should_return_a_correct_response = () =>
            {
                result.First().ProductId.ShouldEqual(ProductType.BtcUsd);
                result.First().ExchangeVolume.ShouldEqual(11800M);
                result.First().Volume.ShouldEqual(100M);
                result.First().RecordedAt.ShouldEqual(new DateTime(2016, 12, 9));

                result.Skip(1).First().ProductId.ShouldEqual(ProductType.LtcUsd);
                result.Skip(1).First().ExchangeVolume.ShouldEqual(51010.041M);
                result.Skip(1).First().Volume.ShouldEqual(2010.041M);
                result.Skip(1).First().RecordedAt.ShouldEqual(new DateTime(2016, 12, 9));
            };
        }
    }
}
