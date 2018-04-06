using System;

namespace SpanJson
{
    public ref struct JsonReader
    {
        private ReadOnlySpan<char> _chars;
        private int _pos;

        public JsonReader(ReadOnlySpan<char> input)
        {
            _chars = input;
            _pos = 0;
        }

        internal sbyte ReadSByte()
        {
            throw new NotImplementedException();
        }

        internal short ReadInt16()
        {
            throw new NotImplementedException();
        }

        internal int ReadInt32()
        {
            throw new NotImplementedException();
        }

        internal long ReadInt64()
        {
            throw new NotImplementedException();
        }

        internal byte ReadByte()
        {
            throw new NotImplementedException();
        }

        internal ushort ReadUInt16()
        {
            throw new NotImplementedException();
        }

        internal uint ReadUInt32()
        {
            throw new NotImplementedException();
        }

        internal ulong ReadUInt64()
        {
            throw new NotImplementedException();
        }

        internal float ReadSingle()
        {
            throw new NotImplementedException();
        }

        internal double ReadDouble()
        {
            throw new NotImplementedException();
        }

        internal bool ReadBoolean()
        {
            throw new NotImplementedException();
        }

        public char ReadChar()
        {
            throw new NotImplementedException();
        }

        public DateTime ReadDateTime()
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset ReadDateTimeOffset()
        {
            throw new NotImplementedException();
        }

        public TimeSpan ReadTimeSpan()
        {
            throw new NotImplementedException();
        }

        public Guid ReadGuid()
        {
            throw new NotImplementedException();
        }

        public string ReadString()
        {
            if (ReadIsNull())
            {
                return null;
            }

            if (_chars[_pos++] != '"')
            {
                throw new InvalidOperationException();
            }

            for (int i = _pos; i < _chars.Length; i++)
            {
                var c = _chars[i];
                if (c == '"')
                {
                    if (_chars[i - 1] != '\\')
                    {
                        var length = i - _pos;
                        var result = _chars.Slice(_pos, length).ToString();
                        _pos += length + 1; // we skip the '"' too
                        return result;
                    }
                }
            }

            throw new InvalidOperationException();
        }

        public decimal ReadDecimal()
        {
            throw new NotImplementedException();
        }

        public bool ReadIsNull()
        {
            SkipWhitespace();
            if (_pos < _chars.Length - 4 && _chars[_pos] == 'n') // just peek the char
            {
                if (_chars[_pos + 1] != 'u')
                {
                    throw new InvalidOperationException();
                }

                if (_chars[_pos + 2] != 'l')
                {
                    throw new InvalidOperationException();
                }

                if (_chars[_pos + 3] != 'l')
                {
                    throw new InvalidOperationException();
                }

                _pos += 4;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Make sure we don't increase the pointer too much
        /// </summary>
        private void SkipWhitespace()
        {
            do
            {
                var c = _chars[_pos];
                switch (c)
                {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        continue;
                    default:
                        return;
                }
            } while (_pos++ < _chars.Length);
        }

        public void ReadBeginArrayOrThrow()
        {
            if (!ReadBeginArray())
            {
                throw new InvalidOperationException();
            }
        }

        public bool ReadBeginArray()
        {
            SkipWhitespace();
            if (_pos < _chars.Length && _chars[_pos] == '[')
            {
                _pos++;
                return true;
            }

            return false;
        }

        public bool TryReadIsEndArrayOrValueSeparator(ref int count)
        {
            SkipWhitespace();
            if (_pos < _chars.Length && _chars[_pos] == ']')
            {
                _pos++;
                return true;
            }

            if (count++ >= 0)
            {
                ReadIsValueSeparatorOrThrow();
            }

            return false;
        }

        public bool ReadIsValueSeparatorOrThrow()
        {
            SkipWhitespace();
            if (_pos < _chars.Length && _chars[_pos] == ',')
            {
                _pos++;
                return true;
            }

            return false;
        }
    }
}