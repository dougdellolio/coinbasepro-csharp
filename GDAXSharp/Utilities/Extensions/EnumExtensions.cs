using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using GDAXSharp.Shared;

namespace GDAXSharp.Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue<T>(this T value)
            where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString(CultureInfo.InvariantCulture))
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
    }

    public static class ProductIdExtensions
    {
        public static Currency BaseCurrency(this ProductType value)
        {
            var baseCurrency = value.GetEnumMemberValue().Split('-')[0];

            return (Currency)Enum.Parse(typeof(Currency), baseCurrency);
        }

        public static Currency QuoteCurrency(this ProductType value)
        {
            var quoteCurrency = value.GetEnumMemberValue().Split('-')[1];

            return (Currency)Enum.Parse(typeof(Currency), quoteCurrency);
        }

    }
}

