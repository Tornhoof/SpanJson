using System;

namespace SpanJson.Formatters
{
    public sealed class ComplexClassFormatter<T> : ComplexFormatter, IJsonFormatter<T> where T : class
    {
        public static readonly ComplexClassFormatter<T> Default = new ComplexClassFormatter<T>();
        private static readonly SerializationDelegate<T> Delegate = BuildDelegate<T>();


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteObjectStart();
            Delegate(ref writer, value, formatterResolver);
            writer.WriteObjectEnd();
        }

        public T DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public int AllocSize { get; } = EstimateSize<T>();
    }
}