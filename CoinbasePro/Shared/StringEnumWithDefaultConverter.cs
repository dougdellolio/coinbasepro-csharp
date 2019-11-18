//source: https://gist.github.com/gubenkoved/999eb73e227b7063a67a50401578c3a7

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Serilog;

namespace CoinbasePro.Shared
{
    public class StringEnumWithDefaultConverter : JsonConverter
    {
        private Dictionary<Type, Dictionary<string, object>> FromValueMap;

        private Dictionary<Type, Dictionary<object, string>> ToValueMap;

        private const string UnknownValue = "Unknown";

        public override bool CanConvert(Type objectType)
        {
            var type = IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
            return type != null && type.IsEnum;
        }

        public override object ReadJson
            (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var isNullable = IsNullableType(objectType);
            var enumType = isNullable 
                ? Nullable.GetUnderlyingType(objectType) 
                : objectType;

            InitMap(enumType);

            if (reader.TokenType == JsonToken.String)
            {
                var enumText = reader.Value.ToString();

                var val = FromValue(enumType, enumText);

                if (val != null)
                {
                    return val;
                }
            }

            if (isNullable)
            {
                return null;
            }

            var names = Enum.GetNames(enumType);

            var unknownName = names.
                SingleOrDefault(n => string.Equals(n, UnknownValue, StringComparison.OrdinalIgnoreCase));

            if (unknownName == null)
            {
                throw new JsonSerializationException(
                    $"Unable to parse '{reader.Value}' to enum {enumType}. Consider adding Unknown as fail-back value.");
            }

            Log.Error("The response value {val} cannot be mapped to the enum type {enumType}.", reader.Value, enumType);

            return Enum.Parse(enumType, unknownName);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var enumType = value.GetType();

            InitMap(enumType);

            var val = ToValue(enumType, value);

            writer.WriteValue(val);
        }

        private bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private void InitMap(Type enumType)
        {
            if (FromValueMap == null)
            {
                FromValueMap = new Dictionary<Type, Dictionary<string, object>>();
            }

            if (ToValueMap == null)
            {
                ToValueMap = new Dictionary<Type, Dictionary<object, string>>();
            }

            if (FromValueMap.ContainsKey(enumType))
            {
                return;
            }

            var fromMap = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            var toMap = new Dictionary<object, string>();

            var fields = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var field in fields)
            {
                var name = field.Name;
                var enumValue = Enum.Parse(enumType, name);

                var enumMemberAttrbiute = field.GetCustomAttribute<EnumMemberAttribute>();

                if (enumMemberAttrbiute != null)
                {
                    var enumMemberValue = enumMemberAttrbiute.Value;

                    fromMap[enumMemberValue] = enumValue;
                    toMap[enumValue] = enumMemberValue;
                }
                else
                {
                    toMap[enumValue] = name;
                }

                fromMap[name] = enumValue;
            }

            FromValueMap[enumType] = fromMap;
            ToValueMap[enumType] = toMap;
        }

        private string ToValue(Type enumType, object obj)
        {
            var map = ToValueMap[enumType];

            return map[obj];
        }

        private object FromValue(Type enumType, string value)
        {
            var map = FromValueMap[enumType];

            return !map.ContainsKey(value) 
                ? null 
                : map[value];
        }
    }
}
