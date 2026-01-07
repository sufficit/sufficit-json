using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sufficit.Json
{
    /// <summary>
    /// JSON serializer configuration for Sufficit platform.
    /// 
    /// This class was moved from sufficit-utils to sufficit-base on 2025-01-08
    /// to centralize shared JSON functionality in the base layer, eliminating duplication
    /// and ensuring consistent JSON handling across all projects in the Sufficit ecosystem.
    /// 
    /// This follows the architectural principle where sufficit-base contains core DTOs,
    /// interfaces, models, and shared contracts, while sufficit-utils focuses on
    /// extension methods and helper functions for business logic.
    /// </summary>
    public static partial class JsonSerializer
    {
        /// <summary>
        /// Use default json options
        /// </summary>
        public static JsonSerializerOptions Options { get; } = Generate();

        /// <summary>
        /// If you need an unmodified version
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerOptions Generate()
        {
            var namingPolicy = JsonNamingPolicy.CamelCase;

            var options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
                AllowTrailingCommas = true,
                WriteIndented = false,
                PropertyNamingPolicy = namingPolicy,
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
            };

            // options.Converters.Add(new JsonStringEnumConverter(namingPolicy, true));
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new JsonStringTypeConverter());
            options.Converters.Add(new GuidConverter());
            options.Converters.Add(new NullableGuidConverter());
            // options.Converters.Add(new ExceptionConverter());
            // options.Converters.Add(new RFC2822DateTimeConverter());
            return options;
        }
    }
}