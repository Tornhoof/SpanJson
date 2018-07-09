using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using SpanJson.Formatters;
using SpanJson.Resolvers;
using SpanJson.Shared;

namespace SpanJson.Benchmarks
{
    [ShortRunJob]
    [MemoryDiagnoser]
    public class AsyncListBenchmark
    {
        [Params(10000, 100000, 1000000)] public int Count;

        private const string Value = "Hello World";

        private List<string> _list;

        [GlobalSetup]
        public void Setup()
        {
            _list = new List<string>(Count);
            for (int i = 0; i < Count; i++)
            {
                _list.Add(Value);
            }
        }


        [Benchmark(Baseline = true)]
        public void SyncStringList()
        {
            var writer = new JsonWriter<byte>();
            ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, _list, 0);
        }

        //[Benchmark]
        //public void SyncStringListFile()
        //{
        //    var writer = new JsonWriter<byte>();
        //    ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, _list, 0);
        //    File.WriteAllBytes($"synctest{Count}.bin", writer.Data);
        //}


        [Benchmark]
        public async Task AsyncStringListStreamNull()
        {
            using (var asyncWriter = new AsyncWriter<byte>(Stream.Null))
            {
                await ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.SerializeAsync(asyncWriter, _list, 0);
            }
        }

         [Benchmark]
        public async Task AsyncStringListAsyncStreamNull()
        {
            using (var asyncWriter = new AsyncWriter<byte>(AsyncNullStream.Default))
            {
                await ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.SerializeAsync(asyncWriter, _list, 0);
            }
        }

        //[Benchmark]
        //public async Task AsyncStringListFileStream()
        //{
        //    using (var fs = File.Create($"asynctest{Count}.bin"))
        //    {
        //        using (var asyncWriter = new AsyncWriter<byte>(fs))
        //        {
        //            await ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.SerializeAsync(asyncWriter, _list, 0);
        //        }
        //    }
        //}
    }
}
