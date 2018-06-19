using Jil;

namespace SpanJson.Benchmarks.Serializers
{
    public class JilSerializer : SerializerBase<string>
    {
        public override T Deserialize<T>(string input)
        {
            return JSON.Deserialize<T>(input, Options.ISO8601ExcludeNullsIncludeInheritedUtc);
        }

        public override string Serialize<T>(T input)
        {
            return JSON.Serialize(input, Options.ISO8601ExcludeNullsIncludeInheritedUtc);
        }
    }
}