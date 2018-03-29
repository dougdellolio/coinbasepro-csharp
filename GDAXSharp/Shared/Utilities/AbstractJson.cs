using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GDAXSharp.Shared.Utilities
{
    public abstract class AbstractJson
    {
        private JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings
        {
            FloatParseHandling = FloatParseHandling.Decimal,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        protected string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value, SerializerSettings);
        }

        protected T DeserializeObject<T>(string contentBody)
        {
            return JsonConvert.DeserializeObject<T>(contentBody, SerializerSettings);
        }
    }
}
