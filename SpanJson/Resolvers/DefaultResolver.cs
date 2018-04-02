using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using SpanJson.Formatters;
using SpanJson.Helpers;

namespace SpanJson.Resolvers
{
    public sealed class DefaultResolver : IJsonFormatterResolver
    {
        private static readonly ConcurrentDictionary<Type, IJsonFormatter> Formatters =
            new ConcurrentDictionary<Type, IJsonFormatter>();

        public static readonly DefaultResolver Default = new DefaultResolver();

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return (IJsonFormatter<T>) GetFormatter(typeof(T));
        }

        public IJsonFormatter GetFormatter(Type type)
        {
            return Formatters.GetOrAdd(type, x => BuildFormatter(x));
        }

        private static IJsonFormatter GetDefault(Type type)
        {
            return (IJsonFormatter) type.GetField("Default", BindingFlags.Public | BindingFlags.Static).GetValue(null);
        }

        private static IJsonFormatter BuildFormatter(Type type)
        {
            // todo: support for multidimensional array
            if (type.IsArray)
            {
                return GetDefault(typeof(ArrayFormatter<>).MakeGenericType(type.GetElementType()));
            }

            if (type.TryGetListType(out var elementType))
            {
                return GetDefault(typeof(ListFormatter<>).MakeGenericType(elementType));
            }

            if (type.IsEnum)
            {
                return GetDefault(typeof(EnumFormatter<>).MakeGenericType(type));
            }

            if (type.TryGetNullableUnderlyingType(out var underlyingType))
            {
                return GetDefault(typeof(NullableFormatter<>).MakeGenericType(underlyingType));
            }

            var builtInType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(a => typeof(IJsonFormatter<>).MakeGenericType(type).IsAssignableFrom(a));
            if (builtInType != null)
            {
                return GetDefault(builtInType);
            }

            // no integrated type, let's build it
            if (type.IsValueType)
            {
                return GetDefault(typeof(ComplexStructFormatter<>).MakeGenericType(type));
            }

            return GetDefault(typeof(ComplexClassFormatter<>).MakeGenericType(type));
        }
    }
}