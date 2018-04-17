using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson
{
    public abstract class DictionaryFormatter
    {
        public int AllocSize { get; } = 100;

        protected static Dictionary<string, T> Deserialize<T, TResolver>(ref JsonParser parser,
            IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (parser.ReadIsNull())
            {
                return null;
            }

            parser.ReadBeginObjectOrThrow();
            var result = new Dictionary<string, T>();
            var count = 0;
            while (!parser.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var key = parser.ReadEscapedName();
                var value = formatter.Deserialize(ref parser);
                result[key] = value;
            }
            return result;
        }

        protected static void Serialize<T, TResolver>(ref JsonWriter writer, Dictionary<string, T> value, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var valueLength = value.Count;
            writer.WriteObjectStart();
            if (valueLength > 0)
            {
                int counter = 0;
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

            writer.WriteObjectEnd();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class DictionaryFormatter<T, TResolver> : DictionaryFormatter, IJsonFormatter<Dictionary<string, T>, TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly DictionaryFormatter<T, TResolver> Default = new DictionaryFormatter<T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter = StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public Dictionary<string, T> Deserialize(ref JsonParser parser)
        {
            return Deserialize(ref parser, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter writer, Dictionary<string, T> value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}
