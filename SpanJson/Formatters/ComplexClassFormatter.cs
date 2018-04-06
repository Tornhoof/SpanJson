using System;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ComplexClassFormatter<T> : ComplexFormatter, IJsonFormatter<T> where T : class, new()
    {
        public static readonly ComplexClassFormatter<T> Default = new ComplexClassFormatter<T>();
        private static readonly SerializeDelegate<T> Serializer = BuildSerializeDelegate<T>();
        private static readonly DeserializeDelegate<T> Deserializer = BuildDeserializeDelegate<T>();

        public int AllocSize { get; } = EstimateSize<T>();

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            return Deserializer(ref reader, formatterResolver);
        }


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
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