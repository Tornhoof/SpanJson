using System;
using System.Collections.Generic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public class ReadOnlyDictionaryFormatter<TDictionary, T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<TDictionary, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IReadOnlyDictionary<string, T>
    {
        private static readonly Func<Dictionary<string, T>, TDictionary> Converter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetEnumerableConvertFunctor<Dictionary<string, T>, TDictionary>();

        public static readonly ReadOnlyDictionaryFormatter<TDictionary, T, TSymbol, TResolver> Default =
            new ReadOnlyDictionaryFormatter<TDictionary, T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();
        private static readonly bool IsRecursionCandidate = RecursionCandidate<T>.IsRecursionCandidate;

        public TDictionary Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginObjectOrThrow();
            var result = new Dictionary<string, T>();
            var count = 0;
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var key = reader.ReadEscapedName();
                var value = ElementFormatter.Deserialize(ref reader);
                result[key] = value;
            }

            return Converter(result);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TDictionary value)
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
            var valueLength = value.Count;
            writer.WriteBeginObject();
            if (valueLength > 0)
            {
                var counter = 0;
                foreach (var kvp in value)
                {
                    writer.WriteName(kvp.Key);
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, kvp.Value, ElementFormatter);
                    if (counter++ < valueLength - 1)
                    {
                        writer.WriteValueSeparator();
                    }
                }
            }
            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }
            writer.WriteEndObject();
        }
    }
}