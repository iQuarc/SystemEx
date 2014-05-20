using System;
using System.Globalization;
using System.Linq;

namespace iQuarc.SystemEx
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Gets the generic interface implemented by this type, which complies with given interface type definition.
        ///     An interface type definition is an open generic type which is an interface.
        /// </summary>
        public static Type GetGenericInterface(this Type type, Type genericInterfaceTypeDefinition)
        {
            if (type == null) throw new ArgumentNullException("type");
            return type.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceTypeDefinition);
        }

        /// <summary>
        ///     Gets the type arguments of the generic interface implemented by this type. The generic interface should match the given interface type definition.
        ///     An interface type definition is an open generic type which is an interface.
        /// </summary>
        public static Type[] GetGenericInterfaceArguments(this Type type, Type genericInterfaceTypeDefinition)
        {
            Type genericType = type.GetGenericInterface(genericInterfaceTypeDefinition);
            if (genericType == null)
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                                                                  "Type '{0}' does not implement '{1}'", type, genericInterfaceTypeDefinition));

            return genericType.GetGenericArguments();
        }
    }
}