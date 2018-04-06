using System;
using System.Reflection.Metadata.Ecma335;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ArrayFormatter
    {
        public int AllocSize { get; } = 100;

        protected T[] Deserialize<T>(ref JsonReader reader, IJsonFormatter<T> formatter,
            IJsonFormatterResolver formatterResolver)
        {
            return ListFormatter<T>.Default.Deserialize(ref reader, formatterResolver)?.ToArray(); // TODO improve
        }

        protected void Serialize<T>(ref JsonWriter writer, T[] value, IJsonFormatter<T> formatter,
            IJsonFormatterResolver formatterResolver)
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
    public sealed class ArrayFormatter<T> : ArrayFormatter, IJsonFormatter<T[]>
    {
        public static readonly ArrayFormatter<T> Default = new ArrayFormatter<T>();
        private static readonly IJsonFormatter<T> DefaultFormatter = DefaultResolver.Default.GetFormatter<T>();

        public T[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DefaultFormatter, formatterResolver);
        }

        public void Serialize(ref JsonWriter writer, T[] value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, DefaultFormatter, formatterResolver);
        }
    }
}