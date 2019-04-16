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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncJsonWriter(Stream outputStream)
        {
            _outputStream = outputStream;
            _data = ArrayPool<TSymbol>.Shared.Rent(4000);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncJsonWriter(TextWriter outputWriter)
        {
            _outputWriter = outputWriter;
            _data = ArrayPool<TSymbol>.Shared.Rent(4000);
        }

        public JsonWriter<TSymbol> Create()
        {
            return new JsonWriter<TSymbol>(_data, 0, 0);
        }

        public void Dispose()
        {
            if (_data != null)
            {
                ArrayPool<TSymbol>.Shared.Return(_data);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueTask FlushAsync(int length, CancellationToken cancellationToken = default)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                var chars = Unsafe.As<TSymbol[], char[]>(ref _data);
                return new ValueTask(_outputWriter.WriteAsync(chars, 0, length));
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                var bytes = Unsafe.As<TSymbol[], byte[]>(ref _data);
                return new ValueTask(_outputStream.WriteAsync(bytes, 0, length, cancellationToken));
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
