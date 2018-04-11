using System;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ComplexStructFormatter<T, TResolver> : ComplexFormatter, IJsonFormatter<T, TResolver>
        where T : struct where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly ComplexStructFormatter<T, TResolver>
            Default = new ComplexStructFormatter<T, TResolver>();

        private static readonly SerializeDelegate<T, TResolver> Serializer = BuildSerializeDelegate<T, TResolver>();

        private static readonly DeserializeDelegate<T, TResolver> Deserializer =
            BuildDeserializeDelegate<T, TResolver>();

        public int AllocSize { get; } = EstimateSize<T>();

        public T Deserialize(ref JsonReader reader, TResolver formatterResolver) =>
            Deserializer(ref reader, formatterResolver);


        public void Serialize(ref JsonWriter writer, T value, TResolver formatterResolver) =>
            Serializer(ref writer, value, formatterResolver);
    }
}