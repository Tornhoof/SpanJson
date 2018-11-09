using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class EnumStringFlagsFormatter<T, TSymbol, TResolver> : BaseEnumStringFormatterr<T, TSymbol>, IJsonFormatter<T, TSymbol> where T : Enum
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct, IEquatable<TSymbol>
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate(s => s);
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();
        public static readonly EnumStringFlagsFormatter<T, TSymbol, TResolver> Default = new EnumStringFlagsFormatter<T, TSymbol, TResolver>();
        private static readonly T[] Flags = BuildFlags();



        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            var span = reader.ReadStringSpan();
            var length = span.Length;
            if (length == 0)
            {
                return default;
            }

            ulong result = default;
            var separator = GetSeparator();

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
            var returnValue = Expression.Variable(typeof(ulong), "returnValue");
            var nameSpan = Expression.Parameter(typeof(ReadOnlySpan<TSymbol>), "nameSpan");
            var lengthParameter = Expression.Variable(typeof(int), "length");
            var lengthExpression = Expression.Assign(lengthParameter, Expression.PropertyOrField(nameSpan, "Length"));
            Expression<Action> functor = () => MemoryMarshal.AsBytes(new ReadOnlySpan<char>());
            var asBytesMethodInfo = (functor.Body as MethodCallExpression).Method;
            var nameSpanExpression = Expression.Call(null, asBytesMethodInfo, nameSpan);
            var byteNameSpan = Expression.Variable(typeof(ReadOnlySpan<byte>), "byteNameSpan");
            var parameters = new List<ParameterExpression> {nameSpan, lengthParameter, returnValue, byteNameSpan};
            var assignNameSpan = Expression.Assign(byteNameSpan, nameSpanExpression);
            return BuildDeserializeDelegateExpressions<DeserializeDelegate>(returnValue, assignNameSpan, lengthExpression, lengthParameter, byteNameSpan,
                parameters, nameSpan);
        }

        private static T[] BuildFlags()
        {
            var values = Enum.GetValues(typeof(T));
            var result = new T[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                result[i] = (T) values.GetValue(i);
            }

            return result;
        }


        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        private IEnumerable<T> GetFlags(T input)
        {
            foreach (var flag in Flags)
            {
                if (input.HasFlag(flag))
                {
                    yield return flag;
                }
            }
        }


        private delegate ulong DeserializeDelegate(in ReadOnlySpan<TSymbol> input);
    }
}