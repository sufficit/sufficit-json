using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sufficit.Json
{
    public static class JsonExtensions
    {
        public static IServiceCollection AddJsonOptions(this IServiceCollection services)
        {
            // Setting default JsonSerializerOptions
            services.TryAddSingleton<JsonSerializerOptions>(Sufficit.Json.JsonSerializer.Options);

            return services;
        }

        public static T? FromJson<T>(this string? source, JsonSerializerOptions? options = null)
            => !string.IsNullOrWhiteSpace(source) ? System.Text.Json.JsonSerializer.Deserialize<T>(source, options ?? Sufficit.Json.JsonSerializer.Options) : default;
        
        public static object? FromJson(this string? source, Type type, JsonSerializerOptions? options = null)
            => !string.IsNullOrWhiteSpace(source) ? System.Text.Json.JsonSerializer.Deserialize(source, type, options ?? Sufficit.Json.JsonSerializer.Options) : default;

        public static string? ToJson(this object? source, JsonSerializerOptions? options = null)
            => source?.ToJson(source.GetType(), options);

        public static string ToJson(this object? source, Type type, JsonSerializerOptions? options = null)
            => System.Text.Json.JsonSerializer.Serialize(source, type, options ?? Sufficit.Json.JsonSerializer.Options);
        
        public static string ToJson<T>(this object? source, JsonSerializerOptions? options = null)
            => source.ToJson(typeof(T), options);

        /// <summary>
        ///     ToJson() without throws exceptions
        /// </summary>
        public static string? ToJsonOrDefault (this object? source)
            => source.ToJsonOrDefault(null);

        /// <summary>
        ///     ToJson() without throws exceptions, with custom json options
        /// </summary>
        public static string? ToJsonOrDefault(this object? source, JsonSerializerOptions? options)
        {
            if (source == null) return null;

            try
            {
                return source.ToJson(options);
            } 
            catch { return null; }
        }

        /// <summary>
        ///     ToJson() without throws exceptions
        /// </summary>
        public static string? ToJsonOrError(this object? source)
            => source.ToJsonOrError(null);

        /// <summary>
        ///     ToJson() without throws exceptions, with custom json options, return error message and type from converter
        /// </summary>
        public static string? ToJsonOrError(this object? source, JsonSerializerOptions? options)
        {
            if (source == null) return null;

            try
            {
                return source.ToJson(options);
            }
            catch (Exception ex) { return $"{{ \"error\": {{ \"type\": \"{ex.GetType()}\", \"message\": \"{ex.Message}\" }} }}"; }
        }

        /// <summary>
        /// Perform a deep Copy of the object, using Json as a serialization method. NOTE: Private members are not cloned using this method.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T CloneJson<T>(this T source) where T : class
        {
            // Don't serialize a null object, simply return the default for that object
            if (source is null) return default!;

            var serialized = source.ToJson<T>();

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var options = new JsonSerializerOptions();
#if NET6_0_OR_GREATER
            options.PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace;
#endif
            return JsonSerializer.Deserialize<T>(serialized, options);
        }

        /// <summary>
        /// Adds all Sufficit JSON converters to the JsonSerializerOptions
        /// </summary>
        public static JsonSerializerOptions AddSufficitConverters(this JsonSerializerOptions options)
        {
            Sufficit.Json.JsonSerializer.AddAutoRegisteredConverters(options.Converters);
            return options;
        }
    }
}
