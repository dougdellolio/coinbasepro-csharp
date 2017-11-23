using GDAXClient.Authentication;
using GDAXClient.Services.Accounts;
using GDAXClient.Utilities;

namespace GDAXClient
{
    public class GDAXClient
    {
        private readonly Authenticator authenticator;

        public GDAXClient(Authenticator authenticator)
        {
            this.authenticator = authenticator;

            var httpClient = new HttpClient.HttpClient();
            var clock = new Clock();
            var httpRequestMessageService = new Services.HttpRequest.HttpRequestMessageService(clock);

            accountsService = new AccountsService(httpClient, httpRequestMessageService, authenticator);
        }

        public AccountsService accountsService { get; }
    }
}
