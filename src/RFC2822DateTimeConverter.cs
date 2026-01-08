using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sufficit.Json
{
    /// <summary>
    ///     "Fri, 20 Dec 2024 20:19:30 +0000"
    /// </summary>
    public class RFC2822DateTimeConverter : JsonConverter<DateTime?>
    {
        private readonly string _format = "ddd, dd MMM yyyy HH:mm:ss zzz";
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            // Lê o valor da string de data
            string dateString = reader.GetString()!;

            // Converte a string para DateTime, assumindo que está no formato RFC 2822
            return DateTime.ParseExact(dateString, _format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                // Escreve o valor DateTime no formato RFC 2822 (opcional)
                string formattedDate = value.Value.ToString(_format, CultureInfo.InvariantCulture);
                writer.WriteStringValue(formattedDate);
            } 
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}