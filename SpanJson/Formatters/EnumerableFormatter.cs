using System;
using System.Collections.Generic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class EnumerableFormatter<TEnumerable, T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<TEnumerable, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TEnumerable : IEnumerable<T>

    {
        private static readonly Func<T[], TEnumerable> Converter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetEnumerableConvertFunctor<T[], TEnumerable>();

        public static readonly EnumerableFormatter<TEnumerable, T, TSymbol, TResolver> Default = new EnumerableFormatter<TEnumerable, T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();
        private static readonly bool IsRecursionCandidate = RecursionCandidate<T>.IsRecursionCandidate;

        public TEnumerable Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return default;
            }

            var array = ArrayFormatter<T, TSymbol, TResolver>.Default.Deserialize(ref reader);
            return Converter(array);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TEnumerable value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (IsRecursionCandidate)
            {
                writer.IncrementDepth();
            }
            IEnumerator<T> enumerator = null;
            try
            {
                enumerator = value.GetEnumerator();
                writer.WriteBeginArray();
                if (enumerator.MoveNext())
                {
                    // first one, so we can write the separator prior to every following one
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, enumerator.Current, ElementFormatter);
                    // write all the other ones
                    while (enumerator.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, enumerator.Current, ElementFormatter);
                    }
                }
                if (IsRecursionCandidate)
                {
                    writer.DecrementDepth();
                }
                writer.WriteEndArray();
            }
            finally
            {
                enumerator?.Dispose();
            }
        }
    }
}