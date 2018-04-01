using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpanJson.Formatters
{
    public static class FormatterHelper
    {
        private static readonly ConcurrentDictionary<Type, IJsonFormatter> Formatters = new ConcurrentDictionary<Type, IJsonFormatter>();

        public static bool TryGetDefaultFormatter<T>(out IJsonFormatter<T> formatter)
        {
            if (TryGetDefaultFormatter(typeof(T), out var jsonFormatter))
            {
                formatter = (IJsonFormatter<T>) jsonFormatter;
                return true;
            }

            formatter = null;
            return false;
        }

        public static bool TryGetDefaultFormatter(Type type, out IJsonFormatter formatter)
        {
            formatter = Formatters.GetOrAdd(type, x => BuildFormatter(x));
            return formatter != null;
        }

        public static IJsonFormatter<T> GetDefaultFormatter<T>()
        {
            return (IJsonFormatter<T>) Formatters.GetOrAdd(typeof(T), x => BuildFormatter(x));
        }

        // Todo improve
        private static IJsonFormatter BuildFormatter(Type inputType)
        {
            Type foundType = null;
            if (inputType.IsArray)
            {
                foundType = typeof(ArrayFormatter<>).MakeGenericType(inputType.GetElementType());
            }
            else if (inputType.IsValueType && Nullable.GetUnderlyingType(inputType) != null)
            {
                foundType = typeof(NullableFormatter<>).MakeGenericType(Nullable.GetUnderlyingType(inputType));
            }
            else if (inputType.IsGenericType && inputType.GetGenericTypeDefinition() == typeof(List<>))
            {
                foundType = typeof(ListFormatter<>).MakeGenericType(inputType.GetGenericArguments()[0]);
            }
            else if (IsIncluded(inputType))
            {
                foundType = Assembly.GetExecutingAssembly().DefinedTypes.FirstOrDefault(a =>
                    typeof(IJsonFormatter<>).MakeGenericType(inputType).IsAssignableFrom(a));
            }
            else
            {
                foundType = typeof(ComplexClassFormatter<>).MakeGenericType(inputType);
            }

            if (foundType == null)
            {
                throw new NotImplementedException("Not integrated.");
            }

            var instance = (IJsonFormatter) foundType
                .GetField("Default", BindingFlags.Static | BindingFlags.Public)
                .GetValue(null);
            return instance;
        }

        private static bool IsIncluded(Type type)
        {
            var extra = new Type[]
                {typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Guid), typeof(string)}; // todo find better wax
            return type.IsPrimitive || extra.Contains(type);
        }
    }
}