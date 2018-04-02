using System;

namespace SpanJson.Formatters
{
    public sealed class ComplexStructFormatter<T> : ComplexFormatter, IJsonFormatter<T> where T : struct
    {
        public static readonly ComplexStructFormatter<T> Default = new ComplexStructFormatter<T>();
        private static readonly SerializationDelegate<T> Delegate = BuildDelegate<T>();


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteObjectStart();
            Delegate(ref writer, value, formatterResolver);
            writer.WriteObjectEnd();
        }

        public T DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }
    }
}