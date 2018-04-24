using System;
using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class DictionaryFormatter : BaseFormatter
    {
        protected static TDictionary Deserialize<TDictionary, T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader,
            IJsonFormatter<T, TSymbol, TResolver> formatter, Func<TDictionary> createFunctor)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IDictionary<string, T>
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            reader.ReadUtf16BeginObjectOrThrow();
            var result = createFunctor(); // using new T() is 5-10 times slower
            var count = 0;
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var key = reader.ReadUtf16EscapedName();
                var value = formatter.Deserialize(ref reader);
                result[key] = value;
            }

            return result;
        }

        protected static void Serialize<TDictionary, T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, TDictionary value, IJsonFormatter<T, TSymbol, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IDictionary<string, T>
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            var valueLength = value.Count;
            writer.WriteUtf16BeginObject();
            if (valueLength > 0)
            {
                var counter = 0;
                foreach (var kvp in value)
                {
                    writer.WriteUtf16Name(kvp.Key);
                    formatter.Serialize(ref writer, kvp.Value);
                    if (counter++ < valueLength - 1)
                    {
                        writer.WriteUtf16ValueSeparator();
                    }
                }
            }

            writer.WriteUtf16EndObject();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class DictionaryFormatter<TDictionary, T, TSymbol, TResolver> : DictionaryFormatter, IJsonFormatter<TDictionary, TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : class, IDictionary<string, T>
    {
        public static readonly DictionaryFormatter<TDictionary, T, TSymbol, TResolver> Default = new DictionaryFormatter<TDictionary, T, TSymbol, TResolver>();
        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();
        private static readonly Func<TDictionary> CreateFunctor = BuildCreateFunctor<TDictionary>(typeof(Dictionary<string, T>));

        public TDictionary Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize(ref reader, DefaultFormatter, CreateFunctor);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TDictionary value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}