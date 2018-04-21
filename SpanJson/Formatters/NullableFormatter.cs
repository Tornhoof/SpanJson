using System.Runtime.CompilerServices;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class NullableFormatter : BaseFormatter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static T? Deserialize<T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter)
            where T : struct where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return formatter.Deserialize(ref reader);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void Serialize<T, TResolver>(ref JsonWriter writer, T? value, IJsonFormatter<T, TResolver> formatter)
            where T : struct where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            formatter.Serialize(ref writer, value.Value);
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class NullableFormatter<T, TResolver> : NullableFormatter, IJsonFormatter<T?, TResolver>
        where T : struct where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly NullableFormatter<T, TResolver> Default = new NullableFormatter<T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public T? Deserialize(ref JsonReader reader)
        {
            return Deserialize(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter writer, T? value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}