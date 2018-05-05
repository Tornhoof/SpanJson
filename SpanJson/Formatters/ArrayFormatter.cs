using System;
using System.Buffers;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ArrayFormatter : BaseFormatter
    {
        protected static T[] Deserialize<T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader, IJsonFormatter<T, TSymbol, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            T[] temp = null;
            T[] result;
            try
            {
                temp = ArrayPool<T>.Shared.Rent(4);
                reader.ReadBeginArrayOrThrow();
                var count = 0;
                while (!reader.TryReadIsEndArrayOrValueSeparator(ref count)) // count is already preincremented, as it counts the separators
                {
                    if (count == temp.Length)
                    {
                        Grow(ref temp);
                    }

                    temp[count - 1] = formatter.Deserialize(ref reader);
                }

                if (count == 0)
                {
                    result = Array.Empty<T>();
                }
                else
                {
                    result = new T[count];
                    Array.Copy(temp, result, count);
                }
            }
            finally
            {
                if (temp != null)
                {
                    ArrayPool<T>.Shared.Return(temp);
                }
            }

            return result;
        }
        /// <summary>
        /// Special case, the included serializers do not need any runtime check
        /// </summary>
        protected static void Serialize<T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, T[] value, IJsonFormatter<T, TSymbol, TResolver> formatter,
            int nestingLimit)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var valueLength = value.Length;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                formatter.Serialize(ref writer, value[0], nextNestingLimit);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    formatter.Serialize(ref writer, value[i], nextNestingLimit);
                }
            }

            writer.WriteEndArray();
        }

        protected static void SerializeRuntimeDecision<T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, T[] value, IJsonFormatter<T, TSymbol, TResolver> formatter,
            int nestingLimit)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var valueLength = value.Length;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                SerializeRuntimeDecisionInternal(ref writer, value[0], formatter, nextNestingLimit);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal(ref writer, value[i], formatter, nextNestingLimit);
                }
            }

            writer.WriteEndArray();
        }

        private static void Grow<T>(ref T[] array)
        {
            var backup = array;
            array = ArrayPool<T>.Shared.Rent(backup.Length * 2);
            backup.CopyTo(array, 0);
            ArrayPool<T>.Shared.Return(backup);
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ArrayFormatter<T, TSymbol, TResolver> : ArrayFormatter, IJsonFormatter<T[], TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly ArrayFormatter<T, TSymbol, TResolver> Default = new ArrayFormatter<T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public T[] Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T[] value, int nestingLimit)
        {
            SerializeRuntimeDecision(ref writer, value, DefaultFormatter, nestingLimit);
        }
    }
}