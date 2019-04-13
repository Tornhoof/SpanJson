using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Shared
{
    public sealed class YieldStream : Stream
    {
        private readonly Stream _wrappedStream;

        public YieldStream(Stream wrappedStream)
        {
            _wrappedStream = wrappedStream;
        }

        public override void Flush()
        {
            _wrappedStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _wrappedStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _wrappedStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _wrappedStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _wrappedStream.Write(buffer, offset, count);
        }

        public override bool CanRead => _wrappedStream.CanRead;

        public override bool CanSeek => _wrappedStream.CanSeek;

        public override bool CanWrite => _wrappedStream.CanWrite;

        public override long Length => _wrappedStream.Length;

        public override long Position
        {
            get => _wrappedStream.Position;
            set => _wrappedStream.Position = value;
        }

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _wrappedStream.WriteAsync(buffer, offset, count, cancellationToken).ContinueWith(t => Task.Yield(), cancellationToken);
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _wrappedStream.ReadAsync(buffer, offset, count, cancellationToken).ContinueWith(t =>
            {
                Task.Yield();
                return t.Result;
            }, cancellationToken);
        }
    }

    public sealed class AsyncNullStream : Stream
    {
        public static readonly AsyncNullStream Default = new AsyncNullStream();
        public override void Flush()
        {
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            await Task.Yield();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return 0;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return 0;
        }

        public override void SetLength(long value)
        {
        }

        public override void Write(byte[] buffer, int offset, int count)
        {

        }

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Yield();
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            await Task.Yield();
            return 0;
        }

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;
        public override long Length => 0;

        public override long Position
        {
            get => 0;
            set { }
        }
    }
}
