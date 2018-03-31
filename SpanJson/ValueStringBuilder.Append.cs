using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace SpanJson
{
    internal ref partial struct ValueStringBuilder
    {

        public void Append(bool value)
        {
        }

        public void Append(byte value)
        {
        }

        public void Append(double value)
        {
        }

        public void Append(short value)
        {
        }

        public void Append(int value)
        {
            var pos = _pos;
            var necessarySize = 8; // this is probably not necessary
            if (pos > _chars.Length - necessarySize)
            {
                Grow(necessarySize);
            }

            value.TryFormat(_chars.Slice(pos), out var written);
            _pos += written;
        }

        public void Append(long value)
        {
        }

        public void Append(sbyte value)
        {
        }

        public void Append(float value)
        {
        }

        public void Append(ushort value)
        {
        }

        public void Append(uint value)
        {
        }

        public void Append(ulong value)
        {
        }

        public void Append(DateTime value)
        {
            AppendDoubleQuote();
            var pos = _pos;
            const int dtSize = 35; // Form o + two '"'
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            value.TryFormat(_chars.Slice(pos), out var written, dtFormat, CultureInfo.InvariantCulture);
            _pos += written;
            AppendDoubleQuote();
        }

        public void Append(DateTimeOffset value)
        {
        }

        public void Append(TimeSpan value)
        {
        }

        public void Append(Guid guid)
        {
            var pos = _pos;
            const int guidSize = 42; // Format D + two '"';
            if (pos > _chars.Length - guidSize)
            {
                Grow(guidSize);
            }

            AppendDoubleQuote();
            guid.TryFormat(_chars.Slice(pos), out var written);
            _pos += written;
            AppendDoubleQuote();
        }

        public void Append(string s)
        {
            ref int pos = ref _pos;
            if (s.Length == 1 && pos < (_chars.Length + 2)) // very common case, e.g. appending strings from NumberFormatInfo like separators, percent symbols, etc.
            {
                AppendDoubleQuote();
                _chars[pos++] = s[0];
                AppendDoubleQuote();
            }
            else
            {
                AppendSlow(s);
            }
        }

        private void AppendSlow(string s)
        {
            ref int pos = ref _pos;
            int sLength = s.Length + 2;
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }
            AppendDoubleQuote();
            s.AsSpan().CopyTo(_chars.Slice(pos));
            pos += s.Length;
            AppendDoubleQuote();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AppendDoubleQuote()
        {
            _chars[_pos++] = '"';
        }
    }
}