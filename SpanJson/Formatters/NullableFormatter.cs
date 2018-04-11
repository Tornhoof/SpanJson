using System;
using System.Runtime.CompilerServices;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class NullableFormatter
    {
        public int AllocSize { get; } = 100;

        protected T? Deserialize<T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter,
            TResolver formatterResolver) where T : struct where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return formatter.Deserialize(ref reader, formatterResolver);
        }

        protected void Serialize<T, TResolver>(ref JsonWriter writer, T? value, IJsonFormatter<T, TResolver> formatter,
            TResolver formatterResolver) where T : struct where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            formatter.Serialize(ref writer, value.Value, formatterResolver);
        }
    }

    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class NullableFormatter<T, TResolver> : NullableFormatter, IJsonFormatter<T?, TResolver> where T : struct where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly NullableFormatter<T, TResolver> Default = new NullableFormatter<T, TResolver>();
        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter = StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public T? Deserialize(ref JsonReader reader, TResolver formatterResolver)
        {
            return Deserialize(ref reader, DefaultFormatter, formatterResolver);
        }

        public void Serialize(ref JsonWriter writer, T? value, TResolver formatterResolver)
        {
            Serialize(ref writer, value, DefaultFormatter, formatterResolver);
        }
    }
}