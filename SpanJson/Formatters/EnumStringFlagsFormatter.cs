using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using SpanJson.Helpers;

namespace SpanJson.Formatters
{
    public sealed class EnumStringFlagsFormatter<T, TEnumBase, TSymbol, TResolver> : BaseEnumStringFormatter<T, TSymbol>, IJsonFormatter<T, TSymbol>
        where T : Enum
        where TEnumBase : struct, IComparable, IFormattable
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct, IEquatable<TSymbol>
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate(s => s);
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();
        private static readonly T[] Flags = BuildFlags();

        public static readonly EnumStringFlagsFormatter<T, TEnumBase, TSymbol, TResolver> Default =
            new EnumStringFlagsFormatter<T, TEnumBase, TSymbol, TResolver>();

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            var span = reader.ReadStringSpan();
            var length = span.Length;
            if (length == 0)
            {
                return default;
            }

            TEnumBase result = default;
            var separator = GetSeparator();

            while (span.Length > 0)
            {
                var index = span.IndexOf(separator);

                if (index != -1)
                {
                    var currentValue = span.Slice(0, index).Trim();
                    result = EnumFlagHelpers.Combine(result, Deserializer(currentValue));
                    span = span.Slice(index + 1);
                }
                else
                {
                    span = span.Trim();
                    result = EnumFlagHelpers.Combine(result, Deserializer(span));
                    break;
                }
            }

            return Unsafe.As<TEnumBase, T>(ref result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TSymbol GetSeparator()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                var sepChar = ',';
                return Unsafe.As<char, TSymbol>(ref sepChar);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                var sepChar = (byte) ',';
                return Unsafe.As<byte, TSymbol>(ref sepChar);
            }

            ThrowNotSupportedException();
            return default;
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            writer.WriteDoubleQuote();
            using (var enumerator = GetFlags(value).GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    Serializer(ref writer, enumerator.Current);
                }

                while (enumerator.MoveNext())
                {
                    writer.WriteValueSeparator();
                    Serializer(ref writer, enumerator.Current);
                }
            }

            writer.WriteDoubleQuote();
        }

        private static DeserializeDelegate BuildDeserializeDelegate()
        {
            var nameSpan = Expression.Parameter(typeof(ReadOnlySpan<TSymbol>), "nameSpan");
            Expression nameSpanExpression = nameSpan;
            return BuildDeserializeDelegateExpressions<DeserializeDelegate, TEnumBase>(nameSpan, nameSpanExpression);
        }

        private static T[] BuildFlags()
        {
            var names = Enum.GetNames(typeof(T));
            var result = new T[names.Length];
            for (var i = 0; i < names.Length; i++)
            {
                var value = Enum.Parse(typeof(T), names[i]);
                result[i] = (T) value;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        private IEnumerable<T> GetFlags(T input)
        {
            foreach (var flag in Flags)
            {
                if (EnumFlagHelpers.HasFlag<T, TEnumBase>(input, flag))
                {
                    yield return flag;
                }
            }
        }

        private delegate TEnumBase DeserializeDelegate(ReadOnlySpan<TSymbol> input);
    }
}