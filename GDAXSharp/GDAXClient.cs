using GDAXSharp.Network.Authentication;
using GDAXSharp.Network.HttpClient;
using GDAXSharp.Network.HttpRequest;
using GDAXSharp.Services.Accounts;
using GDAXSharp.Services.CoinbaseAccounts;
using GDAXSharp.Services.Currencies;
using GDAXSharp.Services.Deposits;
using GDAXSharp.Services.Fills;
using GDAXSharp.Services.Fundings;
using GDAXSharp.Services.Orders;
using GDAXSharp.Services.Payments;
using GDAXSharp.Services.Products;
using GDAXSharp.Services.Reports;
using GDAXSharp.Services.UserAccount;
using GDAXSharp.Services.Withdrawals;
using GDAXSharp.Shared.Utilities.Clock;
using GDAXSharp.Shared.Utilities.Queries;
using GDAXSharp.WebSocket;

namespace GDAXSharp
{
    public class GDAXClient
    {
        public GDAXClient(
            IAuthenticator authenticator,
            bool sandBox = false)
                : this(authenticator, new HttpClient(), sandBox)
        {
        }

        public GDAXClient(
            IAuthenticator authenticator,
            IHttpClient httpClient,
            bool sandBox = false)
        {
            var clock = new Clock();
            var httpRequestMessageService = new HttpRequestMessageService(authenticator, clock, sandBox);
            var webSocketFeed = new WebSocketFeed();
            var queryBuilder = new QueryBuilder();

            AccountsService = new AccountsService(httpClient, httpRequestMessageService);
            CoinbaseAccountsService = new CoinbaseAccountsService(httpClient, httpRequestMessageService);
            OrdersService = new OrdersService(httpClient, httpRequestMessageService);
            PaymentsService = new PaymentsService(httpClient, httpRequestMessageService);
            WithdrawalsService = new WithdrawalsService(httpClient, httpRequestMessageService);
            DepositsService = new DepositsService(httpClient, httpRequestMessageService);
            ProductsService = new ProductsService(httpClient, httpRequestMessageService, queryBuilder);
            CurrenciesService = new CurrenciesService(httpClient, httpRequestMessageService);
            FillsService = new FillsService(httpClient, httpRequestMessageService);
            FundingsService = new FundingsService(httpClient, httpRequestMessageService, queryBuilder);
            ReportsService = new ReportsService(httpClient, httpRequestMessageService);
            UserAccountService = new UserAccountService(httpClient, httpRequestMessageService);
            WebSocket = new WebSocket.WebSocket(webSocketFeed, authenticator, clock, sandBox); 
        }

        public AccountsService AccountsService { get; }

        public CoinbaseAccountsService CoinbaseAccountsService { get; }

        public OrdersService OrdersService { get; }

        public PaymentsService PaymentsService { get; }

        public WithdrawalsService WithdrawalsService { get; }

        public DepositsService DepositsService { get; }

        public ProductsService ProductsService { get; }

        public CurrenciesService CurrenciesService { get; }

        public FillsService FillsService { get; }

        public FundingsService FundingsService { get; }

        public ReportsService ReportsService { get; }

        public UserAccountService UserAccountService { get; }

        public WebSocket.WebSocket WebSocket { get; }
    }
}
