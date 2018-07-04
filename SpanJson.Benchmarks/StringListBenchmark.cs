using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    public class StringListBenchmark
    {
        [Params(1000, 10000)] public int Count;

        [GlobalSetup]
        public void Setup()
        {
            _inputList = Enumerable.Repeat("Hello World and Universe", Count).ToList();
            _inputByteBuffer = JsonSerializer.Generic.Utf8.Serialize(_inputList);
            _inputStringBuffer = JsonSerializer.Generic.Utf16.Serialize(_inputList);
        }

        private List<string> _inputList;
        private byte[] _inputByteBuffer;
        private string _inputStringBuffer;

        [Benchmark]
        public void OldWriterWayUtf8()
        {
            var writer = new JsonWriter<byte>();
            ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, _inputList, 0);
        }

        [Benchmark]
        public void OldWriterWayUtf16()
        {
            var writer = new JsonWriter<char>();
            ListFormatter<List<string>, string, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, _inputList, 0);
        }


        [Benchmark]
        public void NewWriterWayUtf8()
        {
            var writer = new StreamingJsonWriter<byte>(Stream.Null, 2084);
            ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, _inputList, 0);
            writer.FlushAll();
            writer.Return();
        }

        [Benchmark]
        public void NewWriterWayUtf16()
        {
            using (var textWriter = new StreamWriter(Stream.Null))
            {
                var writer = new StreamingJsonWriter<char>(textWriter, 2048);
                ListFormatter<List<string>, string, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, _inputList, 0);
                writer.FlushAll();
                writer.Return();
            }
        }

        [Benchmark]
        public List<string> OldReaderWayUtf8()
        {
            var reader = new JsonReader<byte>(_inputByteBuffer);
            return ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
        }

        [Benchmark]
        public List<string> OldReaderWayUtf16()
        {
            var reader = new JsonReader<char>(_inputStringBuffer);
            return ListFormatter<List<string>, string, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
        }


        [Benchmark]
        public List<string> NewReaderWayUtf8()
        {
            using (var ms = new MemoryStream(_inputByteBuffer, false))
            {
                var reader = new StreamingJsonReader<byte>(ms, 2048);
                reader.Fill(0);
                var result = ListFormatter<List<string>, string, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                reader.Return();
                return result;
            }
        }

        [Benchmark]
        public List<string> NewReaderWayUtf16()
        {
            using (var textReader = new StringReader(_inputStringBuffer))
            {
                var reader = new StreamingJsonReader<char>(textReader, 2048);
                reader.Fill(0);
                var result = ListFormatter<List<string>, string, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                reader.Return();
                return result;
            }
        }
    }
}

