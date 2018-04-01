using System;

namespace SpanJson.Formatters
{
    public sealed class ArrayFormatter<T> : IJsonFormatter<T[]>, IJsonCompositeFormatter<T>
    {
        public static readonly ArrayFormatter<T> Default = new ArrayFormatter<T>();
        private static readonly IJsonFormatter<T> DefaultFormatter = FormatterHelper.GetDefaultFormatter<T>();

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
    }
}