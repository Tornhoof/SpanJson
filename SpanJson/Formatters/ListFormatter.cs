using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ListFormatter
    {
        public int AllocSize { get; } = 100;

        protected static List<T> Deserialize<T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (reader.ReadIsNull()) return null;

            reader.ReadBeginArrayOrThrow();
            var list = new List<T>();
            var count = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader));
            }

            return list;
        }

        protected static void Serialize<T, TResolver>(ref JsonWriter writer, List<T> value,
            IJsonFormatter<T, TResolver> formatter) where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var valueLength = value.Count;
            writer.WriteArrayStart();
            if (valueLength > 0)
            {
                formatter.Serialize(ref writer, value[0]);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    formatter.Serialize(ref writer, value[i]);
                }
            }

            writer.WriteArrayEnd();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ListFormatter<T, TResolver> : ListFormatter, IJsonFormatter<List<T>, TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly ListFormatter<T, TResolver> Default = new ListFormatter<T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public List<T> Deserialize(ref JsonReader reader)
        {
            return Deserialize(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter writer, List<T> value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}