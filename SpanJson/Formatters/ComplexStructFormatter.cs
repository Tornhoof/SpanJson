using System;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ComplexStructFormatter<T> : ComplexFormatter, IJsonFormatter<T> where T : struct
    {
        public static readonly ComplexStructFormatter<T> Default = new ComplexStructFormatter<T>();
        private static readonly SerializeDelegate<T> Serializer = BuildSerializeDelegate<T>();
        private static readonly DeserializeDelegate<T> Deserializer = BuildDeserializeDelegate<T>();

        public int AllocSize { get; } = EstimateSize<T>();

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => Deserializer(ref reader, formatterResolver);


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver) => Serializer(ref writer, value, formatterResolver);
    }
}