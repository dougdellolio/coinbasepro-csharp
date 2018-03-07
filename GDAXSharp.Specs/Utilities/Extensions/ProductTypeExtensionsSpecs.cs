using GDAXSharp.Utilities.Extensions;
using GDAXSharp.Shared;
using Machine.Specifications;

namespace GDAXSharp.Specs.Utilities.Extensions
{
    [Subject("ProductTypeExtensions")]
    public class ProductTypeExtensionsSpecs
    {
        static ProductType productId;

        static Currency baseCurrency;
        private static Currency quoteCurrency;

        class base_eth_product_type
        {
            Establish context = () => productId = ProductType.EthUsd;

            private Because of = () =>
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

            private Because of = () =>
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

            private Because of = () =>
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

            private Because of = () =>
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
