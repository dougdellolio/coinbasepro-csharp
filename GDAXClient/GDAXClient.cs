using GDAXClient.Authentication;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.Orders;
using GDAXClient.Services.Payments;
using GDAXClient.Services.WithdrawalsService;
using GDAXClient.Utilities;

namespace GDAXClient
{
    public class GDAXClient
    {
        private readonly Authenticator authenticator;

        public GDAXClient(Authenticator authenticator, bool sandBox = false)
        {
            this.authenticator = authenticator;

            var httpClient = new HttpClient.HttpClient();
            var clock = new Clock();
            var httpRequestMessageService = new Services.HttpRequest.HttpRequestMessageService(clock, sandBox);

            AccountsService = new AccountsService(httpClient, httpRequestMessageService, authenticator);
            OrdersService = new OrdersService(httpClient, httpRequestMessageService, authenticator);
            PaymentsService = new PaymentsService(httpClient, httpRequestMessageService, authenticator);
            WithdrawalsService = new WithdrawalsService(httpClient, httpRequestMessageService, authenticator);
        }

        public AccountsService AccountsService { get; }

        public OrdersService OrdersService { get; }

        public PaymentsService PaymentsService { get; }

        public WithdrawalsService WithdrawalsService { get; }
    }
}
