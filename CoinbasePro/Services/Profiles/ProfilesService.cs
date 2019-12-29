using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Profiles.Models;
using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinbasePro.Services.Profiles
{
    public class ProfilesService : AbstractService, IProfilesService
    {
        public ProfilesService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            return await SendServiceCall<List<Profile>>(HttpMethod.Get, "/profiles");
        }

        public async Task<Profile> GetProfileByIdAsync(Guid id)
        {
            return await SendServiceCall<Profile>(HttpMethod.Get, $"/profiles/{id}");
        }

        public async Task<string> CreateProfileTransferAsync(
            Guid from,
            Guid to,
            Currency currency,
            decimal amount)
        {
            var newProfileTransfer = new ProfileTransfer
            {
                From = from,
                To = to,
                Currency = currency,
                Amount = amount
            };

            return await SendServiceCall<string>(HttpMethod.Post, "/profiles/transfer", JsonConfig.SerializeObject(newProfileTransfer)).ConfigureAwait(false);
        }
    }
}
