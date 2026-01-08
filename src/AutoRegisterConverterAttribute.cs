using System;

namespace Sufficit.Json
{
    /// <summary>
    ///     Attribute to mark JSON converters that should be automatically registered
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoRegisterConverterAttribute : Attribute { }
}