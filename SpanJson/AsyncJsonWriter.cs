using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson
{
    public sealed class AsyncJsonWriter<TSymbol> : IDisposable where TSymbol : struct
    {
        private readonly Stream _outputStream;
        private readonly TextWriter _outputWriter;
        private TSymbol[] _data;
        private int _pos;
        private int _depth;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncJsonWriter(Stream outputStream)
        {
            _outputStream = outputStream;
            _data = ArrayPool<TSymbol>.Shared.Rent(4096);
            _pos = 0;
            _depth = 0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncJsonWriter(TextWriter outputWriter)
        {
            _outputWriter = outputWriter;
            _data = ArrayPool<TSymbol>.Shared.Rent(4096);
            _pos = 0;
            _depth = 0;
        }

        public JsonWriter<TSymbol> Create()
        {
            return new JsonWriter<TSymbol>(_data, _pos, _depth);
        }

        public void Dispose()
        {
            if (_data != null)
            {
                ArrayPool<TSymbol>.Shared.Return(_data);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueTask FlushAsync(CancellationToken cancellationToken = default)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                var oldPosition = _pos;
                _pos = 0;
                var chars = Unsafe.As<TSymbol[], char[]>(ref _data);
                return new ValueTask(_outputWriter.WriteAsync(chars, 0, oldPosition));
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                var oldPosition = _pos;
                _pos = 0;
                var bytes = Unsafe.As<TSymbol[], byte[]>(ref _data);
                return new ValueTask(_outputStream.WriteAsync(bytes, 0, oldPosition, cancellationToken));
            }

            ThrowNotSupportedException();
            return default;
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        public int MaxSafeWriteSize => _data.Length - 128;
    }

    public class AsyncJsonReader<TSymbol> where TSymbol : struct
    {

    }
}
