using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SpanJson.Benchmarks.Serializers
{
    public class SystemTextJsonSerializer : SerializerBase<byte[]>
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = false };

        public override byte[] Serialize<TInput>(TInput input)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(input, Options);
        }

        public override T Deserialize<T>(byte[] input)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(input, Options);
        }
    }
}
