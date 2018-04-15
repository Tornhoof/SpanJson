using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ArrayFormatter
    {
        public int AllocSize { get; } = 100;

        protected static T[] Deserialize<T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            // TODO improve
            return ListFormatter<T, TResolver>.Default.Deserialize(ref reader)?.ToArray();
        }

        protected static void Serialize<T, TResolver>(ref JsonWriter writer, T[] value, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new()
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
    public sealed class ArrayFormatter<T, TResolver> : ArrayFormatter, IJsonFormatter<T[], TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly ArrayFormatter<T, TResolver> Default = new ArrayFormatter<T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public T[] Deserialize(ref JsonReader reader)
        {
            return Deserialize(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter writer, T[] value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}