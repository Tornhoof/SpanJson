using System;
using System.Collections.Generic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class DictionaryFormatter<TDictionary, T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<TDictionary, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IDictionary<string, T>
    {
        private static readonly Func<TDictionary> CreateFunctor = BuildCreateFunctor<TDictionary>(typeof(Dictionary<string, T>));
        public static readonly DictionaryFormatter<TDictionary, T, TSymbol, TResolver> Default = new DictionaryFormatter<TDictionary, T, TSymbol, TResolver>();
        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public TDictionary Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginObjectOrThrow();
            var result = CreateFunctor(); // using new T() is 5-10 times slower
            var count = 0;
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var key = reader.ReadEscapedName();
                var value = ElementFormatter.Deserialize(ref reader);
                result[key] = value;
            }

            return result;
        }

        public void Serialize(ref StreamingJsonWriter<TSymbol> writer, TDictionary value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var valueLength = value.Count;
            writer.WriteBeginObject();
            if (valueLength > 0)
            {
                var counter = 0;
                foreach (var kvp in value)
                {
                    writer.WriteName(kvp.Key);
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, kvp.Value, ElementFormatter, nextNestingLimit);
                    if (counter++ < valueLength - 1)
                    {
                        writer.WriteValueSeparator();
                    }
                }
            }

            writer.WriteEndObject();
        }

        public TDictionary Deserialize(ref StreamingJsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginObjectOrThrow();
            var result = CreateFunctor(); // using new T() is 5-10 times slower
            var count = 0;
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var key = reader.ReadEscapedName();
                var value = ElementFormatter.Deserialize(ref reader);
                result[key] = value;
            }
            return result;
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TDictionary value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var valueLength = value.Count;
            writer.WriteBeginObject();
            if (valueLength > 0)
            {
                var counter = 0;
                foreach (var kvp in value)
                {
                    writer.WriteName(kvp.Key);
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, kvp.Value, ElementFormatter, nextNestingLimit);
                    if (counter++ < valueLength - 1)
                    {
                        writer.WriteValueSeparator();
                    }
                }
            }

            writer.WriteEndObject();
        }
    }
}