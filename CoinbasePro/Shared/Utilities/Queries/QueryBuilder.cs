using System.Collections.Generic;
using System.Text;

namespace CoinbasePro.Shared.Utilities.Queries
{
    public class QueryBuilder : IQueryBuilder
    {
        public string BuildQuery(params KeyValuePair<string, string>[] queryParameters)
        {
            var queryString = new StringBuilder("?");

            foreach(var queryParameter in queryParameters)
            {
                if(queryParameter.Value != string.Empty)
                {
                    queryString.Append(queryParameter.Key.ToLower() + "=" + queryParameter.Value + "&");
                }
            }

            return queryString.ToString().TrimEnd('&');
        }
    }
}
