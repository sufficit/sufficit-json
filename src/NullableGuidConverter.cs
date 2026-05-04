using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Sufficit.Json
{
    [AutoRegisterConverter]
    public class NullableGuidConverter : JsonConverter<Guid?>
    {
        public override Guid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions _)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            // reads parsed data as string
            string? value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                return null;

            // accepts D, N, B and P formats; throws for unrecognized values
            if (Guid.TryParse(value, out var result))
                return result;

            throw new JsonException($"The value '{value}' could not be converted to a Guid.");
        }

        public override void Write(Utf8JsonWriter writer, Guid? data, JsonSerializerOptions _)
        {
            if (data.HasValue)
            {
                // default guid to string representation
                string formatted = data.Value.ToString();
                writer.WriteStringValue(formatted);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
