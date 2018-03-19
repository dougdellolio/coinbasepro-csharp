using System.Collections.Generic;
using System.Net.Http;
using GDAXSharp.Network.Authentication;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.UserAccount.Models;
using System.Threading.Tasks;

namespace GDAXSharp.Services.UserAccount
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
