using System.Collections.Generic;
using System.Text;

namespace GDAXClient.Utilities
{
    public class QueryBuilder
    {
        public string BuildQuery(params KeyValuePair<string, string>[] queryParameters)
        {
            var queryString = new StringBuilder("?");

            foreach(var queryParameter in queryParameters)
            {
                if(queryParameter.Value != "null")
                {
                    queryString.Append(queryParameter.Key + "=" + queryParameter.Value + "&");
                }
            }

            return queryString.ToString().TrimEnd('&');
        }
    }
}
