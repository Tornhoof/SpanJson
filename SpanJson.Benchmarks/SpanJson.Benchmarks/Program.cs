using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Jil;
using Newtonsoft.Json;

namespace SpanJson.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }

    [MemoryDiagnoser]
    public class Benchmark
    {
        private static readonly AccessToken accessToken  = new AccessToken
        {
            access_token = "Hello World",
            account_id = 2500,
            expires_on_date = DateTime.Now,
            scope = new List<string> { "Hello", "World" }
        };

        //[Benchmark]
        //public string SerializeJil()
        //{
        //    return JSON.Serialize(accessToken);
        //}

        //[Benchmark]
        //public byte[] SerializeUTF8()
        //{
        //    return Utf8Json.JsonSerializer.Serialize(accessToken);
        //}

        [Benchmark]
        public string SerializeSpanJson()
        {
            return JsonSerializer.Serialize(accessToken);
        }

        //[Benchmark]
        //public string SerializeJsonNet()
        //{
        //    return JsonConvert.SerializeObject(accessToken, Formatting.None);
        //}
    }
}
