using System;
using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class DictionaryFormatter : BaseFormatter
    {
        protected static TDictionary Deserialize<TDictionary, T, TResolver>(ref JsonReader reader,
            IJsonFormatter<T, TResolver> formatter, Func<TDictionary> createFunctor)
            where TResolver : IJsonFormatterResolver<TResolver>, new() where TDictionary : class, IDictionary<string, T>
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

        protected static void Serialize<TDictionary, T, TResolver>(ref JsonWriter writer, TDictionary value, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new() where TDictionary : class, IDictionary<string, T>
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var valueLength = value.Count;
            writer.WriteBeginObject();
            if (valueLength > 0)
            {
                var counter = 0;
                foreach (var kvp in value)
                {
                    writer.WriteName(kvp.Key);
                    formatter.Serialize(ref writer, kvp.Value);
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
    public sealed class DictionaryFormatter<TDictionary, T, TResolver> : DictionaryFormatter, IJsonFormatter<TDictionary, TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new() where TDictionary : class, IDictionary<string, T>, new()
    {
        public static readonly DictionaryFormatter<TDictionary, T, TResolver> Default = new DictionaryFormatter<TDictionary, T, TResolver>();
        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter = StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();
        private static readonly Func<TDictionary> CreateFunctor = BuildCreateFunctor<TDictionary>();

        public TDictionary Deserialize(ref JsonReader reader)
        {
            return Deserialize(ref reader, DefaultFormatter, CreateFunctor);
        }

        public void Serialize(ref JsonWriter writer, TDictionary value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}