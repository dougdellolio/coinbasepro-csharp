using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.UserAccount.Models;

namespace CoinbasePro.Services.UserAccount
{
    public class UserAccountService : AbstractService
    {
        public UserAccountService(
            IHttpClient httpClient,
            IHttpRequestMessageService httpRequestMessageService)
                : base(httpClient, httpRequestMessageService)
        {
        }

        public async Task<IEnumerable<TrailingVolume>> GetTrailingVolumeAsync()
        {
            return await SendServiceCall<IEnumerable<TrailingVolume>>(HttpMethod.Get, "/users/self/trailing-volume");
        }
    }
}
