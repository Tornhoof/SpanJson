using System;
using System.Collections.Concurrent;
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

        public IJsonFormatter GetFormatter(Type type)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            return Formatters.GetOrAdd(type, x => BuildFormatter(x));
            // ReSharper restore ConvertClosureToMethodGroup
        }

        private static IJsonFormatter GetDefaultOrCreate(Type type)
        {
            return (IJsonFormatter) (type.GetField("Default", BindingFlags.Public | BindingFlags.Static)
                                         ?.GetValue(null) ?? Activator.CreateInstance(type));
        }

        private static IJsonFormatter BuildFormatter(Type type)
        {
            // todo: support for multidimensional array
            if (type.IsArray)
            {
                return GetIntegrated(type) ??
                       GetDefaultOrCreate(typeof(ArrayFormatter<,>).MakeGenericType(type.GetElementType(),
                           typeof(DefaultResolver)));
            }

            if (type.TryGetListType(out var elementType))
            {
                return GetIntegrated(type) ??
                       GetDefaultOrCreate(
                           typeof(ListFormatter<,>).MakeGenericType(elementType, typeof(DefaultResolver)));
            }

            if (type.IsEnum)
            {
                return GetDefaultOrCreate(typeof(EnumFormatter<,>).MakeGenericType(type, typeof(DefaultResolver)));
            }

            if (type.TryGetNullableUnderlyingType(out var underlyingType))
            {
                return GetIntegrated(type) ??
                       GetDefaultOrCreate(typeof(NullableFormatter<,>).MakeGenericType(underlyingType,
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
                return GetDefaultOrCreate(
                    typeof(ComplexStructFormatter<,>).MakeGenericType(type, typeof(DefaultResolver)));
            }

            return GetDefaultOrCreate(typeof(ComplexClassFormatter<,>).MakeGenericType(type, typeof(DefaultResolver)));
        }

        private static IJsonFormatter GetIntegrated(Type type)
        {
            var allTypes = typeof(DefaultResolver).Assembly.GetTypes();
            foreach (var allType in allTypes)
            {
                if (allType.IsGenericTypeDefinition && allType.ContainsGenericParameters && allType.IsGenericType)
                {
                    var genericArgs = allType.GetGenericArguments();
                    if (genericArgs.Length == 1 && typeof(IJsonFormatterResolver).IsAssignableFrom(genericArgs[0]))
                    {
                        var iface = typeof(IJsonFormatter<,>).MakeGenericType(type, genericArgs[0]);
                        if (iface.IsAssignableFrom(allType))
                        {
                            return GetDefaultOrCreate(allType.MakeGenericType(typeof(DefaultResolver)));
                        }
                    }
                }
            }

            //var builtInType = allTypes.FirstOrDefault(a =>
            //    a.IsGenericTypeDefinition && a.ContainsGenericParameters &&
            //    a.GetGenericArguments().Any(b => b.IsAssignableFrom(typeof(IJsonFormatterResolver))));
            //if (builtInType != null)
            //{
            //    return GetDefaultOrCreate(builtInType);
            //}

            return null;
        }
    }
}