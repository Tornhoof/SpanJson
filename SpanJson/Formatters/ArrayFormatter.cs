using System;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class ArrayFormatter<T> : IJsonFormatter<T[]>
    {
        public static readonly ArrayFormatter<T> Default = new ArrayFormatter<T>();
        private static readonly IJsonFormatter<T> DefaultFormatter = DefaultResolver.Default.GetFormatter<T>();

        public T[] DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter writer, T[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var valueLength = value.Length;
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

        public int AllocSize { get; } = 100;
    }
}