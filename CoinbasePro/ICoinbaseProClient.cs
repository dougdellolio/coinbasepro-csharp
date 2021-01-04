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
using CoinbasePro.Services.Reports;
using CoinbasePro.Services.StablecoinConversions;
using CoinbasePro.Services.UserAccount;
using CoinbasePro.Services.Withdrawals;
using CoinbasePro.WebSocket;

namespace CoinbasePro
{
    public interface ICoinbaseProClient
    {
        IAccountsService AccountsService { get; }

        ICoinbaseAccountsService CoinbaseAccountsService { get; }

        IOrdersService OrdersService { get; }

        IPaymentsService PaymentsService { get; }

        IWithdrawalsService WithdrawalsService { get; }

        IDepositsService DepositsService { get; }

        IProductsService ProductsService { get; }

        ICurrenciesService CurrenciesService { get; }

        IFillsService FillsService { get; }

        IFeesService FeesService { get; }

        IFundingsService FundingsService { get; }

        IReportsService ReportsService { get; }

        IUserAccountService UserAccountService { get; }

        IStablecoinConversionsService StablecoinConversionsService { get; }

        IWebSocket WebSocket { get; }
    }
}
