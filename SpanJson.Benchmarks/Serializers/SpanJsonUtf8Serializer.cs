namespace SpanJson.Benchmarks.Serializers
{
    public class SpanJsonUtf8Serializer : SerializerBase<byte[]>
    {
        public override byte[] Serialize<TInput>(TInput input)
        {
            return JsonSerializer.Generic.SerializeToByteArray(input);
        }

        public override T Deserialize<T>(byte[] input)
        {
            return JsonSerializer.Generic.Deserialize<T>(input);
        }
    }
}