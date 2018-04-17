namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ComplexClassFormatter<T, TResolver> : ComplexFormatter, IJsonFormatter<T, TResolver>
        where T : class where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly ComplexClassFormatter<T, TResolver> Default = new ComplexClassFormatter<T, TResolver>();

        private static readonly DeserializeDelegate<T, TResolver> Deserializer =
            BuildDeserializeDelegate<T, TResolver>();

        private static readonly SerializeDelegate<T, TResolver> Serializer = BuildSerializeDelegate<T, TResolver>();

        public int AllocSize { get; } = EstimateSize<T>();

        public T Deserialize(ref JsonParser parser)
        {
            if (parser.ReadIsNull())
            {
                return null;
            }

            return Deserializer(ref parser);
        }


        public void Serialize(ref JsonWriter writer, T value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Serializer(ref writer, value);
        }
    }
}