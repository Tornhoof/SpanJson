namespace SpanJson.Benchmarks.Serializers
{
    public class SpanJsonSerializer : SerializerBase<string>
    {
        public override T Deserialize<T>(string input)
        {
            return default;
        }

        public override string Serialize<T>(T input)
        {
            return JsonSerializer.Generic.Serialize(input);
        }
    }
}