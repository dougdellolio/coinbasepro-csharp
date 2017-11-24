using GDAXClient.Utilities.Extensions;
using Machine.Specifications;
using System;

namespace GDAXClient.Specs.Utilities.Extensions
{
    [Subject("DateExtensions")]
    public class DateExtensionsSpecs
    {
        static DateTime date;

        static double timestamp_result;

        Establish context = () =>
            date = new DateTime(1970, 1, 2);

        Because of = () =>
            timestamp_result = date.ToTimeStamp();

        It should_calculate_correct_time_stamp = () =>
             timestamp_result.ShouldEqual(86400);
    }
}
