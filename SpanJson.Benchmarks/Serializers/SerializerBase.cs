namespace SpanJson.Benchmarks.Serializers
{
    public abstract class SerializerBase
    {
    }

    public abstract class SerializerBase<TOutput> : SerializerBase
    {
        public abstract TOutput Serialize<TInput>(TInput input);

        public abstract T Deserialize<T>(TOutput input);
    }
}