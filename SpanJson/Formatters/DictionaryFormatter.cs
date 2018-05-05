using System;
using System.Collections.Generic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class DictionaryFormatter : BaseFormatter
    {
        protected static TDictionary Deserialize<TDictionary, T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader,
            IJsonFormatter<T, TSymbol, TResolver> formatter, Func<TDictionary> createFunctor)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IDictionary<string, T>
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginObjectOrThrow();
            var result = createFunctor(); // using new T() is 5-10 times slower
            var count = 0;
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var key = reader.ReadEscapedName();
                var value = formatter.Deserialize(ref reader);
                result[key] = value;
            }

            return result;
        }

        protected static void SerializeRuntimeDecision<TDictionary, T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, TDictionary value,
            IJsonFormatter<T, TSymbol, TResolver> formatter, int nestingLimit)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IDictionary<string, T>
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
                    SerializeRuntimeDecisionInternal(ref writer, kvp.Value, formatter, nextNestingLimit);
                    if (counter++ < valueLength - 1)
                    {
                        writer.WriteValueSeparator();
                    }
                }
            }

            writer.WriteEndObject();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class DictionaryFormatter<TDictionary, T, TSymbol, TResolver> : DictionaryFormatter, IJsonFormatter<TDictionary, TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IDictionary<string, T>
    {
        private static readonly Func<TDictionary> CreateFunctor = BuildCreateFunctor<TDictionary>(typeof(Dictionary<string, T>));
        public static readonly DictionaryFormatter<TDictionary, T, TSymbol, TResolver> Default = new DictionaryFormatter<TDictionary, T, TSymbol, TResolver>();
        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public TDictionary Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize(ref reader, DefaultFormatter, CreateFunctor);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TDictionary value, int nestingLimit)
        {
            SerializeRuntimeDecision(ref writer, value, DefaultFormatter, nestingLimit);
        }
    }
}