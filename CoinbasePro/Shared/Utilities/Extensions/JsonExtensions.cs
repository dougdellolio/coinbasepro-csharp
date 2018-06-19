namespace CoinbasePro.Shared.Utilities.Extensions
{
    public static class JsonExtensions
    {
        public static bool TryDeserializeObject<T>(this string json, out T result)
        {
            try
            {
                result = JsonConfig.DeserializeObject<T>(json);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
    }
}
