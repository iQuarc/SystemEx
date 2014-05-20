using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace iQuarc.SystemEx
{
    public static class ReflectionExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(object instance)
        {
            return GetAttribute<TAttribute>(instance, false);
        }

        public static TAttribute GetAttribute<TAttribute>(object instance, bool inherit)
        {
            Contract.Requires(instance != null);
            return GetAttribute<TAttribute>(instance.GetType(), inherit);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(object instance)
        {
            return GetAttributes<TAttribute>(instance, false);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(object instance, bool inherit)
        {
            Contract.Requires(instance != null);
            return GetAttributes<TAttribute>(instance.GetType(), inherit);
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return GetAttribute<TAttribute>(attributeProvider, false);
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider, bool inherit)
        {
            Contract.Requires(attributeProvider != null);

            return GetAttributes<TAttribute>(attributeProvider, inherit).FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return GetAttributes<TAttribute>(attributeProvider, false);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider, bool inherit)
        {
            Contract.Requires(attributeProvider != null);
            return attributeProvider.GetCustomAttributes(typeof (TAttribute), inherit).Cast<TAttribute>();
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(this ICustomAttributeProvider attributeProvider, Func<TAttribute, TValue> accessor,
                                                                   TValue defaultValue = default(TValue))
            where TAttribute : class
        {
            Contract.Requires(attributeProvider != null);
            Contract.Requires(accessor != null);

            TAttribute attribute = attributeProvider.GetAttribute<TAttribute>();
            if (attribute != null)
                return accessor(attribute);

            return defaultValue;
        }

        /// <summary>
        ///     Gets all the properties which are editable.
        ///     A property is editable if it has public a getter and a public setter
        /// </summary>
        public static IEnumerable<PropertyInfo> GetEditableProperties(object instance)
        {
            var properties = instance.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                                     .Select(m => m as PropertyInfo)
                                     .Where(p => p != null);

            foreach (var property in properties)
            {
                var getter = property.GetGetMethod();
                var setter = property.GetSetMethod();
                if (getter != null && setter != null)
                    yield return property;
            }
        }

        /// <summary>
        ///     Gets all the properties which are editable and are of simple type
        ///     A property is editable if it has public a getter and a public setter
        ///     A type is simple if it is primitive or value type, or enum or string
        /// </summary>
        public static IEnumerable<PropertyInfo> GetEditableSimpleProperties(object instance)
        {
            return GetEditableProperties(instance).Where(p => IsSimpleType(p.PropertyType));
        }

        /// <summary>
        ///     A type is simple if it is primitive or value type, or enum or string
        /// </summary>
        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive || type.IsValueType || type.IsEnum || type == typeof (String);
        }
    }
}