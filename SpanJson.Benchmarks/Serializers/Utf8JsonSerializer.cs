using Utf8Json.Resolvers;

namespace SpanJson.Benchmarks.Serializers
{
    public class Utf8JsonSerializer : SerializerBase<byte[]>
    {
        public override byte[] Serialize<TInput>(TInput input)
        {
            return Utf8Json.JsonSerializer.Serialize(input, StandardResolver.ExcludeNull);
        }

        public override T Deserialize<T>(byte[] input)
        {
            return Utf8Json.JsonSerializer.Deserialize<T>(input, StandardResolver.ExcludeNull);
        }
    }
}