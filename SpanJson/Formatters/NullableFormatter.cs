using System.Runtime.CompilerServices;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class NullableFormatter : BaseFormatter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static T? Deserialize<T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader, IJsonFormatter<T, TSymbol, TResolver> formatter)
            where T : struct where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            return formatter.Deserialize(ref reader);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void Serialize<T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, T? value, IJsonFormatter<T, TSymbol, TResolver> formatter)
            where T : struct where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            formatter.Serialize(ref writer, value.Value);
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class NullableFormatter<T, TSymbol, TResolver> : NullableFormatter, IJsonFormatter<T?, TSymbol, TResolver>
        where T : struct where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly NullableFormatter<T, TSymbol, TResolver> Default = new NullableFormatter<T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public T? Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T? value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}