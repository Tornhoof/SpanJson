using System;
using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ListFormatter
    {
        public int AllocSize { get; } = 100;

        protected List<T> Deserialize<T>(ref JsonReader reader, IJsonFormatter<T> formatter,
            IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginArrayOrThrow();
            var list = new List<T>();
            int counter = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref counter))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return list;
        }

        protected void Serialize<T>(ref JsonWriter writer, List<T> value, IJsonFormatter<T> formatter,
            IJsonFormatterResolver formatterResolver)
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
    public sealed class ListFormatter<T> : ListFormatter, IJsonFormatter<List<T>>
    {
        public static readonly ListFormatter<T> Default = new ListFormatter<T>();
        private static readonly IJsonFormatter<T> DefaultFormatter = DefaultResolver.Default.GetFormatter<T>();

        public List<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DefaultFormatter, formatterResolver);
        }

        public void Serialize(ref JsonWriter writer, List<T> value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, DefaultFormatter, formatterResolver);
        }
    }
}