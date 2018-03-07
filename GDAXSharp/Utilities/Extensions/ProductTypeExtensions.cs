using GDAXSharp.Shared;

namespace GDAXSharp.Utilities.Extensions
{
    public static class ProductTypeExtensions
    {
        public static string ToDasherizedUpper(this ProductType orderType)
        {
            var orderTypeString = orderType.ToString();

            return orderTypeString.Insert(3, "-").ToUpper();
        }
    }
}
