using Jil;

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
             return SpanJson.JsonSerializer.Generic.Serialize(input);
        }
    }
}