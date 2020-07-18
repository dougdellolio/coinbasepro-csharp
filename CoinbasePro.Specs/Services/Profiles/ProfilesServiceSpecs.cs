using CoinbasePro.Network.HttpClient;
using CoinbasePro.Services.Profiles;
using CoinbasePro.Services.Profiles.Models;
using CoinbasePro.Specs.JsonFixtures.Services.Profiles;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using CoinbasePro.Shared.Types;

namespace CoinbasePro.Specs.Services.Profiles
{
    [Subject("ProductsService")]
    public class ProfilesServiceSpecs : WithSubject<ProfilesService>
    {
        Establish context = () =>
            The<IHttpClient>().WhenToldTo(p => p.SendAsync(Param.IsAny<HttpRequestMessage>()))
                .Return(Task.FromResult(new HttpResponseMessage()));

        class when_getting_all_profiles
        {
            static IEnumerable<Profile> result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(AllProfilesResponseFixture.Create()));

            Because of = () =>
                result = Subject.GetAllProfilesAsync().Result;

            It should_have_correct_profiles_response_count = () =>
                result.Count().ShouldEqual(1);

            It should_have_correct_profiles = () =>
            {
                result.First().Id.ShouldEqual(new Guid("86602c68-306a-4500-ac73-4ce56a91d83c"));
                result.First().UserId.ShouldEqual("5844eceecf7e803e259d0365");
                result.First().Name.ShouldEqual("default");
                result.First().Active.ShouldEqual(true);
                result.First().IsDefault.ShouldEqual(true);
                result.First().CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
            };
        }

        class when_getting_a_profile_by_id
        {
            static Profile result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult(ProfileResponseFixture.Create()));

            Because of = () =>
                result = Subject.GetProfileByIdAsync(new Guid("86602c68-306a-4500-ac73-4ce56a91d83c")).Result;

            It should_have_correct_profile = () =>
            {
                result.Id.ShouldEqual(new Guid("86602c68-306a-4500-ac73-4ce56a91d83c"));
                result.UserId.ShouldEqual("5844eceecf7e803e259d0365");
                result.Name.ShouldEqual("default");
                result.Active.ShouldEqual(true);
                result.IsDefault.ShouldEqual(true);
                result.CreatedAt.ShouldEqual(new DateTime(2016, 12, 9));
            };
        }

        class when_creating_a_profile_transfer_on_success
        {
            static string result;

            Establish context = () =>
                The<IHttpClient>().WhenToldTo(p => p.ReadAsStringAsync(Param.IsAny<HttpResponseMessage>()))
                    .Return(Task.FromResult("OK"));

            Because of = () =>
                result = Subject.CreateProfileTransferAsync(
                    new Guid("53f58772-76e7-40d7-86bc-8155b80d7b20"),
                    new Guid("53f58772-76e7-40d7-86bc-8155b80d7b20"),
                    Currency.BTC,
                    100).Result;

            It should_have_returned_an_ok_response = () =>
                result.ShouldEqual("OK");
        }
    }
}

