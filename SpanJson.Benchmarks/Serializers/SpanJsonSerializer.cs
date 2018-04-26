namespace SpanJson.Benchmarks.Serializers
{
    public class SpanJsonSerializer : SerializerBase<string>
    {
        public override T Deserialize<T>(string input)
        {
            return JsonSerializer.Generic.Utf16.Deserialize<T>(input);
        }

        public override string Serialize<T>(T input)
        {
            return JsonSerializer.Generic.Utf16.Serialize(input);
        }
    }
}