using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SpanJson.Helpers;

namespace SpanJson.Formatters
{
    public sealed class EnumStringFlagsFormatter<T, TSymbol, TResolver> : BaseEnumStringFormatterr<T, TSymbol>, IJsonFormatter<T, TSymbol> where T : Enum
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct, IEquatable<TSymbol>
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate(s => s);
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();
        public static readonly EnumStringFlagsFormatter<T, TSymbol, TResolver> Default = new EnumStringFlagsFormatter<T, TSymbol, TResolver>();

        private static readonly ValueTuple<T, ulong>[] Flags = BuildFlags();


        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            var span = reader.ReadStringSpan();
            var length = span.Length;
            if (length == 0)
            {
                return default;
            }

            ulong result = default;
            TSymbol separator;
            if (typeof(TSymbol) == typeof(char))
            {
                var sepChar = ',';
                separator = Unsafe.As<char, TSymbol>(ref sepChar);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                var sepChar = (byte) ',';
                separator = Unsafe.As<byte, TSymbol>(ref sepChar);
            }
            else
            {
                ThrowNotSupportedException();
                return default;
            }

            while (span.Length > 0)
            {
                var index = span.IndexOf(separator);

                if (index != -1)
                {
                    var currentValue = span.Slice(0, index).Trim();
                    result |= Deserializer(currentValue);
                    span = span.Slice(index + 1);
                }
                else
                {
                    span = span.Trim();
                    result |= Deserializer(span);
                    break;
                }
            }

            return (T) Enum.ToObject(typeof(T), result);
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
            return (in ReadOnlySpan<TSymbol> input) => default;
        }

        private static (T, ulong)[] BuildFlags()
        {
            var values = Enum.GetValues(typeof(T));
            var result = new (T, ulong)[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                var value = (T) values.GetValue(i);
                result[i] = (value, Convert.ToUInt64(value));
            }

            return result;
        }


        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        private IEnumerable<T> GetFlags(T input)
        {
            foreach (var valueTuple in Flags)
            {
                var value = Convert.ToUInt64(input);
                if ((value & valueTuple.Item2) == valueTuple.Item2)
                {
                    yield return valueTuple.Item1;
                }
            }
        }


        private delegate ulong DeserializeDelegate(in ReadOnlySpan<TSymbol> input);
    }
}