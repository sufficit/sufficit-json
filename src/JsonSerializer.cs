using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        /// Automatically discovers and adds JSON converters marked with AutoRegisterConverterAttribute
        /// from all loaded Sufficit assemblies
        /// </summary>
        public static void AddAutoRegisteredConverters(this IList<JsonConverter> converters)
        {
            var loadedAssemblies = new HashSet<Assembly>();
            
            // First, get all already loaded Sufficit assemblies
            var alreadyLoaded = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name?.StartsWith("Sufficit.") == true)
                .ToArray();
            
            foreach (var assembly in alreadyLoaded)
            {
                loadedAssemblies.Add(assembly);
            }
            
            // Then, try to load additional Sufficit assemblies that might not be loaded yet
            // This is important for test environments where not all assemblies are pre-loaded
            try
            {
                var entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                var assemblyLocation = Path.GetDirectoryName(entryAssembly.Location);
                
                if (assemblyLocation != null)
                {
                    // Look for Sufficit assemblies in the same directory
                    var sufficitAssemblyFiles = Directory.GetFiles(assemblyLocation, "Sufficit.*.dll")
                        .Where(file => !Path.GetFileName(file).StartsWith("Sufficit.EndPointsTests"))
                        .ToArray();
                    
                    foreach (var assemblyFile in sufficitAssemblyFiles)
                    {
                        try
                        {
                            var assemblyName = Path.GetFileNameWithoutExtension(assemblyFile);
                            if (!loadedAssemblies.Any(a => a.GetName().Name == assemblyName))
                            {
                                var assembly = Assembly.LoadFrom(assemblyFile);
                                loadedAssemblies.Add(assembly);
                            }
                        }
                        catch
                        {
                            // Skip assemblies that cannot be loaded
                        }
                    }
                }
            }
            catch
            {
                // If we can't load additional assemblies, continue with what we have
            }
            
            // Process all loaded Sufficit assemblies
            foreach (var assembly in loadedAssemblies.OrderBy(a => a.GetName().Name))
            {
                try
                {
                    var converterTypes = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && 
                                   t.GetCustomAttributes(typeof(AutoRegisterConverterAttribute), false).Any() &&
                                   typeof(JsonConverter).IsAssignableFrom(t));

                    foreach (var converterType in converterTypes)
                    {
                        try
                        {
                            // Check if converter of this type is already added
                            if (!converters.Any(c => c.GetType() == converterType))
                            {
                                var instance = Activator.CreateInstance(converterType);
                                if (instance is JsonConverter converter)
                                {
                                    converters.Add(converter);
                                }
                            }
                        }
                        catch
                        {
                            // Skip converters that cannot be instantiated
                        }
                    }
                }
                catch
                {
                    // Skip assemblies that cannot be loaded or have issues
                }
            }
        }

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

            // Add auto-registered converters from all Sufficit assemblies
            options.Converters.AddAutoRegisteredConverters();
            
            return options;
        }
        
        /// <summary>
        ///     Used from database functions, throws on null
        /// </summary>
        public static T FromJson<T>(string source, JsonSerializerOptions? options = null) where T : class
            => Deserialize<T>(source, options)!;

#if NETSTANDARD2_0
        public static T FromJsonOrDefault<T>(string? source, JsonSerializerOptions? options = null) where T : class
#else
        public static T? FromJsonOrDefault<T>(string? source, JsonSerializerOptions? options = null) where T : class
#endif
        {
            if (string.IsNullOrWhiteSpace(source)) return null;

            try
            {
                return FromJson<T>(source, options);
            }
            catch { return null; }
        }
    }
}