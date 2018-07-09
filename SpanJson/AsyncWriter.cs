using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson
{
    public class AsyncWriter<TSymbol> : IDisposable where TSymbol : struct
    {
        private readonly TextWriter _writer;
        private readonly Stream _stream;
        private TSymbol[] _data;

        public AsyncWriter(Stream stream) : this()
        {
            _stream = stream;
        }

        public AsyncWriter(TextWriter writer) : this()
        {
            _writer = writer;
        }

        private AsyncWriter()
        {
            _data = ArrayPool<TSymbol>.Shared.Rent(8192);
        }

        public int MaxSafeWriteSize => _data.Length - 100;

        public bool SyncMode { get; set; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task FlushAsync(int count, CancellationToken cancellationToken = default)
        {

            if (typeof(TSymbol) == typeof(char))
            {
                var temp = Unsafe.As<TSymbol[], char[]>(ref _data);
                return _writer.WriteAsync(temp, 0, count);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                var temp = Unsafe.As<TSymbol[], byte[]>(ref _data);
                return _stream.WriteAsync(temp, 0, count, cancellationToken);
            }

            ThrowNotSupportedException();
            return default;
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        public JsonWriter<TSymbol> Create()
        {
            return new JsonWriter<TSymbol>(_data);
        }

        public void Dispose()
        {
            if (_data != null)
            {
                ArrayPool<TSymbol>.Shared.Return(_data);
                _data = null;
            }
        }
    }
}
