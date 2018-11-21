using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class NullableFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T?, TSymbol>
        where T : struct where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly NullableFormatter<T, TSymbol, TResolver> Default = new NullableFormatter<T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public T? Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return ElementFormatter.Deserialize(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T? value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            ElementFormatter.Serialize(ref writer, value.GetValueOrDefault());
        }
    }
}