using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ListFormatter
    {
        public int AllocSize { get; } = 100;

        protected static TList Deserialize<TList, T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new() where TList : class, IList<T>, new()
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginArrayOrThrow();
            var list = new TList();
            var count = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader));
            }

            return list;
        }

        protected static void Serialize<TList, T, TResolver>(ref JsonWriter writer, TList value,
            IJsonFormatter<T, TResolver> formatter) where TResolver : IJsonFormatterResolver<TResolver>, new() where TList : class, IList<T>, new()
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var valueLength = value.Count;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                formatter.Serialize(ref writer, value[0]);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    formatter.Serialize(ref writer, value[i]);
                }
            }

            writer.WriteEndArray();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ListFormatter<TList, T, TResolver> : ListFormatter, IJsonFormatter<TList, TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new() where TList : class, IList<T>, new()

    {
        public static readonly ListFormatter<TList, T, TResolver> Default = new ListFormatter<TList, T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public TList Deserialize(ref JsonReader reader)
        {
            return Deserialize<TList, T, TResolver>(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter writer, TList value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}