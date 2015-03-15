using System;
using System.Collections.Generic;
using System.Reflection;

namespace NWkHtmlToX.Common.Utilities {
    internal static class ObjectDictionaryMapper {

        private const BindingFlags DEFAULT_BINDING_FLAGS = BindingFlags.Instance |
                                                           BindingFlags.GetProperty |
                                                           BindingFlags.Public;

        internal static IDictionary<string, string> GetDictionary<T>(T instance) {
            return GetDictionary(instance, DEFAULT_BINDING_FLAGS);
        }

        internal static IDictionary<string, string> GetDictionary<T>(T instance, BindingFlags bindingFlags) {
            Guard.ArgumentNotNull(instance, nameof(instance));

            IDictionary<string, string> result = new Dictionary<string, string>();

            GetProperties(result, instance, bindingFlags);

            return result;
        }

        private static void GetProperties<T>(IDictionary<string, string> properties, T instance, BindingFlags bindingFlags, string prefix = null) {
            foreach (var property in instance.GetType().GetProperties(bindingFlags)) {
                var value = property.GetValue(instance);
                if (value == null) continue;

                var propertyType = property.PropertyType;
                var propertyName = prefix == null ? ToCamelCase(property.Name) : String.Concat(prefix, Type.Delimiter, ToCamelCase(property.Name));

                if ((propertyType.IsValueType && !value.Equals(Activator.CreateInstance(propertyType))) || propertyType == typeof(string)) {
                    properties.Add(propertyName, propertyType == typeof(bool) ? value.ToString().ToLowerInvariant() : value.ToString());
                } else {
                    GetProperties(properties, value, bindingFlags, propertyName);
                }
            }
        }

        private static string ToCamelCase(string value) {
            return String.IsNullOrEmpty(value) ? value : String.Concat(Char.ToLowerInvariant(value[0]), value.Substring(1));
        }
    }
}