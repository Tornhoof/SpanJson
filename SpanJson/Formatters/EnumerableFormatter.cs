using System;
using System.Collections.Generic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class EnumerableFormatter : BaseFormatter
    {
        protected static TEnumerable Deserialize<TEnumerable, T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader,
            IJsonFormatter<T, TSymbol, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TEnumerable : class, IEnumerable<T>
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            throw new NotImplementedException(); // generic IEnumerable<> deserialization is not supported
        }
        protected static void Serialize<TEnumerable, T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, TEnumerable value,
            IJsonFormatter<T, TSymbol, TResolver> formatter, int nestingLimit) where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
            where TSymbol : struct
            where TEnumerable : class, IEnumerable<T>
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            IEnumerator<T> enumerator = null;
            try
            {
                enumerator = value.GetEnumerator();
                writer.WriteBeginArray();
                if (enumerator.MoveNext())
                {
                    // first one, so we can write the separator prior to every following one
                    SerializeRuntimeDecisionInternal(ref writer, enumerator.Current, formatter, nextNestingLimit);
                    // write all the other ones
                    while (enumerator.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        SerializeRuntimeDecisionInternal(ref writer, enumerator.Current, formatter, nextNestingLimit);
                    }
                }

                writer.WriteEndArray();
            }
            finally
            {
                enumerator?.Dispose();
            }
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class EnumerableFormatter<TEnumerable, T, TSymbol, TResolver> : EnumerableFormatter, IJsonFormatter<TEnumerable, TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TEnumerable : class, IEnumerable<T>

    {
        public static readonly EnumerableFormatter<TEnumerable, T, TSymbol, TResolver> Default = new EnumerableFormatter<TEnumerable, T, TSymbol, TResolver>();


        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public TEnumerable Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize<TEnumerable, T, TSymbol, TResolver>(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TEnumerable value, int nestingLimit)
        {
            Serialize(ref writer, value, DefaultFormatter, nestingLimit);
        }
    }
}