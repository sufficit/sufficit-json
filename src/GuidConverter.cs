using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Sufficit.Json
{
    [AutoRegisterConverter]
    public class GuidConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions _)
        {
            // reads parsed data as string
            string? value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                return Guid.Empty;

            // accepts D, N, B and P formats; throws for unrecognized values
            if (Guid.TryParse(value, out var result))
                return result;

            throw new JsonException($"The value '{value}' could not be converted to a Guid.");
        }

        public override void Write(Utf8JsonWriter writer, Guid data, JsonSerializerOptions _)
        {
            // default guid to string representation
            writer.WriteStringValue(data.ToString());
        }
    }
}
