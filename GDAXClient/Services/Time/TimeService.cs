using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GDAXClient.Authentication;
using GDAXClient.HttpClient;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.HttpRequest;
using Newtonsoft.Json;

namespace GDAXClient.Services.Time
{
    public class TimeService : AbstractService
    {
        private readonly IHttpRequestMessageService httpRequestMessageService;

        private readonly IHttpClient httpClient;

        private readonly IAuthenticator authenticator;

        public TimeService(IHttpClient httpClient, IHttpRequestMessageService httpRequestMessageService, IAuthenticator authenticator) 
            : base(httpClient, httpRequestMessageService)
        {
            this.httpRequestMessageService = httpRequestMessageService;
            this.httpClient = httpClient;
            this.authenticator = authenticator;
        }

        public async Task<Models.Time> GetTimeAsync()
        {
            var httpResponseMessage = await SendHttpRequestMessageAsync(HttpMethod.Get, authenticator, "/time");
            var contentBody = await httpClient.ReadAsStringAsync(httpResponseMessage).ConfigureAwait(false);
            var time = JsonConvert.DeserializeObject<Models.Time>(contentBody);

            return time;
        }
    }
}
