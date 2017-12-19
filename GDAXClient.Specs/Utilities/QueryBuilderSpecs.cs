using GDAXClient.Utilities;
using Machine.Fakes;
using Machine.Specifications;
using System.Collections.Generic;

namespace GDAXClient.Specs.Utilities
{
    [Subject("QueryBuilder")]
    public class QueryBuilderSpecs : WithSubject<QueryBuilder>
    {
        static string result;

        Because of = () =>
        {
            result = Subject.BuildQuery(
                new KeyValuePair<string, string>("limit", "10"),
                new KeyValuePair<string, string>("status", "approved"),
                new KeyValuePair<string, string>("product_id", "eth-usd"));
        };

        It should_have_built_the_correct_query = () =>
        {
            result.ShouldEqual("?limit=10&status=approved&product_id=eth-usd");
        };
    }
}
