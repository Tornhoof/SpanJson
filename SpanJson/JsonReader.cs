using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref struct JsonReader
    {
        private readonly ReadOnlySpan<char> _chars;
        private static readonly char[] NullTerminator = new[] {'\0'};
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
            return float.Parse(ReadNumberInternal(), NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public double ReadDouble()
        {
            return double.Parse(ReadNumberInternal(), NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<char> ReadNumberInternal()
        {
            SkipWhitespace();
            ref var pos = ref _pos;
            if (TryFindEndOfNumber(pos, out var charsConsumed))
            {
                var result = _chars.Slice(pos, charsConsumed);
                pos += charsConsumed;
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
            var span = ReadStringSpan();
            var pos = 0;
            return ReadCharInternal(span, ref pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static char ReadCharInternal(ReadOnlySpan<char> span, ref int pos)
        {
            if (span.Length == 1)
            {
                return span[pos++];
            }

            if (span[pos] == '\\')
            {
                pos++;
                switch (span[pos++])
                {
                    case '"':
                        return '"';
                    case '\\':
                        return '\\';
                    case 'b':
                        return '\b';
                    case 'f':
                        return '\f';
                    case 'n':
                        return '\n';
                    case 'r':
                        return '\r';
                    case 't':
                        return '\t';
                    case 'U':
                    case 'u':
                        if (span.Length == 6)
                        {
                            var result = (char) int.Parse(span.Slice(2, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                            pos += 4;
                            return result;
                        }

                        break;
                }
            }

            ThrowInvalidOperationException();
            return default;
        }

        public DateTime ReadDateTime()
        {
            var span = ReadStringSpan();
            if (DateTimeParser.TryParseDateTime(span, out var value, out var charsConsumed))
            {
                return value;
            }

            ThrowInvalidOperationException();
            return default;
        }

        public DateTimeOffset ReadDateTimeOffset()
        {            
            var span = ReadStringSpan();
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var charsConsumed))
            {
                return value;
            }

            ThrowInvalidOperationException();
            return default;
        }

        public TimeSpan ReadTimeSpan()
        {
            var span = ReadStringSpan();
            return TimeSpan.Parse(span, CultureInfo.InvariantCulture);
        }

        public Guid ReadGuid()
        {
            var span = ReadStringSpan();
            return Guid.Parse(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadNameSpan()
        {
            var span = ReadStringSpan();
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
            return Unescape(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string Unescape(in ReadOnlySpan<char> span)
        {
            var escapedChars = 0;
            for (var i = 0; i < span.Length; i++)
            {
                if (span[i] == '\\')
                {
                    escapedChars++;
                    i++;
                    if (i < span.Length && span[i] == 'u')
                    {
                        escapedChars += 4; //4 hex digits
                        i += 4;
                    }
                }
            }

            if (escapedChars == 0)
            {
                return span.ToString();
            }

            var unescapedLength = span.Length - escapedChars;
            var result = new string('\0', unescapedLength);
            ref var c = ref MemoryMarshal.GetReference(result.AsSpan());
            var unescapedIndex = 0;
            var index = 0;
            while (index < span.Length)
            {
                var current = span[index++];
                if (current == '\\')
                {
                    current = span[index++];
                    switch (current)
                    {
                        case '"':
                            current = '"';
                            break;
                        case '\\':
                            current = '\\';
                            break;
                        case 'b':
                            current = '\b';
                            break;
                        case 'f':
                            current = '\f';
                            break;
                        case 'n':
                            current = '\n';
                            break;
                        case 'r':
                            current = '\r';
                            break;
                        case 't':
                            current = '\t';
                            break;
                        case 'U':
                        case 'u':
                        {
                            current = (char) int.Parse(span.Slice(index, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                            index += 4;
                            break;
                        }
                    }
                }
                Unsafe.Add(ref c, unescapedIndex++) = current;
            }

            return result;
        }


        /// <summary>
        /// Not escaped
        /// </summary>
        public ReadOnlySpan<char> ReadStringSpan()
        {
            if (ReadIsNull())
            {
                return NullTerminator;
            }
            return ReadStringSpanInternal();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<char> ReadStringSpanInternal()
        {
            ref var pos = ref _pos;
            if (_chars[pos++] != JsonConstant.String)
            {
                ThrowInvalidOperationException();
            }

            if (TryFindEndOfString(pos, out var charsConsumed))
            {
                var result = _chars.Slice(pos, charsConsumed);
                pos += charsConsumed + 1; // skip the '"' too
                return result;
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
            var stringValue = ReadString();
            if (stringValue == null)
            {
                return default;
            }
            return Version.Parse(stringValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Uri ReadUri()
        {
            var stringValue = ReadString();
            if (stringValue == null)
            {
                return default;
            }
            return new Uri(stringValue);
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
                    if (TryFindEndOfNumber(pos, out var charsConsumed))
                    {
                        pos += charsConsumed;
                    }

                    break;
                }
                case JsonToken.String:
                {
                    if (TryFindEndOfString(pos, out var charsConsumed))
                    {
                        pos += charsConsumed + 1; // skip '"' too
                        return;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryFindEndOfNumber(int pos, out int charsConsumed)
        {
            for (var i = pos; i < _chars.Length; i++)
            {
                ref readonly var c = ref _chars[i];
                if (!IsNumericSymbol(c))
                {
                    charsConsumed = i - pos;
                    return true;
                }
            }

            charsConsumed = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryFindEndOfString(int pos, out int charsConsumed)
        {
            for (var i = pos + 1; i < _chars.Length; i++)
            {
                ref readonly var c = ref _chars[i];
                if (c == JsonConstant.String)
                {
                    if (_chars[i - 1] == '\\') // now it could be just an escaped '"'
                    {
                        if (i - 2 >= 0 && _chars[i - 2] != '\\') // it's an escaped string and not just an escaped '\\'
                        {
                            continue;
                        }
                    }

                    charsConsumed = i - pos;
                    return true;
                }
            }

            charsConsumed = default;
            return false;
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
                case '0':
                    return JsonToken.Number;
                default:
                    return JsonToken.None;
            }
        }
    }
}