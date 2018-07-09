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
        private readonly TSymbol[] _data;
        private readonly byte[] _byteData;
        private readonly char[] _charData;

        public int Position { get; set; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncWriter(Stream stream) : this()
        {
            _stream = stream;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncWriter(TextWriter writer) : this()
        {
            _writer = writer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AsyncWriter()
        {
            _data = ArrayPool<TSymbol>.Shared.Rent(8192);

            if (typeof(TSymbol) == typeof(char))
            {
                _charData = Unsafe.As<TSymbol[], char[]>(ref _data);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _byteData = Unsafe.As<TSymbol[], byte[]>(ref _data);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        public int MaxSafeWriteSize => _data.Length - 100;

        public bool SyncMode { get; set; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public async Task FlushAsync(CancellationToken cancellationToken = default)
        {

            if (typeof(TSymbol) == typeof(char))
            {
                await _writer.WriteAsync(_charData, 0, Position).ConfigureAwait(false);
                Position = 0;
            }

            else if (typeof(TSymbol) == typeof(byte))
            {
                await _stream.WriteAsync(_byteData, 0, Position, cancellationToken).ConfigureAwait(false);
                Position = 0;
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        public JsonWriter<TSymbol> Create()
        {
            return new JsonWriter<TSymbol>(_data, Position);
        }

        public void Dispose()
        {
            if (_data != null)
            {
                ArrayPool<TSymbol>.Shared.Return(_data);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteValueSeparator()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                _charData[Position++] = JsonUtf16Constant.ValueSeparator;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _byteData[Position++] = JsonUtf8Constant.ValueSeparator;
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginArray()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                _charData[Position++] = JsonUtf16Constant.BeginArray;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _byteData[Position++] = JsonUtf8Constant.BeginArray;
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndArray()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                _charData[Position++] = JsonUtf16Constant.EndArray;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _byteData[Position++] = JsonUtf8Constant.EndArray;
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNull()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                _charData[Position++] = 'n';
                _charData[Position++] = 'u';
                _charData[Position++] = 'l';
                _charData[Position++] = 'l';
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _byteData[Position++] = (byte) 'n';
                _byteData[Position++] = (byte) 'u';
                _byteData[Position++] = (byte) 'l';
                _byteData[Position++] = (byte) 'l';
            }
            else
            {
                ThrowNotSupportedException();
            }
        }
    }
}
