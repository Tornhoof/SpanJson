using System;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ComplexStructFormatter<T> : ComplexFormatter<T>, IJsonFormatter<T> where T : struct
    {
        public static readonly ComplexStructFormatter<T> Default = new ComplexStructFormatter<T>();
        private static readonly SerializeDelegate<T> Serializer = BuildSerializeDelegate<T>();

        public int AllocSize { get; } = EstimateSize<T>();

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return DeserializeInternal(ref reader, formatterResolver);
        }


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteObjectStart();
            Serializer(ref writer, value, formatterResolver);
            writer.WriteObjectEnd();
        }
    }
}