using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Sufficit.Json
{
    /// <summary>
    ///     Frame to facilitate Json Methods
    /// </summary>
    public static partial class JsonSerializer
    {
        /// <summary>
        ///     Wrapper to standard Deserializer Method
        /// </summary>
        public static TValue Deserialize<TValue>(string json, JsonSerializerOptions? options = null)
            => System.Text.Json.JsonSerializer.Deserialize<TValue>(json, options ?? Options) ?? default!;

#if NETSTANDARD2_0
        public static TValue Deserialize<TValue>(JsonNode? json, JsonSerializerOptions? options = null) where TValue : class
#else
        public static TValue? Deserialize<TValue>(JsonNode? json, JsonSerializerOptions? options = null) where TValue : class
#endif

            => System.Text.Json.JsonSerializer.Deserialize<TValue>(json, options ?? Options);

        public static object? Deserialize(string json, Type inputType, JsonSerializerOptions? options = null)
            => System.Text.Json.JsonSerializer.Deserialize(json, inputType, options ?? Options) ?? default!;

        /// <summary>
        ///     Wrapper to standard Serializer Method
        /// </summary>
        public static string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null)
            => System.Text.Json.JsonSerializer.Serialize(value, options ?? Options);

        public static string Serialize<TValue>(TValue value, Type inputType, JsonSerializerOptions? options = null)
            => System.Text.Json.JsonSerializer.Serialize(value, inputType, options ?? Options);

        /// <summary>
        ///     Wrapper to standard Deserializer Method
        /// </summary>
        public static JsonElement SerializeToElement<TValue>(TValue value, Type inputType, JsonSerializerOptions? options = null)
            => System.Text.Json.JsonSerializer.SerializeToElement(value, inputType, options ?? Options);

        /// <summary>
        ///     Wrapper to standard Deserializer Method
        /// </summary>
        public static JsonElement SerializeToElement<TValue>(TValue value, JsonSerializerOptions? options = null)
            => System.Text.Json.JsonSerializer.SerializeToElement<TValue>(value, options ?? Options);
    }
}
