using System;
using Jil;

namespace SpanJson.Benchmarks.Serializers
{
    public class JilSerializer : SerializerBase<string>
    {
        public override T Deserialize<T>(string input)
        {
            return Jil.JSON.Deserialize<T>(input, Options.ISO8601);
        }

        public override string Serialize<T>(T input)
        {
            return Jil.JSON.Serialize(input, Options.ISO8601);
        }
    }
}