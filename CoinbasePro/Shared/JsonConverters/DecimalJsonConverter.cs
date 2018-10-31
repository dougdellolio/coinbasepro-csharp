using System;
using Newtonsoft.Json;

namespace CoinbasePro.Shared.JsonConverters
{
    public class DecimalJsonConverter : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal);
        }

        public override void WriteJson(
            JsonWriter writer, 
            object value, 
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(
            JsonReader reader, 
            Type objectType, 
            object existingValue, 
            JsonSerializer serializer)
        {
            var value = reader.Value as string;
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            try
            {
                return decimal.Parse(value);
            }
            catch
            {
                return (decimal)double.Parse(value);
            }
        }
    }
}
