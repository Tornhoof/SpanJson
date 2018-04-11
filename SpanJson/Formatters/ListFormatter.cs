using System;
using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ListFormatter
    {
        public int AllocSize { get; } = 100;

        protected List<T> Deserialize<T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter,
            TResolver formatterResolver) where TResolver : IJsonFormatterResolver, new()
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginArrayOrThrow();
            var list = new List<T>();
            int count = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return list;
        }

        protected void Serialize<T, TResolver>(ref JsonWriter writer, List<T> value, IJsonFormatter<T, TResolver> formatter,
            TResolver formatterResolver) where TResolver : IJsonFormatterResolver, new()
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
                formatter.Serialize(ref writer, value[0], formatterResolver);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteSeparator();
                    formatter.Serialize(ref writer, value[i], formatterResolver);
                }
            }

            writer.WriteArrayEnd();
        }
    }

    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ListFormatter<T, TResolver> : ListFormatter, IJsonFormatter<List<T>, TResolver> where TResolver : IJsonFormatterResolver, new()
    {
        public static readonly ListFormatter<T, TResolver> Default = new ListFormatter<T, TResolver>();
        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter = StandardResolvers.GetResolver<TResolver>().GetFormatter<T, TResolver>();

        public List<T> Deserialize(ref JsonReader reader, TResolver formatterResolver)
        {
            return Deserialize(ref reader, DefaultFormatter, formatterResolver);
        }

        public void Serialize(ref JsonWriter writer, List<T> value, TResolver formatterResolver)
        {
            Serialize(ref writer, value, DefaultFormatter, formatterResolver);
        }
    }
}