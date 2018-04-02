using System;
using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class ListFormatter<T> : IJsonFormatter<List<T>>
    {
        public static readonly ListFormatter<T> Default = new ListFormatter<T>();
        private static readonly IJsonFormatter<T> DefaultFormatter = DefaultResolver.Default.GetFormatter<T>();

        public void Serialize(ref JsonWriter writer, List<T> value, IJsonFormatterResolver formatterResolver)
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
                DefaultFormatter.Serialize(ref writer, value[0], formatterResolver);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteSeparator();
                    DefaultFormatter.Serialize(ref writer, value[i], formatterResolver);
                }
            }

            writer.WriteArrayEnd();
        }

        public List<T> DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public int AllocSize { get; } = 100;
    }
}