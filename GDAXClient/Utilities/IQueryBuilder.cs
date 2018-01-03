using System.Collections.Generic;

namespace GDAXClient.Utilities
{
    public interface IQueryBuilder
    {
        string BuildQuery(params KeyValuePair<string, string>[] queryParameters);
    }
}
