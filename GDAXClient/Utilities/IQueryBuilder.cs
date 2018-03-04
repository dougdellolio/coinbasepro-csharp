using System.Collections.Generic;

namespace GDAXSharp.Utilities
{
    public interface IQueryBuilder
    {
        string BuildQuery(params KeyValuePair<string, string>[] queryParameters);
    }
}
