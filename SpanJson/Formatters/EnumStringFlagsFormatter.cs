using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class EnumStringFlagsFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T, TSymbol> where T : Enum
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct, IEquatable<TSymbol>
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate();
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();

        private static DeserializeDelegate BuildDeserializeDelegate()
        {
            return new DeserializeDelegate((in ReadOnlySpan<TSymbol> input) => default);
        }

        public static readonly EnumStringFlagsFormatter<T, TSymbol, TResolver> Default = new EnumStringFlagsFormatter<T, TSymbol, TResolver>();
        private static readonly ValueTuple<T, ulong>[] Flags = BuildFlags();

        private static (T, ulong)[] BuildFlags()
        {
            var values = Enum.GetValues(typeof(T));
            var result = new (T, ulong)[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                var value = (T) values.GetValue(i);
                result[i] = (value, Convert.ToUInt64(value));
            }

            return result;
        }

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            var span = reader.ReadStringSpan();
            int length = span.Length;
            if (length == 0)
            {
                return default;
            }

            ulong result = default;
            TSymbol separator;
            if (typeof(TSymbol) == typeof(char))
            {
                char sepChar = ',';
                separator = Unsafe.As<char, TSymbol>(ref sepChar);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                byte sepChar = (byte) ',';
                separator = Unsafe.As<byte, TSymbol>(ref sepChar);
            }
            else
            {
                ThrowNotSupportedException();
                return default;
            }

            while (span.Length > 0)
            {
                var index = span.IndexOf(separator); // TODO make it work for byte

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


        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
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

        private static SerializeDelegate BuildSerializeDelegate()
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter<TSymbol>).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            MethodInfo writerMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16Verbatim), typeof(string));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8Verbatim), typeof(byte[]));
            }
            else
            {
                throw new NotSupportedException();
            }

            var cases = new List<SwitchCase>();
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                Expression valueConstant;
                var formattedValue = GetFormattedValue(value);
                if (typeof(TSymbol) == typeof(char))
                {
                    valueConstant = Expression.Constant(formattedValue);
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    valueConstant = Expression.Constant(Encoding.UTF8.GetBytes(formattedValue));
                }
                else
                {
                    throw new NotSupportedException();
                }

                var switchCase = Expression.SwitchCase(Expression.Call(writerParameter, writerMethodInfo, valueConstant), Expression.Constant(value));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(valueParameter,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), cases.ToArray());

            var lambdaExpression =
                Expression.Lambda<SerializeDelegate>(switchExpression, writerParameter, valueParameter);
            return lambdaExpression.Compile();
        }

        private static string GetFormattedValue(T enumValue)
        {
            var name = enumValue.ToString();
            return typeof(T).GetMember(name)?.FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? name;
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


        private delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, T value);

        private delegate ulong DeserializeDelegate(in ReadOnlySpan<TSymbol> input);
    }
}