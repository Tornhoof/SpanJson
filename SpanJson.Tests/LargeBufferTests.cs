using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SpanJson;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.Tests
{
    public class LargeBufferTests
    {


        private static string CreateString(int length)
        {
            const string alphaNumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var array = new char[length];
            Random prng = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = alphaNumeric[prng.Next(alphaNumeric.Length)];
            }

            return new string(array);
        }

        [Fact]
        public void WriteLargeStringUtf8()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var encoded = Encoding.UTF8.GetBytes(testString);
            var jsonWriter = new JsonWriter<byte>(20000);
            jsonWriter.WriteUtf8String(testString);
            var output = jsonWriter.ToByteArray();
            Assert.Equal(encoded, output.AsSpan().Slice(1, output.Length - 2).ToArray());
        }

        [Fact]
        public void WriteLargeStringUtf8Buffered()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var encoded = Encoding.UTF8.GetBytes(testString);
            using (var ms = new MemoryStream())
            {
                using (var bw = new BufferWriter<byte>(ms))
                {
                    var jsonWriter = new JsonWriter<byte>(bw);
                    jsonWriter.WriteUtf8String(testString);
                    int pos = jsonWriter.Position;
                    bw.Flush(ref pos);
                    var output = ms.ToArray();
                    Assert.Equal((byte)'"', output[0]);
                    Assert.Equal((byte) '"', output[output.Length - 1]);
                    Assert.Equal(encoded, output.AsSpan().Slice(1, output.Length - 2).ToArray());
                }
            }
        }

        [Fact]
        public void WriteLargeVerbatimUtf8()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var encoded = Encoding.UTF8.GetBytes(testString);
            var jsonWriter = new JsonWriter<byte>(20000);
            jsonWriter.WriteUtf8Verbatim(encoded);
            var output = jsonWriter.ToByteArray();
            Assert.Equal(encoded, output);
        }

        [Fact]
        public void WriteLargeVerbatimUtf8Buffered()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var encoded = Encoding.UTF8.GetBytes(testString);
            using (var ms = new MemoryStream())
            {
                using (var bw = new BufferWriter<byte>(ms))
                {
                    var jsonWriter = new JsonWriter<byte>(bw);
                    jsonWriter.WriteUtf8Verbatim(encoded);
                    int pos = jsonWriter.Position;
                    bw.Flush(ref pos);
                    var output = ms.ToArray();
                    Assert.Equal(encoded, output);
                }
            }
        }

        [Fact]
        public void WriteLargeNameUtf8()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var encoded = Encoding.UTF8.GetBytes(testString);
            var jsonWriter = new JsonWriter<byte>(20000);
            jsonWriter.WriteUtf8Name(testString);
            var output = jsonWriter.ToByteArray();
            Assert.Equal((byte) '"', output[0]);
            Assert.Equal((byte) '"', output[output.Length - 2]);
            Assert.Equal((byte) ':', output[output.Length - 1]);
            Assert.Equal(encoded, output.AsSpan().Slice(1, output.Length - 3).ToArray());
        }

        [Fact]
        public void WriteLargeNameUtf8Buffered()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var encoded = Encoding.UTF8.GetBytes(testString);
            using (var ms = new MemoryStream())
            {
                using (var bw = new BufferWriter<byte>(ms))
                {
                    var jsonWriter = new JsonWriter<byte>(bw);
                    jsonWriter.WriteUtf8Name(testString);
                    int pos = jsonWriter.Position;
                    bw.Flush(ref pos);
                    var output = ms.ToArray();
                    Assert.Equal((byte)'"', output[0]);
                    Assert.Equal((byte)'"', output[output.Length - 2]);
                    Assert.Equal((byte)':', output[output.Length - 1]);
                    Assert.Equal(encoded, output.AsSpan().Slice(1, output.Length - 3).ToArray());
                }
            }
        }

        [Fact]
        public void WriteLargeStringUtf16()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var jsonWriter = new JsonWriter<char>(20000);
            jsonWriter.WriteUtf16String(testString);
            var output = jsonWriter.ToString();
            output = output.Substring(1, output.Length - 2);
            Assert.Equal(testString, output);
        }

        [Fact]
        public void WriteLargeStringUtf16Buffered()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                using (var bw = new BufferWriter<char>(tw))
                {
                    var jsonWriter = new JsonWriter<char>(bw);
                    jsonWriter.WriteUtf16String(testString);
                    var pos = jsonWriter.Position;
                    bw.Flush(ref pos);
                    Assert.Equal('"', sb[0]);
                    Assert.Equal('"', sb[sb.Length - 1]);
                    var output = sb.ToString(1, sb.Length - 2);
                    Assert.Equal(testString, output);
                }
            }
        }

        [Fact]
        public void WriteLargeNameUtf16()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var jsonWriter = new JsonWriter<char>(20000);
            jsonWriter.WriteUtf16Name(testString);
            var output = jsonWriter.ToString();
            Assert.Equal('"', output[0]);
            Assert.Equal('"', output[output.Length - 2]);
            Assert.Equal(':', output[output.Length - 1]);
            output = output.Substring(1, output.Length - 3);
            Assert.Equal(testString, output);
        }

        [Fact]
        public void WriteLargeNameUtf16Buffered()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                using (var bw = new BufferWriter<char>(tw))
                {
                    var jsonWriter = new JsonWriter<char>(bw);
                    jsonWriter.WriteUtf16Name(testString);
                    var pos = jsonWriter.Position;
                    bw.Flush(ref pos);
                    Assert.Equal('"', sb[0]);
                    Assert.Equal('"', sb[sb.Length - 2]);
                    Assert.Equal(':', sb[sb.Length - 1]);
                    var output = sb.ToString(1, sb.Length - 3);
                    Assert.Equal(testString, output);
                }
            }
        }

        [Fact]
        public void WriteLargeVerbatimUtf16()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var jsonWriter = new JsonWriter<char>(20000);
            jsonWriter.WriteUtf16Verbatim(testString);
            var output = jsonWriter.ToString();
            Assert.Equal(testString, output);
        }

        [Fact]
        public void WriteLargeVerbatimUtf16Buffered()
        {
            // size must be atleast 10000;
            var testString = CreateString(10000);
            var sb = new StringBuilder();
            using (var tw = new StringWriter(sb))
            {
                using (var bw = new BufferWriter<char>(tw))
                {
                    var jsonWriter = new JsonWriter<char>(bw);
                    jsonWriter.WriteUtf16Verbatim(testString);
                    var pos = jsonWriter.Position;
                    bw.Flush(ref pos);
                    var output = sb.ToString();
                    Assert.Equal(testString, output);
                }
            }
        }
    }
}
