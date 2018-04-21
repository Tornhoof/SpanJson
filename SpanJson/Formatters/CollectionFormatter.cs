using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class CollectionFormatter
    {
        public int AllocSize { get; } = 100;

        protected static TCollection Deserialize<TCollection, T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new() where TCollection : class, ICollection<T>, new()
        {
            if (reader.ReadIsNull())
            {
                return default;
            }

            reader.ReadBeginArrayOrThrow();
            var list = new TCollection();
            var count = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader));
            }

            return list;
        }

        protected static void Serialize<TCollection, T, TResolver>(ref JsonWriter writer, TCollection value,
            IJsonFormatter<T, TResolver> formatter) where TResolver : IJsonFormatterResolver<TResolver>, new() where TCollection : class, ICollection<T>, new()
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
                var counter = 0;
                foreach (var entry in value)
                {
                    formatter.Serialize(ref writer, entry);
                    if (counter++ < valueLength - 1)
                    {
                        writer.WriteValueSeparator();
                    }
                }
            }
            writer.WriteEndArray();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class CollectionFormatter<TCollection, T, TResolver> : CollectionFormatter, IJsonFormatter<TCollection, TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new() where TCollection : class, ICollection<T>, new()
    {
        public static readonly CollectionFormatter<TCollection, T, TResolver> Default = new CollectionFormatter<TCollection, T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public TCollection Deserialize(ref JsonReader reader)
        {
            return Deserialize<TCollection, T, TResolver>(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter writer, TCollection value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}