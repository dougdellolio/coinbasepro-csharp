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
using GDAXSharp.Services.Withdrawals;
using GDAXSharp.Shared.Utilities.Clock;
using GDAXSharp.Shared.Utilities.Queries;

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
            var httpRequestMessageService = new HttpRequestMessageService(clock, sandBox);
            var queryBuilder = new QueryBuilder();

            AccountsService = new AccountsService(httpClient, httpRequestMessageService, authenticator);
            CoinbaseAccountsService = new CoinbaseAccountsService(httpClient, httpRequestMessageService, authenticator);
            OrdersService = new OrdersService(httpClient, httpRequestMessageService, authenticator);
            PaymentsService = new PaymentsService(httpClient, httpRequestMessageService, authenticator);
            WithdrawalsService = new WithdrawalsService(httpClient, httpRequestMessageService, authenticator);
            DepositsService = new DepositsService(httpClient, httpRequestMessageService, authenticator);
            ProductsService = new ProductsService(httpClient, httpRequestMessageService, authenticator, queryBuilder);
            CurrenciesService = new CurrenciesService(httpClient, httpRequestMessageService, authenticator);
            FillsService = new FillsService(httpClient, httpRequestMessageService, authenticator);
            FundingsService = new FundingsService(httpClient, httpRequestMessageService, authenticator, queryBuilder);
            ReportsService = new ReportsService(httpClient, httpRequestMessageService, authenticator);
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
    }
}
