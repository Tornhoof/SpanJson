using System;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ComplexClassFormatter<T, TResolver> : ComplexFormatter, IJsonFormatter<T, TResolver>
        where T : class, new() where TResolver : IJsonFormatterResolver, new()
    {
        public static readonly ComplexClassFormatter<T, TResolver> Default = new ComplexClassFormatter<T, TResolver>();
        private static readonly SerializeDelegate<T, TResolver> Serializer = BuildSerializeDelegate<T, TResolver>();

        private static readonly DeserializeDelegate<T, TResolver> Deserializer =
            BuildDeserializeDelegate<T, TResolver>();

        public int AllocSize { get; } = EstimateSize<T>();

        public T Deserialize(ref JsonReader reader, TResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return Deserializer(ref reader, formatterResolver);
        }


        public void Serialize(ref JsonWriter writer, T value, TResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Serializer(ref writer, value, formatterResolver);
        }
    }
}