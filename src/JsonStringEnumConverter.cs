using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sufficit.Json
{
    public class JsonStringEnumConverter<T> : JsonConverter<T> where T : struct, System.Enum
    {
        public override T Read (ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return default;

            if (reader.TokenType == JsonTokenType.String)
            {
                // reads parsed data as string
                return (T)Enum.Parse(typeof(T), reader.GetString()!, true);
            }

            // default serializer
            return System.Text.Json.JsonSerializer.Deserialize<T>(ref reader, options);
        }

        public override void Write (Utf8JsonWriter writer, T value, JsonSerializerOptions _)
            => writer.WriteStringValue(value.ToString());
    }


    public class JsonStringEnumConverter : JsonConverterFactory
        {
        public override bool CanConvert (Type typeToConvert) => typeToConvert.IsEnum;

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            (JsonConverter)Activator.CreateInstance(typeof(JsonStringEnumConverter<>).MakeGenericType(typeToConvert))!;
    }
}