using System;
using System.Collections.Generic;
using System.Linq;

namespace SpanJson.Helpers
{
    public static class TypeExtensions
    {
        public static bool TryGetNullableUnderlyingType(this Type type, out Type underlingType)
        {
            if (type.IsValueType)
            {
                underlingType = Nullable.GetUnderlyingType(type);
                return underlingType != null;
            }

            underlingType = default;
            return false;
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsClass || Nullable.GetUnderlyingType(type) != null;
        }

        public static bool TryGetTypeOfGenericInterface(this Type type, Type interfaceType, out Type[] argumentTypes)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"{interfaceType} is not an interface.");
            }

            if (type.IsGenericType && interfaceType.IsAssignableFrom(type.GetGenericTypeDefinition()))
            {
                argumentTypes = type.GetGenericArguments();
                return true;
            }

            var interfaces = type.GetInterfaces();
            var match = interfaces.FirstOrDefault(a => a.IsGenericType && interfaceType.IsAssignableFrom(a.GetGenericTypeDefinition()));
            if(match != null)
            {
                argumentTypes = match.GetGenericArguments();
                return true;
            }

            argumentTypes = default;
            return false;
        }
    }
}