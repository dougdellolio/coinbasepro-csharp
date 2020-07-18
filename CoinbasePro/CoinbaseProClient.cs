using System;
using CoinbasePro.Network.Authentication;
using CoinbasePro.Network.HttpClient;
using CoinbasePro.Network.HttpRequest;
using CoinbasePro.Services.Accounts;
using CoinbasePro.Services.CoinbaseAccounts;
using CoinbasePro.Services.Currencies;
using CoinbasePro.Services.Deposits;
using CoinbasePro.Services.Fees;
using CoinbasePro.Services.Fills;
using CoinbasePro.Services.Fundings;
using CoinbasePro.Services.Orders;
using CoinbasePro.Services.Payments;
using CoinbasePro.Services.Products;
using CoinbasePro.Services.Profiles;
using CoinbasePro.Services.Reports;
using CoinbasePro.Services.StablecoinConversions;
using CoinbasePro.Services.UserAccount;
using CoinbasePro.Services.Withdrawals;
using CoinbasePro.Shared.Utilities.Clock;
using CoinbasePro.Shared.Utilities.Queries;
using CoinbasePro.WebSocket;
using Serilog;

namespace CoinbasePro
{
    public class CoinbaseProClient : ICoinbaseProClient
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
            OrdersService = new OrdersService(httpClient, httpRequestMessageService, queryBuilder);
            PaymentsService = new PaymentsService(httpClient, httpRequestMessageService);
            WithdrawalsService = new WithdrawalsService(httpClient, httpRequestMessageService);
            DepositsService = new DepositsService(httpClient, httpRequestMessageService);
            ProductsService = new ProductsService(httpClient, httpRequestMessageService, queryBuilder);
            CurrenciesService = new CurrenciesService(httpClient, httpRequestMessageService);
            FillsService = new FillsService(httpClient, httpRequestMessageService);
            FundingsService = new FundingsService(httpClient, httpRequestMessageService, queryBuilder);
            ReportsService = new ReportsService(httpClient, httpRequestMessageService);
            UserAccountService = new UserAccountService(httpClient, httpRequestMessageService);
            StablecoinConversionsService = new StablecoinConversionsService(httpClient, httpRequestMessageService);
            FeesService = new FeesService(httpClient, httpRequestMessageService);
            ProfilesService = new ProfilesService(httpClient, httpRequestMessageService);
            WebSocket = new WebSocket.WebSocket(createWebSocketFeed, authenticator, clock);

            Log.Information("CoinbaseProClient constructed");
        }

        public IAccountsService AccountsService { get; }

        public ICoinbaseAccountsService CoinbaseAccountsService { get; }

        public IOrdersService OrdersService { get; }

        public IPaymentsService PaymentsService { get; }

        public IWithdrawalsService WithdrawalsService { get; }

        public IDepositsService DepositsService { get; }

        public IProductsService ProductsService { get; }

        public ICurrenciesService CurrenciesService { get; }

        public IFillsService FillsService { get; }

        public IFundingsService FundingsService { get; }

        public IFeesService FeesService { get; }

        public IReportsService ReportsService { get; }

        public IUserAccountService UserAccountService { get; }

        public IStablecoinConversionsService StablecoinConversionsService { get; }

        public IProfilesService ProfilesService { get; }

        public IWebSocket WebSocket { get; }
    }
}
