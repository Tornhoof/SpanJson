using System;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ComplexStructFormatter<T> : ComplexFormatter, IJsonFormatter<T> where T : struct
    {
        public static readonly ComplexStructFormatter<T> Default = new ComplexStructFormatter<T>();
        private static readonly SerializationDelegate<T> Delegate = BuildDelegate<T>();

        public int AllocSize { get; } = EstimateSize<T>();

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteObjectStart();
            Delegate(ref writer, value, formatterResolver);
            writer.WriteObjectEnd();
        }
    }
}