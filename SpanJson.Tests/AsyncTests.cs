using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpanJson.Tests
{
    public class AsyncTests
    {
        public class AsyncTestObject : IEquatable<AsyncTestObject>
        {
            public string Text { get; set; }

            public bool Equals(AsyncTestObject other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Text, other.Text);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((AsyncTestObject) obj);
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

        [Fact]
        public async Task SerializeDeserializeGenericUtf16()
        {
            var sb = new StringBuilder();
            var input = new AsyncTestObject {Text = "Hello World"};
            using (var tw = new StringWriter(sb))
            {
                await JsonSerializer.Generic.Utf16.SerializeAsync(input, tw);
            }

            AsyncTestObject deserialized = null;
            using (var tr = new StringReader(sb.ToString()))
            {
                deserialized = await JsonSerializer.Generic.Utf16.DeserializeAsync<AsyncTestObject>(tr);
            }

            Assert.Equal(input, deserialized);
        }

        [Fact]
        public async Task SerializeDeserializeNonGenericUtf16()
        {
            var sb = new StringBuilder();
            var input = new AsyncTestObject {Text = "Hello World"};
            using (var tw = new StringWriter(sb))
            {
                await JsonSerializer.NonGeneric.Utf16.SerializeAsync(input, tw);
            }

            AsyncTestObject deserialized = null;
            using (var tr = new StringReader(sb.ToString()))
            {
                deserialized = (AsyncTestObject) await JsonSerializer.NonGeneric.Utf16.DeserializeAsync(tr, typeof(AsyncTestObject));
            }

            Assert.Equal(input, deserialized);
        }


        [Fact]
        public async Task SerializeDeserializeGenericUtf8MemoryStream()
        {
            var input = Enumerable.Repeat(new AsyncTestObject {Text = "Hello World"}, 10000).ToList();

            using (var ms = new MemoryStream())
            {
                await JsonSerializer.Generic.Utf8.SerializeAsync(input, ms).ConfigureAwait(false);

                ms.Position = 0;

                var deserialized = await JsonSerializer.Generic.Utf8.DeserializeAsync<List<AsyncTestObject>>(ms);
                Assert.Equal(input, deserialized);
            }
        }

        [Fact]
        public async Task SerializeDeserializeNonGenericUtf8MemoryStream()
        {
            var input = Enumerable.Repeat(new AsyncTestObject {Text = "Hello World"}, 10000).ToList();

            using (var ms = new MemoryStream())
            {
                await JsonSerializer.NonGeneric.Utf8.SerializeAsync(input, ms).ConfigureAwait(false);

                ms.Position = 0;

                var deserialized = await JsonSerializer.NonGeneric.Utf8.DeserializeAsync(ms, typeof(List<AsyncTestObject>));
                Assert.Equal(input, deserialized);
            }
        }

        [Fact]
        public async Task SerializeDeserializeGenericUtf8WrappedMemoryStreamSeekable()
        {
            var input = Enumerable.Repeat(new AsyncTestObject {Text = "Hello World"}, 10000).ToList();

            using (var ms = new WrappedMemoryStream(true))
            {
                await JsonSerializer.Generic.Utf8.SerializeAsync(input, ms).ConfigureAwait(false);

                ms.Position = 0;

                var deserialized = await JsonSerializer.Generic.Utf8.DeserializeAsync<List<AsyncTestObject>>(ms);
                Assert.Equal(input, deserialized);
            }
        }

        [Fact]
        public async Task SerializeDeserializeGenericUtf8WrappedMemoryStreamNonSeekable()
        {
            var input = Enumerable.Repeat(new AsyncTestObject {Text = "Hello World"}, 10000).ToList();

            using (var ms = new WrappedMemoryStream(false))
            {
                await JsonSerializer.Generic.Utf8.SerializeAsync(input, ms).ConfigureAwait(false);

                ms.Position = 0;

                var deserialized = await JsonSerializer.Generic.Utf8.DeserializeAsync<List<AsyncTestObject>>(ms);
                Assert.Equal(input, deserialized);
            }
        }

        public class WrappedMemoryStream : Stream
        {
            private readonly MemoryStream _stream = new MemoryStream();

            public WrappedMemoryStream(bool canSeek)
            {
                CanSeek = canSeek;
            }

            public override void Flush()
            {
                _stream.Flush();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                return _stream.Read(buffer, offset, count);
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                return _stream.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                _stream.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                _stream.Write(buffer, offset, count);
            }

            public override bool CanRead => _stream.CanRead;
            public override bool CanSeek { get; }
            public override bool CanWrite => _stream.CanWrite;
            public override long Length => _stream.Length;

            public override long Position
            {
                get => _stream.Position;
                set => _stream.Position = value;
            }

            protected override void Dispose(bool dispose)
            {
                if (dispose)
                {
                    _stream.Dispose();
                }
            }
        }
    }
}