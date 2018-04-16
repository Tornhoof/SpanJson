using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using SpanJson.Formatters.Dynamic;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref struct JsonReader
    {
        private readonly ReadOnlySpan<char> _chars;
        private static readonly char[] NullTerminator = new[] { '\0' };
        private int _pos;
        public JsonReader(ReadOnlySpan<char> input)
        {
            _chars = input;
            _pos = 0;
        }

        public sbyte ReadSByte()
        {
            return (sbyte)ReadNumberInt64();
        }

        public short ReadInt16()
        {
            return (short)ReadNumberInt64();
        }

        public int ReadInt32()
        {
            return (int)ReadNumberInt64();
        }

        public long ReadInt64()
        {
            return ReadNumberInt64();
        }

        public byte ReadByte()
        {
            return (byte)ReadNumberUInt64();
        }

        public ushort ReadUInt16()
        {
            return (ushort)ReadNumberUInt64();
        }

        public uint ReadUInt32()
        {
            return (uint)ReadNumberUInt64();
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
            ThrowJsonFormatException(JsonFormatException.FormatError.EndOfData);
            return null;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long ReadNumberInt64()
        {
            SkipWhitespace();
            if (!IsAvailable)
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.EndOfData);
                return default;
            }

            ref var pos = ref _pos;
            ref readonly var firstChar = ref _chars[pos];
            var neg = false;
            if (firstChar == '-')
            {                
                pos++;
                neg = true;
            }
            if (!IsAvailable)
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.EndOfData);
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
                ThrowJsonFormatException(JsonFormatException.FormatError.EndOfData);
                return default;
            }

            ref var pos = ref _pos;
            ref readonly var firstChar = ref _chars[pos];
            if (firstChar == '-')
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.InvalidNumberFormat);
                return default;
            }

            var result = _chars[pos++] - 48UL;
            if (result > 9)
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.InvalidNumberFormat);
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
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
                }

                if (_chars[pos + 2] != 'u')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
                }

                if (_chars[pos + 3] != 'e')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
                }

                pos += 4;
                return true;
            }

            if (_chars[pos] == JsonConstant.False) // just peek the char
            {
                if (_chars[pos + 1] != 'a')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
                }

                if (_chars[pos + 2] != 'l')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
                }

                if (_chars[pos + 3] != 's')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
                }

                if (_chars[pos + 4] != 'e')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
                }

                pos += 5;
                return false;
            }

            ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(bool));
            return false;
        }

        public char ReadChar()
        {
            var span = ReadStringSpan();
            var pos = 0;
            return ReadCharInternal(span, ref pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char ReadCharInternal(ReadOnlySpan<char> span, ref int pos)
        {
            if (span.Length == 1)
            {
                return span[pos++];
            }

            if (span[pos] == JsonConstant.Escape)
            {
                pos++;
                switch (span[pos++])
                {
                    case JsonConstant.DoubleQuote:
                        return JsonConstant.DoubleQuote;
                    case JsonConstant.Escape:
                        return JsonConstant.Escape;
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
                            var result = (char)int.Parse(span.Slice(2, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                            pos += 4;
                            return result;
                        }

                        break;
                }
            }

            ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(char));
            return default;
        }

        public DateTime ReadDateTime()
        {
            var span = ReadStringSpan();
            if (DateTimeParser.TryParseDateTime(span, out var value, out var charsConsumed))
            {
                return value;
            }

            ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(DateTime));
            return default;
        }

        public DateTimeOffset ReadDateTimeOffset()
        {
            var span = ReadStringSpan();
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var charsConsumed))
            {
                return value;
            }

            ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(DateTimeOffset));
            return default;
        }

        public TimeSpan ReadTimeSpan()
        {
            var span = ReadStringSpan();
            if (TimeSpan.TryParse(span, CultureInfo.InvariantCulture, out var result))
            {
                return result;
            }
            ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(TimeSpan));
            return default;
        }

        public Guid ReadGuid()
        {
            var span = ReadStringSpan();
            if (Guid.TryParse(span, out var result))
            {
                return result;
            }
            ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol, typeof(Guid));
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadNameSpan()
        {
            var span = ReadStringSpan();
            if (_chars[_pos++] != JsonConstant.NameSeparator)
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedDoubleQuote);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadString()
        {
            if (ReadIsNull())
            {
                return null;
            }

            var span = ReadStringSpanInternal(out var escapedChars);
            return escapedChars == 0 ? span.ToString() : Unescape(span, escapedChars);
        }

        private string Unescape(in ReadOnlySpan<char> span, int escapedChars)
        {
            var unescapedLength = span.Length - escapedChars;
            var result = new string('\0', unescapedLength);
            ref var c = ref MemoryMarshal.GetReference(result.AsSpan());
            var unescapedIndex = 0;
            var index = 0;
            while (index < span.Length)
            {
                var current = span[index++];
                if (current == JsonConstant.Escape)
                {
                    current = span[index++];
                    switch (current)
                    {
                        case JsonConstant.DoubleQuote:
                            current = JsonConstant.DoubleQuote;
                            break;
                        case JsonConstant.Escape:
                            current = JsonConstant.Escape;
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
                                current = (char)int.Parse(span.Slice(index, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                                index += 4;
                                break;
                            }

                        default:
                            ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol);
                            break;
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
            return ReadStringSpanInternal(out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<char> ReadStringSpanInternal(out int escapedChars)
        {
            ref var pos = ref _pos;
            if (_chars[pos++] != JsonConstant.String)
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedDoubleQuote);
            }

            // We should also get info about how many escaped chars exist from here
            if (TryFindEndOfString(pos, out var charsConsumed, out escapedChars))
            {
                var result = _chars.Slice(pos, charsConsumed);
                pos += charsConsumed + 1; // skip the JsonConstant.DoubleQuote too
                return result;
            }

            ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedDoubleQuote);
            return null;
        }

        /// <summary>
        /// Includes the quotes on each end
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<char> ReadStringSpanWithQuotes(out int escapedChars)
        {
            ref var pos = ref _pos;
            if (_chars[pos] != JsonConstant.String)
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedDoubleQuote);
            }

            // We should also get info about how many escaped chars exist from here
            if (TryFindEndOfString(pos + 1, out var charsConsumed, out escapedChars))
            {
                var result = _chars.Slice(pos, charsConsumed + 2); // we include quotes in this version
                pos += charsConsumed +2;  // skip both JsonConstant.DoubleQuote too 
                return result;
            }

            ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedDoubleQuote);
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
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol);
                }

                if (_chars[pos + 2] != 'l')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol);
                }

                if (_chars[pos + 3] != 'l')
                {
                    ThrowJsonFormatException(JsonFormatException.FormatError.InvalidSymbol);
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
                ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedBeginArray);
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
                ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedBeginObject);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadEndObjectOrThrow()
        {
            if (!ReadIsEndObject())
            {
                ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedEndObject);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowJsonFormatException(JsonFormatException.FormatError error, Type type)
        {
            throw new JsonFormatException(error, type, _pos);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowJsonFormatException(JsonFormatException.FormatError error)
        {
            throw new JsonFormatException(error, _pos);
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
        public void SkipNextSegment()
        {
            SkipNextSegment(0);
        }

        private void SkipNextSegment(int stack)
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
                        SkipNextSegment(stack + 1);
                        break;
                    }
                case JsonToken.EndObject:
                case JsonToken.EndArray:
                    {
                        pos++;
                        if (stack > 0)
                        {
                            SkipNextSegment(stack - 1);
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
                            SkipNextValue(token);
                            token = ReadNextToken();
                        } while (stack > 0 && (byte)token > 4); // No None or the Begin/End-Array/Object tokens

                        if (stack > 0)
                        {
                            SkipNextSegment(stack);
                        }

                        break;
                    }
            }
        }

        private void SkipNextValue(JsonToken token)
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
                        if (TryFindEndOfString(pos, out var charsConsumed, out _))
                        {
                            pos += charsConsumed + 1; // skip JsonConstant.DoubleQuote too
                            return;
                        }

                        ThrowJsonFormatException(JsonFormatException.FormatError.ExpectedDoubleQuote);
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
            var i = pos;
            var length = _chars.Length;
            for (; i < length; i++)
            {
                ref readonly var c = ref _chars[i];
                if (!IsNumericSymbol(c))
                {
                    break;
                }
            }

            if (i > pos)
            {
                charsConsumed = i - pos;
                return true;
            }

            charsConsumed = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryFindEndOfString(int pos, out int charsConsumed, out int escapedChars)
        {
            escapedChars = 0;
            for (var i = pos + 1; i < _chars.Length; i++)
            {
                ref readonly var c = ref _chars[i];
                if (c == JsonConstant.Escape)
                {
                    escapedChars++;
                    var nextChar = _chars[i + 1];
                    if (nextChar == 'u' || nextChar == 'U')
                    {
                        escapedChars += 4; // add only 4 and not 5 as we still need one unescaped char
                    }
                }
                else if (c == JsonConstant.String)
                {
                    if (_chars[i - 1] == JsonConstant.Escape) // now it could be just an escaped JsonConstant.DoubleQuote
                    {
                        if (i - 2 >= 0 && _chars[i - 2] != JsonConstant.Escape) // it's an escaped string and not just an escaped JsonConstant.Escape
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

        public object ReadDynamic()
        {
            ref var pos = ref _pos;
            var nextToken = ReadNextToken();
            switch (nextToken)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.False:
                case JsonToken.True:
                    {
                        return ReadBoolean();
                    }
                case JsonToken.Number:
                    {
                        return new SpanJsonDynamicNumber(ReadNumberInternal());
                    }
                case JsonToken.String:
                    {
                        var span = ReadStringSpanWithQuotes(out var escapedChars);
                        return new SpanJsonDynamicString(span, escapedChars);
                    }
                case JsonToken.BeginObject:
                    {
                        pos++;
                        var count = 0;
                        var dictionary = new Dictionary<string, object>();
                        while (!TryReadIsEndObjectOrValueSeparator(ref count))
                        {
                            var name = ReadNameSpan().ToString();
                            var value = ReadDynamic();
                            dictionary.Add(name, value);
                        }

                        return new SpanJsonDynamicObject(dictionary);
                    }
                case JsonToken.BeginArray:
                    {
                        pos++;
                        var count = 0;
                        var list = new List<object>();
                        while (!TryReadIsEndArrayOrValueSeparator(ref count))
                        {
                            var value = ReadDynamic();
                            list.Add(value);
                        }

                        return new SpanJsonDynamicArray(list.ToArray());
                    }
                default:
                    {
                        ThrowJsonFormatException(JsonFormatException.FormatError.EndOfData);
                        return default;
                    }
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
                case '0':
                    return JsonToken.Number;
                default:
                    return JsonToken.None;
            }
        }


    }
}