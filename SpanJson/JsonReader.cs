using System;
using System.Globalization;
using System.Runtime.CompilerServices;

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

        public sbyte ReadSByte()
        {
            return sbyte.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public short ReadInt16()
        {
            return short.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public int ReadInt32()
        {
            return int.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public long ReadInt64()
        {
            return long.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public byte ReadByte()
        {
            return byte.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public ushort ReadUInt16()
        {
            return ushort.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public uint ReadUInt32()
        {
            return uint.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public ulong ReadUInt64()
        {
            return ulong.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public float ReadSingle()
        {
            return float.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        public double ReadDouble()
        {
            return double.Parse(ReadNumberInternal(), provider: CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadNumberInternal()
        {
            SkipWhitespace();
            int i;
            for (i = _pos; i < _chars.Length; i++)
            {
                var c = _chars[i];
                if (!IsNumericSymbol(c))
                {
                    break;
                }
            }

            if (i > _pos)
            {
                var result = _chars.Slice(_pos, i - _pos);
                _pos = i;
                return result;
            }

            ThrowInvalidOperationException();
            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsNumber(char c)
        {
            return '0' <= c && c <= '9';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsNumericSymbol(char c)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '+':
                case '-':
                case '.':
                case 'E':
                case 'e':
                    return true;
            }

            return false;
        }

        public bool ReadBoolean()
        {
            SkipWhitespace();
            if (_chars[_pos] == 't') // just peek the char
            {
                if (_chars[_pos + 1] != 'r')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[_pos + 2] != 'u')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[_pos + 3] != 'e')
                {
                    ThrowInvalidOperationException();
                }

                _pos += 4;
                return true;
            }

            if (_chars[_pos] == 'f') // just peek the char
            {
                if (_chars[_pos + 1] != 'a')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[_pos + 2] != 'l')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[_pos + 3] != 's')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[_pos + 4] != 'e')
                {
                    ThrowInvalidOperationException();
                }

                _pos += 5;
                return false;
            }

            ThrowInvalidOperationException();
            return false;
        }

        public char ReadChar()
        {
            return _chars[_pos++];
        }

        public DateTime ReadDateTime()
        {
            var span = ReadStringSpanInternal();
            return DateTime.Parse(span, CultureInfo.InvariantCulture);
        }

        public DateTimeOffset ReadDateTimeOffset()
        {
            var span = ReadStringSpanInternal();
            return DateTimeOffset.Parse(span, CultureInfo.InvariantCulture);
        }

        public TimeSpan ReadTimeSpan()
        {
            var span = ReadStringSpanInternal();
            return TimeSpan.Parse(span, CultureInfo.InvariantCulture);
        }

        public Guid ReadGuid()
        {
            var span = ReadStringSpanInternal();
            return Guid.Parse(span);
        }

        public string ReadName()
        {
            var span = ReadStringSpanInternal();
            if (_chars[_pos++] != ':')
            {
               ThrowInvalidOperationException();
            }

            return span.ToString();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadNameSpan()
        {
            var span = ReadStringSpanInternal();
            if (_chars[_pos++] != ':')
            {
               ThrowInvalidOperationException();
            }

            return span;
        }


        public string ReadString()
        {
            if (ReadIsNull())
            {
                return null;
            }

            var span = ReadStringSpanInternal();
            return span.ToString();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadStringSpanInternal()
        {
            if (_chars[_pos++] != '"')
            {
                ThrowInvalidOperationException();
            }

            for (int i = _pos; i < _chars.Length; i++)
            {
                var c = _chars[i];
                if (c == '"')
                {
                    if (_chars[i - 1] != '\\')
                    {
                        var length = i - _pos;
                        var result = _chars.Slice(_pos, length);
                        _pos += length + 1; // we skip the '"' too
                        return result;

                    }
                }
            }

            ThrowInvalidOperationException();
            return null;
        }

        public decimal ReadDecimal()
        {
            return decimal.Parse(ReadNumberInternal(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadIsNull()
        {
            SkipWhitespace();
            if (IsAvailable && _chars[_pos] == 'n') // just peek the char
            {
                if (_chars[_pos + 1] != 'u')
                {
                   ThrowInvalidOperationException();
                }

                if (_chars[_pos + 2] != 'l')
                {
                   ThrowInvalidOperationException();
                }

                if (_chars[_pos + 3] != 'l')
                {
                   ThrowInvalidOperationException();
                }

                _pos += 4;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Make sure we don't increase the pointer too much
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadBeginArrayOrThrow()
        {
            if (!ReadBeginArray())
            {
               ThrowInvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadBeginArray()
        {
            SkipWhitespace();
            if (IsAvailable && _chars[_pos] == '[')
            {
                _pos++;
                return true;
            }

            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndArrayOrValueSeparator(ref int count)
        {
            SkipWhitespace();
            if (IsAvailable && _chars[_pos] == ']')
            {
                _pos++;
                return true;
            }

            if (count++ >= 0)
            {
                ReadIsValueSeparator();
            }

            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadIsValueSeparator()
        {
            SkipWhitespace();
            if (IsAvailable && _chars[_pos] == ',')
            {
                _pos++;
                return true;
            }

            return false;
        }

        public bool IsAvailable => _pos < _chars.Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadIsBeginObject()
        {
            SkipWhitespace();
            if (_chars[_pos] == '{')
            {
                _pos++;
                return true;
            }

            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadBeginObjectOrThrow()
        {
            if (!ReadIsBeginObject())
            {
               ThrowInvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadEndObjectOrThrow()
        {
            if (!ReadIsEndObject())
            {
               ThrowInvalidOperationException();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowInvalidOperationException()
        {
           throw new InvalidOperationException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool PeekEndObject()
        {
            SkipWhitespace();
            return _chars[_pos] == '}';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadIsEndObject()
        {
            SkipWhitespace();
            if (_chars[_pos] == '}')
            {
                _pos++;
                return true;
            }

            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndObjectOrValueSeparator(ref int count)
        {
            SkipWhitespace();
            if (IsAvailable && _chars[_pos] == '}')
            {
                _pos++;
                return true;
            }

            if (count++ >= 0)
            {
                ReadIsValueSeparator();
            }

            return false;
        }
    }
}