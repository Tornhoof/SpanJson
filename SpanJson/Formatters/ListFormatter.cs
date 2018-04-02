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
            for (var i = 0; i < valueLength; i++)
            {
                DefaultFormatter.Serialize(ref writer, value[i], formatterResolver);
                if (i < valueLength - 1)
                {
                    writer.WriteSeparator();
                }
            }

            writer.WriteArrayEnd();
        }

        public List<T> DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }
    }
}