using System;
using System.Reflection.Metadata.Ecma335;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ArrayFormatter
    {
        public int AllocSize { get; } = 100;

        protected T[] Deserialize<T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter,
            TResolver formatterResolver) where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            // TODO improve
            return ListFormatter<T, TResolver>.Default.Deserialize(ref reader, formatterResolver)?.ToArray();
        }

        protected void Serialize<T, TResolver>(ref JsonWriter writer, T[] value, IJsonFormatter<T, TResolver> formatter,
            TResolver formatterResolver) where TResolver : IJsonFormatterResolver<TResolver>, new()
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
    public sealed class ArrayFormatter<T, TResolver> : ArrayFormatter, IJsonFormatter<T[], TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly ArrayFormatter<T, TResolver> Default = new ArrayFormatter<T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public T[] Deserialize(ref JsonReader reader, TResolver formatterResolver)
        {
            return Deserialize(ref reader, DefaultFormatter, formatterResolver);
        }

        public void Serialize(ref JsonWriter writer, T[] value, TResolver formatterResolver)
        {
            Serialize(ref writer, value, DefaultFormatter, formatterResolver);
        }
    }
}