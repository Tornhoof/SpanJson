using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using SpanJson.Formatters;
using SpanJson.Helpers;

namespace SpanJson.Resolvers
{
    public sealed class DefaultResolver : IJsonFormatterResolver<DefaultResolver>
    {
        private static readonly ConcurrentDictionary<Type, IJsonFormatter> Formatters =
            new ConcurrentDictionary<Type, IJsonFormatter>();

        public IJsonFormatter<T, DefaultResolver> GetFormatter<T>()
        {
            return (IJsonFormatter<T, DefaultResolver>) GetFormatter(typeof(T));
        }

        public IJsonFormatter<T, TResolver> GetFormatter<T, TResolver>() where TResolver : IJsonFormatterResolver, new()
        {
            return (IJsonFormatter<T, TResolver>) GetFormatter<T>(); // TODO REMOVE
        }

        public IJsonFormatter GetFormatter(Type type)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            return Formatters.GetOrAdd(type, x => BuildFormatter(x));
            // ReSharper restore ConvertClosureToMethodGroup
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
                return GetIntegrated(type) ??
                       GetDefault(typeof(ArrayFormatter<,>).MakeGenericType(type.GetElementType(),
                           typeof(DefaultResolver)));
            }

            if (type.TryGetListType(out var elementType))
            {
                return GetIntegrated(type) ??
                       GetDefault(typeof(ListFormatter<,>).MakeGenericType(elementType, typeof(DefaultResolver)));
            }

            if (type.IsEnum)
            {
                return GetDefault(typeof(EnumFormatter<,>).MakeGenericType(type));
            }

            if (type.TryGetNullableUnderlyingType(out var underlyingType))
            {
                return GetIntegrated(type) ??
                       GetDefault(typeof(NullableFormatter<,>).MakeGenericType(underlyingType,
                           typeof(DefaultResolver)));
            }

            var integrated = GetIntegrated(type);
            if (integrated != null)
            {
                return integrated;
            }

            // no integrated type, let's build it
            if (type.IsValueType)
            {
                return GetDefault(typeof(ComplexStructFormatter<,>).MakeGenericType(type, typeof(DefaultResolver)));
            }

            return GetDefault(typeof(ComplexClassFormatter<,>).MakeGenericType(type, typeof(DefaultResolver)));
        }

        private static IJsonFormatter GetIntegrated(Type type)
        {
            var builtInType = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .FirstOrDefault(a =>
                    typeof(IJsonFormatter<,>).MakeGenericType(type, typeof(DefaultResolver)).IsAssignableFrom(a));
            if (builtInType != null)
            {
                return GetDefault(builtInType);
            }

            return null;
        }
    }
}