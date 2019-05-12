using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson
{
    public ref struct BufferReader<TSymbol> where TSymbol : struct
    {
        private ReadOnlySpan<TSymbol> _data;
        private ReadOnlySequence<TSymbol> _sequence;
        public int Length;

        public BufferReader(in ReadOnlySpan<TSymbol> data)
        {
            _data = data;
            Length = data.Length;
            _sequence = default;
            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(this._data);
                _bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(this._data);
                _chars = null;
            }
            else
            {
                _chars = default;
                _bytes = default;
            }
        }

        public BufferReader(ReadOnlySequence<TSymbol> sequence)
        {
            _sequence = sequence;
            _data = sequence.FirstSpan;
            Length = _data.Length;

            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(_data);
                _bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(_data);
                _chars = null;
            }
            else
            {
                _chars = default;
                _bytes = default;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void SlideWindow(ref int pos)
        {
            _sequence = _sequence.Slice(pos, _sequence.End);
            _data = _sequence.FirstSpan;
            Length = _data.Length;
            pos = 0;
            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(_data);
                _bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(_data);
                _chars = null;
            }
            else
            {
                _chars = default;
                _bytes = default;
            }
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void EnlargeWindow(ref int pos)
        {
            var nextSequence = _sequence.Slice(pos, _sequence.End);
            _data = _sequence.FirstSpan;
            Length = _data.Length;
            pos = 0;
            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(_data);
                _bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(_data);
                _chars = null;
            }
            else
            {
                _chars = default;
                _bytes = default;
            }
        }

        public ReadOnlySpan<char> Chars
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _chars;
        }
        public ReadOnlySpan<byte> Bytes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _bytes;
        }
        private ReadOnlySpan<char> _chars;
        private ReadOnlySpan<byte> _bytes;

    }
}
