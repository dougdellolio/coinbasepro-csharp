using System;

namespace GDAXClient.Utilities.Extensions
{
    public static class DateExtensions
    {
        public static double ToTimeStamp(this DateTime date)
        {
            return (date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
