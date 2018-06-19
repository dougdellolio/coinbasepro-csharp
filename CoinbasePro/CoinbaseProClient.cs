using System;
using CoinbasePro.Network.Authentication;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Accounts;
using CoinbasePro.Services.CoinbaseAccounts;
using CoinbasePro.Services.Currencies;
using CoinbasePro.Services.Deposits;
using CoinbasePro.Services.Fills;
using CoinbasePro.Services.Fundings;
using CoinbasePro.Services.Orders;
using CoinbasePro.Services.Payments;
using CoinbasePro.Services.Products;
using CoinbasePro.Services.Reports;
using CoinbasePro.Services.UserAccount;
using CoinbasePro.Services.Withdrawals;
using CoinbasePro.Shared.Utilities.Clock;
using CoinbasePro.Shared.Utilities.Queries;
using CoinbasePro.WebSocket;
using Serilog;

namespace CoinbasePro
{
    public class CoinbaseProClient
    {
        public CoinbaseProClient(
            bool sandBox = false)
                : this(null, new HttpClient(), sandBox)
        {
        }

        public CoinbaseProClient(
            IAuthenticator authenticator,
            bool sandBox = false)
                : this(authenticator, new HttpClient(), sandBox)
        {
        }

        public CoinbaseProClient(
            IAuthenticator authenticator,
            IHttpClient httpClient,
            bool sandBox = false)
        {
            var clock = new Clock();
            var httpRequestMessageService = new HttpRequestMessageService(authenticator, clock, sandBox);
            var createWebSocketFeed = (Func<IWebSocketFeed>)(() => new WebSocketFeed(sandBox));
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
            WebSocket = new WebSocket.WebSocket(createWebSocketFeed, authenticator, clock);

            Log.Information("CoinbaseProClient constructed");
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
