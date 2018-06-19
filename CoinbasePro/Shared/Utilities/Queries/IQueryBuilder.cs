using System.Collections.Generic;

namespace CoinbasePro.Shared.Utilities.Queries
{
    public interface IQueryBuilder
    {
        string BuildQuery(params KeyValuePair<string, string>[] queryParameters);
    }
}
