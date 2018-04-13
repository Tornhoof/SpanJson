using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref struct JsonReader
    {
        private readonly ReadOnlySpan<char> _chars;
        private int _pos;

        public JsonReader(ReadOnlySpan<char> input)
        {
            _chars = input;
            _pos = 0;
        }

        public sbyte ReadSByte()
        {
            return (sbyte) ReadNumberInt64();
        }

        public short ReadInt16()
        {
            return (short) ReadNumberInt64();
        }

        public int ReadInt32()
        {
            return (int) ReadNumberInt64();
        }

        public long ReadInt64()
        {
            return ReadNumberInt64();
        }

        public byte ReadByte()
        {
            return (byte) ReadNumberUInt64();
        }

        public ushort ReadUInt16()
        {
            return (ushort) ReadNumberUInt64();
        }

        public uint ReadUInt32()
        {
            return (uint) ReadNumberUInt64();
        }

        public ulong ReadUInt64()
        {
            return ReadNumberUInt64();
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
        private ReadOnlySpan<char> ReadNumberInternal()
        {
            SkipWhitespace();
            int i;
            ref var pos = ref _pos;
            for (i = pos; i < _chars.Length; i++)
            {
                ref readonly var c = ref _chars[i];
                if (!IsNumericSymbol(c))
                {
                    break;
                }
            }

            if (i > pos)
            {
                var result = _chars.Slice(pos, i - pos);
                pos = i;
                return result;
            }

            ThrowInvalidOperationException();
            return null;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long ReadNumberInt64()
        {
            SkipWhitespace();
            if (!IsAvailable)
            {
                ThrowInvalidOperationException();
                return default;
            }

            ref var pos = ref _pos;
            ref readonly var firstChar = ref _chars[pos];
            var neg = false;
            switch (firstChar)
            {
                case '-':
                    neg = true;
                    pos++;
                    break;
                case '+':
                    pos++;
                    break;
            }

            if (!IsAvailable)
            {
                ThrowInvalidOperationException();
                return default;
            }

            var result = _chars[pos++] - 48L;
            uint value;
            while (IsAvailable && (value = _chars[pos] - 48U) <= 9)
            {
                result = unchecked(result * 10 + value);
                pos++;
            }

            return neg ? unchecked(-result) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong ReadNumberUInt64()
        {
            SkipWhitespace();
            if (!IsAvailable)
            {
                ThrowInvalidOperationException();
                return default;
            }

            ref var pos = ref _pos;
            ref readonly var firstChar = ref _chars[pos];
            if (firstChar == '+')
            {
                pos++;
            }

            var result = _chars[pos++] - 48UL;
            if (result > 9)
            {
                ThrowInvalidOperationException();
                return default;
            }

            uint value;
            while (IsAvailable && (value = _chars[pos] - 48U) <= 9)
            {
                result = checked(result * 10 + value);
                pos++;
            }

            return result;
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
            ref var pos = ref _pos;
            if (_chars[pos] == JsonConstant.True) // just peek the char
            {
                if (_chars[pos + 1] != 'r')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[pos + 2] != 'u')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[pos + 3] != 'e')
                {
                    ThrowInvalidOperationException();
                }

                pos += 4;
                return true;
            }

            if (_chars[pos] == JsonConstant.False) // just peek the char
            {
                if (_chars[pos + 1] != 'a')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[pos + 2] != 'l')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[pos + 3] != 's')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[pos + 4] != 'e')
                {
                    ThrowInvalidOperationException();
                }

                pos += 5;
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
            if (DateTimeParser.TryParseDateTime(span, out var value, out var charsConsumed))
            {
                return value;
            }

            ThrowInvalidOperationException();
            return default;
        }

        public DateTimeOffset ReadDateTimeOffset()
        {
            var span = ReadStringSpanInternal();
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var charsConsumed))
            {
                return value;
            }

            ThrowInvalidOperationException();
            return default;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadNameSpan()
        {
            var span = ReadStringSpanInternal();
            if (_chars[_pos++] != JsonConstant.NameSeparator)
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
        private ReadOnlySpan<char> ReadStringSpanInternal()
        {
            ref var pos = ref _pos;
            if (_chars[pos++] != JsonConstant.String)
            {
                ThrowInvalidOperationException();
            }

            for (var i = pos; i < _chars.Length; i++)
            {
                ref readonly char c = ref _chars[i];
                if (c == JsonConstant.String)
                {
                    if (_chars[i - 1] != '\\')
                    {
                        var length = i - pos;
                        var result = _chars.Slice(pos, length);
                        pos += length + 1; // we skip the '"' too
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
            ref var pos = ref _pos;
            if (IsAvailable && _chars[pos] == JsonConstant.Null) // just peek the char
            {
                if (_chars[pos + 1] != 'u')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[pos + 2] != 'l')
                {
                    ThrowInvalidOperationException();
                }

                if (_chars[pos + 3] != 'l')
                {
                    ThrowInvalidOperationException();
                }

                pos += 4;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Make sure we don't increase the pointer too much
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SkipWhitespace()
        {
            ref var pos = ref _pos;
            do
            {
                ref readonly var c = ref _chars[pos];
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
            } while (pos++ < _chars.Length);
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
            ref var pos = ref _pos;
            if (IsAvailable && _chars[pos] == JsonConstant.BeginArray)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndArrayOrValueSeparator(ref int count)
        {
            SkipWhitespace();
            ref var pos = ref _pos;
            if (IsAvailable && _chars[pos] == JsonConstant.EndArray)
            {
                pos++;
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
            ref var pos = ref _pos;
            if (IsAvailable && _chars[pos] == JsonConstant.ValueSeparator)
            {
                pos++;
                return true;
            }

            return false;
        }

        public bool IsAvailable => _pos < _chars.Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadIsBeginObject()
        {
            SkipWhitespace();
            ref var pos = ref _pos;
            if (_chars[pos] == JsonConstant.BeginObject)
            {
                pos++;
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
        public bool ReadIsEndObject()
        {
            SkipWhitespace();
            ref var pos = ref _pos;
            if (_chars[pos] == JsonConstant.EndObject)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndObjectOrValueSeparator(ref int count)
        {
            SkipWhitespace();
            ref var pos = ref _pos;
            if (IsAvailable && _chars[pos] == JsonConstant.EndObject)
            {
                pos++;
                return true;
            }

            if (count++ >= 0)
            {
                ReadIsValueSeparator();
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Version ReadVersion()
        {
            var span = ReadString();
            return Version.Parse(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Uri ReadUri()
        {
            var span = ReadString();
            return new Uri(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadNextSegment()
        {
            ReadNextSegment(0);
        }

        private void ReadNextSegment(int stack)
        {
            ref var pos = ref _pos;
            var token = ReadNextToken();
            switch (token)
            {
                case JsonToken.None:
                    break;
                case JsonToken.BeginArray:
                case JsonToken.BeginObject:
                {
                    pos++;
                    ReadNextSegment(stack + 1);
                    break;
                }
                case JsonToken.EndObject:
                case JsonToken.EndArray:
                {
                    pos++;
                    if (stack > 0)
                    {
                        ReadNextSegment(stack - 1);
                    }

                    break;
                }
                case JsonToken.Number:
                case JsonToken.String:
                case JsonToken.True:
                case JsonToken.False:
                case JsonToken.Null:
                case JsonToken.ValueSeparator:
                case JsonToken.NameSeparator:
                {
                    do
                    {
                        ReadNextValue(token);
                        token = ReadNextToken();
                    } while (stack > 0 && (byte) token > 4); // No None or the Begin/End-Array/Object tokens

                    if (stack > 0)
                    {
                        ReadNextSegment(stack);
                    }

                    break;
                }
            }
        }

        private void ReadNextValue(JsonToken token)
        {
            ref var pos = ref _pos;
            switch (token)
            {
                case JsonToken.None:
                    break;
                case JsonToken.BeginObject:
                case JsonToken.EndObject:
                case JsonToken.BeginArray:
                case JsonToken.EndArray:
                case JsonToken.ValueSeparator:
                case JsonToken.NameSeparator:
                    pos++;
                    break;
                case JsonToken.Number:
                {
                    for (var i = pos; i < _chars.Length; i++)
                    {
                        ref readonly var c = ref _chars[i];
                        if (!IsNumericSymbol(c))
                        {
                            pos = i;
                            break;
                        }
                    }

                    break;
                }
                case JsonToken.String:
                {
                    for (var i = pos + 1; i < _chars.Length; i++)
                    {
                        ref readonly var c = ref _chars[i];
                        if (c == JsonConstant.String)
                        {
                            if (_chars[i - 1] != '\\')
                            {
                                pos = i + 1;
                                return;
                            }
                        }
                    }

                    ThrowInvalidOperationException();
                    break;
                }
                case JsonToken.Null:
                case JsonToken.True:
                    pos += 4;
                    break;
                case JsonToken.False:
                    pos += 5;
                    break;
            }
        }

        private JsonToken ReadNextToken()
        {
            SkipWhitespace();
            ref var pos = ref _pos;
            if (pos >= _chars.Length)
            {
                return JsonToken.None;
            }
            ref readonly var c = ref _chars[pos];
            switch (c)
            {
                case JsonConstant.BeginObject:
                    return JsonToken.BeginObject;
                case JsonConstant.EndObject:
                    return JsonToken.EndObject;
                case JsonConstant.BeginArray:
                    return JsonToken.BeginArray;
                case JsonConstant.EndArray:
                    return JsonToken.EndArray;
                case JsonConstant.String:
                    return JsonToken.String;
                case JsonConstant.True:
                    return JsonToken.True;
                case JsonConstant.False:
                    return JsonToken.False;
                case JsonConstant.Null:
                    return JsonToken.Null;
                case JsonConstant.ValueSeparator:
                    return JsonToken.ValueSeparator;
                case JsonConstant.NameSeparator:
                    return JsonToken.NameSeparator;
                case '+':
                case '-':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return JsonToken.Number;
                default:
                    return JsonToken.None;
            }
        }
    }
}