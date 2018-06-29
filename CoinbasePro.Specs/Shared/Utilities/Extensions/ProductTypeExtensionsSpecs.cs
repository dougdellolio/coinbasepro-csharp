using CoinbasePro.Shared.Types;
using CoinbasePro.Shared.Utilities.Extensions;
using Machine.Specifications;

namespace CoinbasePro.Specs.Shared.Utilities.Extensions
{
    [Subject("ProductTypeExtensions")]
    public class ProductTypeExtensionsSpecs
    {
        static ProductType productId;

        static Currency baseCurrency;

        static Currency quoteCurrency;

        class eth_product_type
        {
            Establish context = () => productId = ProductType.EthUsd;

            Because of = () =>
            {
                baseCurrency = productId.BaseCurrency();
                quoteCurrency = productId.QuoteCurrency();
            };

            It should_calculate_correct_base_currency = () =>
                baseCurrency.ShouldEqual(Currency.ETH);

            It should_calculate_correct_quote_currency = () =>
                quoteCurrency.ShouldEqual(Currency.USD);
        }

        class btc_product_type
        {
            Establish context = () => productId = ProductType.BtcGbp;

            Because of = () =>
            {
                baseCurrency = productId.BaseCurrency();
                quoteCurrency = productId.QuoteCurrency();
            };

            It should_calculate_correct_base_currency = () =>
                baseCurrency.ShouldEqual(Currency.BTC);

            It should_calculate_correct_quote_currency = () =>
                quoteCurrency.ShouldEqual(Currency.GBP);
        }

        class ltc_product_type
        {
            Establish context = () => productId = ProductType.LtcEur;

            Because of = () =>
            {
                baseCurrency = productId.BaseCurrency();
                quoteCurrency = productId.QuoteCurrency();
            };

            It should_calculate_correct_base_currency = () =>
                baseCurrency.ShouldEqual(Currency.LTC);

            It should_calculate_correct_quote_currency = () =>
                quoteCurrency.ShouldEqual(Currency.EUR);
        }

        class bch_product_type
        {
            Establish context = () => productId = ProductType.BchBtc;

            Because of = () =>
            {
                baseCurrency = productId.BaseCurrency();
                quoteCurrency = productId.QuoteCurrency();
            };

            It should_calculate_correct_base_currency = () =>
                baseCurrency.ShouldEqual(Currency.BCH);

            It should_calculate_correct_quote_currency = () =>
                quoteCurrency.ShouldEqual(Currency.BTC);
        }
    }
}
