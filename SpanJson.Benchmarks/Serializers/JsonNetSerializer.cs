using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SpanJson.Benchmarks.Serializers
{
    public class JsonNetSerializer : SerializerBase<string>
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
        };
        public override T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public override string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input, Formatting.None, JsonSerializerSettings);
        }
    }
}
