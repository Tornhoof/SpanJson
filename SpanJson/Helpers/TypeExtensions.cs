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

        public static bool TryGetListType(this Type type, out Type elementType)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                elementType = type.GetGenericArguments()[0];
                return true;
            }

            elementType = default;
            return false;
        }

        public static bool TryGetDictionaryType(this Type type, out Type keyType, out Type valueType)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Any(a => a.IsGenericType && typeof(IDictionary<,>).IsAssignableFrom(a.GetGenericTypeDefinition())))
            {
                keyType = type.GetGenericArguments()[0];
                valueType = type.GetGenericArguments()[1];
                return true;
            }

            keyType = default;
            valueType = default;
            return false;
        }
    }
}