using GDAXClient.Services.Orders;
using GDAXClient.Utilities.Extensions;
using Machine.Specifications;

namespace GDAXClient.Specs.Utilities.Extensions
{
    [Subject("ProductTypeExtensions")]
    public class ProductTypeExtensionsSpecs
    {
        static ProductType product_type;

        static string product_type_result;

        class eth_product_type
        {
            Establish context = () =>
                product_type = ProductType.EthUsd;

            Because of = () =>
                product_type_result = product_type.ToDasherizedUpper();

            It should_calculate_correct_time_stamp = () =>
                 product_type_result.ShouldEqual("ETH-USD");
        }

        class btc_product_type
        {
            Establish context = () =>
                product_type = ProductType.BtcUsd;

            Because of = () =>
                product_type_result = product_type.ToDasherizedUpper();

            It should_calculate_correct_time_stamp = () =>
                 product_type_result.ShouldEqual("BTC-USD");
        }

        class ltc_product_type
        {
            Establish context = () =>
                product_type = ProductType.LtcUsd;

            Because of = () =>
                product_type_result = product_type.ToDasherizedUpper();

            It should_calculate_correct_time_stamp = () =>
                 product_type_result.ShouldEqual("LTC-USD");
        }

        class bch_product_type
        {
            Establish context = () =>
                product_type = ProductType.BchUsd;

            Because of = () =>
                product_type_result = product_type.ToDasherizedUpper();

            It should_calculate_correct_time_stamp = () =>
                 product_type_result.ShouldEqual("BCH-USD");
        }

    }
}
