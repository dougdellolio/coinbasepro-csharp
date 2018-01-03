using GDAXClient.Authentication;
using GDAXClient.Products;
using GDAXClient.Services.Accounts;
using GDAXClient.Services.CoinbaseAccounts;
using GDAXClient.Services.Currencies;
using GDAXClient.Services.Deposits;
using GDAXClient.Services.Fills;
using GDAXClient.Services.Fundings;
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
            var queryBuilder = new QueryBuilder();

            AccountsService = new AccountsService(httpClient, httpRequestMessageService, authenticator);
            CoinbaseAccountsService = new CoinbaseAccountsService(httpClient, httpRequestMessageService, authenticator);
            OrdersService = new OrdersService(httpClient, httpRequestMessageService, authenticator);
            PaymentsService = new PaymentsService(httpClient, httpRequestMessageService, authenticator);
            WithdrawalsService = new WithdrawalsService(httpClient, httpRequestMessageService, authenticator);
            DepositsService = new DepositsService(httpClient, httpRequestMessageService, authenticator);
            ProductsService = new ProductsService(httpClient, httpRequestMessageService, authenticator);
            CurrenciesService = new CurrenciesService(httpClient, httpRequestMessageService, authenticator);
            FillsService = new FillsService(httpClient, httpRequestMessageService, authenticator);
            FundingsService = new FundingsService(httpClient, httpRequestMessageService, authenticator, queryBuilder);
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
    }
}
