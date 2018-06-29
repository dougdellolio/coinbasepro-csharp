using System;
using System.Linq;
using CoinbasePro.Services.Products.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinbasePro.Shared.Utilities.Converters
{
    public class CandleConverter : JsonConverter
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jarray = JArray.Load(reader);
            return new Candle
            {
                Time = UnixEpoch.AddSeconds((long)jarray.ElementAt(0)),
                Low = (decimal?)jarray.ElementAt(1),
                High = (decimal?)jarray.ElementAt(2),
                Open = (decimal?)jarray.ElementAt(3),
                Close = (decimal?)jarray.ElementAt(4),
                Volume = (decimal)jarray.ElementAt(5)
            };
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
