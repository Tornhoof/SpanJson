using System;
using System.Runtime.CompilerServices;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class NullableFormatter
    {
        public int AllocSize { get; } = 100;

        protected T? Deserialize<T>(ref JsonReader reader, IJsonFormatter<T> formatter,
            IJsonFormatterResolver formatterResolver) where T : struct
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return formatter.Deserialize(ref reader, formatterResolver);
        }

        protected void Serialize<T>(ref JsonWriter writer, T? value, IJsonFormatter<T> formatter,
            IJsonFormatterResolver formatterResolver) where T : struct
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            formatter.Serialize(ref writer, value.Value, formatterResolver);
        }
    }

    public sealed class NullableFormatter<T> : NullableFormatter, IJsonFormatter<T?> where T : struct
    {
        public static readonly NullableFormatter<T> Default = new NullableFormatter<T>();
        private static readonly IJsonFormatter<T> DefaultFormatter = DefaultResolver.Default.GetFormatter<T>();

        public T? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, DefaultFormatter, formatterResolver);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(ref JsonWriter writer, T? value, IJsonFormatterResolver formatterResolver)
        {
            Serialize(ref writer, value, DefaultFormatter, formatterResolver);
        }
    }
}