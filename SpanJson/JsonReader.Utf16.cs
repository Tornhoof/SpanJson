using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SpanJson.Formatters.Dynamic;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref partial struct JsonReader<TSymbol> where TSymbol : struct
    {
        public sbyte ReadUtf16SByte()
        {
            return checked((sbyte)ReadUtf16NumberInt64());
        }

        public short ReadUtf16Int16()
        {
            return checked((short)ReadUtf16NumberInt64());
        }

        public int ReadUtf16Int32()
        {
            return checked((int)ReadUtf16NumberInt64());
        }

        public long ReadUtf16Int64()
        {
            return ReadUtf16NumberInt64();
        }

        public byte ReadUtf16Byte()
        {
            return checked((byte)ReadUtf16NumberUInt64());
        }

        public ushort ReadUtf16UInt16()
        {
            return checked((ushort)ReadUtf16NumberUInt64());
        }

        public uint ReadUtf16UInt32()
        {
            return checked((uint)ReadUtf16NumberUInt64());
        }

        public ulong ReadUtf16UInt64()
        {
            return ReadUtf16NumberUInt64();
        }

        public float ReadUtf16Single()
        {
            return float.Parse(ReadUtf16NumberInternal(), NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public double ReadUtf16Double()
        {
            return double.Parse(ReadUtf16NumberInternal(), NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<char> ReadUtf16NumberInternal()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (TryFindEndOfUtf16Number(pos, out var charsConsumed))
            {
                var result = _chars.Slice(pos, charsConsumed);
                pos += charsConsumed;
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
            return default;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long ReadUtf16NumberInt64()
        {
            SkipWhitespaceUtf16();
            if (_pos >= _length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                return default;
            }

            ref var c = ref MemoryMarshal.GetReference(_chars);
            var neg = false;
            if (Unsafe.Add(ref c, _pos) == '-')
            {
                _pos++;
                neg = true;

                if (_pos >= _length) // we still need one digit
                {
                    ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                    return default;
                }
            }

            var result = ReadUtf16NumberDigits(ref c, ref _pos);
            return neg ? unchecked(-(long)result) : checked((long)result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong ReadUtf16NumberDigits(ref char c, ref int pos)
        {
            uint value;
            var result = Unsafe.Add(ref c, pos) - 48UL;
            if (result > 9) // this includes '-'
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat);
                return default;
            }

            pos++;
            while (pos < _length && (value = Unsafe.Add(ref c, pos) - 48U) <= 9)
            {
                result = checked(result * 10 + value);
                pos++;
            }

            return result;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong ReadUtf16NumberUInt64()
        {
            SkipWhitespaceUtf16();
            if (_pos >= _length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                return default;
            }

            ref var c = ref MemoryMarshal.GetReference(_chars);
            return ReadUtf16NumberDigits(ref c, ref _pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsNumericUtf16Symbol(char c)
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

        public bool ReadUtf16Boolean()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (pos <= _length - 4)
            {
                ref var c = ref MemoryMarshal.GetReference(_chars);
                ref var start = ref Unsafe.Add(ref c, pos);
                ref var bstart = ref Unsafe.As<char, byte>(ref start);
                var value = Unsafe.ReadUnaligned<ulong>(ref bstart);
                if (value == 0x0065007500720074UL /*eurt */)
                {
                    pos += 4;
                    return true;
                }

                if (pos <= _length - 5 && value == 0x0073006C00610066UL /* slaf */ && Unsafe.Add(ref c, pos + 4) == 'e')
                {
                    pos += 5;
                    return false;
                }
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.Bool);
            return false;
        }

        public char ReadUtf16Char()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out _);
            return ReadUtf16CharInternal(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char ReadUtf16CharInternal(in ReadOnlySpan<char> span)
        {
            var pos = 0;
            if (span.Length == 1)
            {
                return span[pos];
            }

            if (span[pos++] == JsonUtf16Constant.ReverseSolidus)
            {
                ref readonly var current = ref span[pos++];
                switch (current)
                {
                    case JsonUtf16Constant.DoubleQuote:
                    case JsonUtf16Constant.ReverseSolidus:
                        return current;
                    case JsonUtf16Constant.Solidus:
                        return current;
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
                    case 'u':
                    {
                        if (int.TryParse(span.Slice(pos, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var value))
                        {
                            return (char)value;
                        }

                        break;
                    }
                }
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
            return default;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf16EndObjectOrThrow()
        {
            if (!ReadUtf16IsEndObject())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedEndObject);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf16EndArrayOrThrow()
        {
            if (!ReadUtf16IsEndArray())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedEndArray);
            }
        }


        public DateTime ReadUtf16DateTime()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ParseUtf16DateTime(span) : ParseUtf16DateTimeAllocating(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DateTime ParseUtf16DateTime(in ReadOnlySpan<char> span)
        {
            if (DateTimeParser.TryParseDateTime(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTime);
            return default;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private DateTime ParseUtf16DateTimeAllocating(in ReadOnlySpan<char> input)
        {
            Span<char> span = stackalloc char[JsonSharedConstant.MaxDateTimeLength];
            UnescapeUtf16Chars(input, ref span);
            if (DateTimeParser.TryParseDateTime(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTime);
            return default;
        }

        public DateTimeOffset ReadUtf16DateTimeOffset()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ParseUtf16DateTimeOffset(span) : ParseUtf16DateTimeOffsetAllocating(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DateTimeOffset ParseUtf16DateTimeOffset(in ReadOnlySpan<char> span)
        {
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTimeOffset);
            return default;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private DateTimeOffset ParseUtf16DateTimeOffsetAllocating(in ReadOnlySpan<char> input)
        {
            Span<char> span = stackalloc char[JsonSharedConstant.MaxDateTimeOffsetLength];
            UnescapeUtf16Chars(input, ref span);
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTimeOffset);
            return default;
        }

        public TimeSpan ReadUtf16TimeSpan()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ConvertTimeSpanViaUtf8(span, _pos) : ParseUtf16TimeSpanAllocating(span);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private TimeSpan ParseUtf16TimeSpanAllocating(in ReadOnlySpan<char> input)
        {
            Span<char> span = stackalloc char[JsonSharedConstant.MaxTimeSpanLength];
            UnescapeUtf16Chars(input, ref span);
            return ConvertTimeSpanViaUtf8(span, _pos);
        }

        public Guid ReadUtf16Guid()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ParseUtf16Guid(span, _pos) : ParseUtf16GuidAllocating(span);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Guid ParseUtf16GuidAllocating(in ReadOnlySpan<char> input)
        {
            Span<char> span = stackalloc char[JsonSharedConstant.MaxGuidLength];
            UnescapeUtf16Chars(input, ref span);
            return ParseUtf16Guid(span, _pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Guid ParseUtf16Guid(in ReadOnlySpan<char> span, int position)
        {
            var format = 'D';
            if (Guid.TryParseExact(span, MemoryMarshal.CreateSpan(ref format, 1), out var value))
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.Guid, position);
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TimeSpan ConvertTimeSpanViaUtf8(in ReadOnlySpan<char> span, int position)
        {
            Span<byte> byteSpan = stackalloc byte[JsonSharedConstant.MaxTimeSpanLength];
            for (var i = 0; i < span.Length; i++)
            {
                byteSpan[i] = (byte)span[i];
            }

            // still slow in .NET Core 3.0
            byteSpan = byteSpan.Slice(0, span.Length);
            if (Utf8Parser.TryParse(byteSpan, out TimeSpan value, out var bytesConsumed) && bytesConsumed == span.Length)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.TimeSpan, position);
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadUtf16EscapedName()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out var escapedCharsSize);
            SkipWhitespaceUtf16();
            if (_chars[_pos++] != JsonUtf16Constant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return escapedCharsSize == 0 ? span.ToString() : UnescapeUtf16(span, escapedCharsSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadUtf16VerbatimNameSpan()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out _);
            SkipWhitespaceUtf16();
            if (_chars[_pos++] != JsonUtf16Constant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadUtf16EscapedNameSpan()
        {
            SkipWhitespaceUtf16();
            var span = ReadUtf16StringSpanInternal(out var escapedCharsSize);
            SkipWhitespaceUtf16();
            if (_chars[_pos++] != JsonUtf16Constant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return escapedCharsSize == 0 ? span : UnescapeUtf16(span, escapedCharsSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadUtf16String()
        {
            if (ReadUtf16IsNull())
            {
                return null;
            }

            var span = ReadUtf16StringSpanInternal(out var escapedCharSize);
            return escapedCharSize == 0 ? span.ToString() : UnescapeUtf16(span, escapedCharSize);
        }

        private static string UnescapeUtf16(in ReadOnlySpan<char> span, int escapedCharSize)
        {
            var unescapedLength = span.Length - escapedCharSize;
            var result = new string('\0', unescapedLength);
            var writeableSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), unescapedLength);
            UnescapeUtf16Chars(span, ref writeableSpan);
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void UnescapeUtf16Chars(in ReadOnlySpan<char> span, ref Span<char> result)
        {
            var charOffset = 0;
            var from = 0;
            var index = 0;
            while (index < span.Length)
            {
                ref readonly var current = ref span[index];
                if (current == JsonUtf16Constant.ReverseSolidus)
                {
                    // We copy everything up to the escaped char as utf16 to the string
                    var copyLength = index - from;
                    span.Slice(from, copyLength).CopyTo(result.Slice(charOffset));
                    charOffset += copyLength;
                    index++;
                    current = ref span[index++];
                    char unescaped = default;
                    switch (current)
                    {
                        case JsonUtf16Constant.DoubleQuote:
                            unescaped = JsonUtf16Constant.DoubleQuote;
                            break;
                        case JsonUtf16Constant.ReverseSolidus:
                            unescaped = JsonUtf16Constant.ReverseSolidus;
                            break;
                        case JsonUtf16Constant.Solidus:
                            unescaped = JsonUtf16Constant.Solidus;
                            break;
                        case 'b':
                            unescaped = '\b';
                            break;
                        case 'f':
                            unescaped = '\f';
                            break;
                        case 'n':
                            unescaped = '\n';
                            break;
                        case 'r':
                            unescaped = '\r';
                            break;
                        case 't':
                            unescaped = '\t';
                            break;
                        case 'u':
                        {
                            if (int.TryParse(span.Slice(index, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var value))
                            {
                                index += 4;
                                unescaped = (char)value;
                                break;
                            }

                            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, index);
                            break;
                        }

                        default:
                        {
                            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, index);
                            break;
                        }
                    }

                    result[charOffset++] = unescaped;
                    from = index;
                }
                else
                {
                    index++;
                }
            }

            if (from < span.Length) // still data to copy
            {
                var sliceLength = span.Length - from;
                span.Slice(from, sliceLength).CopyTo(result.Slice(charOffset));
                charOffset += sliceLength;
            }

            result = result.Slice(0, charOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> ReadUtf16StringSpan()
        {
            if (ReadUtf16IsNull())
            {
                return JsonUtf16Constant.NullTerminator;
            }

            var span = ReadUtf16StringSpanInternal(out var escapedCharSize);
            return escapedCharSize == 0 ? span : UnescapeUtf16(span, escapedCharSize);
        }

        private ReadOnlySpan<char> ReadUtf16StringSpanInternal(out int escapedCharsSize)
        {
            ref var pos = ref _pos;
            if (pos <= _length - 2)
            {
                ref var c = ref MemoryMarshal.GetReference(_chars);
                ref var stringStart = ref Unsafe.Add(ref c, pos++);
                if (stringStart != JsonUtf16Constant.String)
                {
                    ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
                }

                var stringLength = 0;
                // We should also get info about how many escaped chars exist from here
                if (TryFindEndOfUtf16String(ref stringStart, _length - pos, ref stringLength, out escapedCharsSize))
                {
                    var result = MemoryMarshal.CreateReadOnlySpan(ref Unsafe.Add(ref stringStart, 1), stringLength - 1);
                    pos += stringLength; // skip the doublequote too
                    return result;
                }
            }

            ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            escapedCharsSize = default;
            return default;
        }

        /// <summary>
        ///     Includes the quotes on each end
        /// </summary>
        private ReadOnlySpan<char> ReadUtf16StringSpanWithQuotes()
        {
            ref var pos = ref _pos;
            if (pos <= _length - 2)
            {
                ref var c = ref MemoryMarshal.GetReference(_chars);
                ref var stringStart = ref Unsafe.Add(ref c, pos++);
                if (stringStart != JsonUtf16Constant.String)
                {
                    ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
                }

                var stringLength = 0;
                // We should also get info about how many escaped chars exist from here
                if (TryFindEndOfUtf16String(ref stringStart, _length - pos, ref stringLength, out _))
                {
                    var result = MemoryMarshal.CreateReadOnlySpan(ref stringStart, stringLength + 1);
                    pos += stringLength; // skip the doublequote too
                    return result;
                }
            }

            ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            return default;
        }

        public decimal ReadUtf16Decimal()
        {
            return decimal.Parse(ReadUtf16NumberInternal(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf16IsNull()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            ref var c = ref MemoryMarshal.GetReference(_chars);
            ref var start = ref Unsafe.Add(ref c, pos);
            ref var bstart = ref Unsafe.As<char, byte>(ref start);
            if (pos <= _length - 4 && Unsafe.ReadUnaligned<ulong>(ref bstart) == 0x006C006C0075006EUL /* llun */)
            {
                pos += 4;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf16Null()
        {
            if (!ReadUtf16IsNull())
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SkipWhitespaceUtf16()
        {
            ref var pos = ref _pos;
            while (pos < _length)
            {
                var c = _chars[pos];
                switch (c)
                {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                    {
                        pos++;
                        continue;
                    }

                    default:
                        return;
                }
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf16BeginArrayOrThrow()
        {
            if (!ReadUtf16BeginArray())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedBeginArray);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf16BeginArray()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (pos < _length && _chars[pos] == JsonUtf16Constant.BeginArray)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadUtf16IsEndArrayOrValueSeparator(ref int count)
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (pos < _length && _chars[pos] == JsonUtf16Constant.EndArray)
            {
                pos++;
                return true;
            }

            if (count++ > 0)
            {
                if (pos < _length && _chars[pos] == JsonUtf16Constant.ValueSeparator)
                {
                    pos++;
                    return false;
                }

                ThrowJsonParserException(JsonParserException.ParserError.ExpectedSeparator);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf16IsBeginObject()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (_chars[pos] == JsonUtf16Constant.BeginObject)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf16BeginObjectOrThrow()
        {
            if (!ReadUtf16IsBeginObject())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedBeginObject);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf16IsEndObject()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (_chars[pos] == JsonUtf16Constant.EndObject)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf16IsEndArray()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (_chars[pos] == JsonUtf16Constant.EndArray)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadUtf16IsEndObjectOrValueSeparator(ref int count)
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (pos < _length && _chars[pos] == JsonUtf16Constant.EndObject)
            {
                pos++;
                return true;
            }

            if (count++ > 0)
            {
                if (_chars[pos] == JsonUtf16Constant.ValueSeparator)
                {
                    pos++;
                    return false;
                }

                ThrowJsonParserException(JsonParserException.ParserError.ExpectedSeparator);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Version ReadUtf16Version()
        {
            var stringValue = ReadUtf16String();
            if (stringValue == null)
            {
                return default;
            }

            return Version.Parse(stringValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Uri ReadUtf16Uri()
        {
            var stringValue = ReadUtf16String();
            if (stringValue == null)
            {
                return default;
            }

            return new Uri(stringValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf16SymbolOrThrow(char constant)
        {
            if (!ReadUtf16IsSymbol(constant))
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf16IsSymbol(char constant)
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (_chars[pos] == constant)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipNextUtf16Segment()
        {
            SkipNextUtf16Segment(0);
        }

        private void SkipNextUtf16Segment(int stack)
        {
            ref var pos = ref _pos;
            while (pos < _length)
            {
                var token = ReadUtf16NextToken();
                switch (token)
                {
                    case JsonToken.None:
                        return;
                    case JsonToken.BeginArray:
                    case JsonToken.BeginObject:
                    {
                        pos++;
                        stack++;
                        continue;
                    }

                    case JsonToken.EndObject:
                    case JsonToken.EndArray:
                    {
                        pos++;
                        if (stack - 1 > 0)
                        {
                            stack--;
                            continue;
                        }

                        return;
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
                            SkipNextUtf16Value(token);
                            token = ReadUtf16NextToken();
                        } while (stack > 0 && (byte)token > 4); // No None or the Begin/End-Array/Object tokens

                        if (stack > 0)
                        {
                            continue;
                        }

                        return;
                    }
                }
            }
        }

        public void SkipNextUtf16Value(JsonToken token)
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
                    if (TryFindEndOfUtf16Number(pos, out var charsConsumed))
                    {
                        pos += charsConsumed;
                    }

                    break;
                }

                case JsonToken.String:
                {
                    if (SkipUtf16String(ref pos))
                    {
                        return;
                    }

                    ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
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
        private bool TryFindEndOfUtf16Number(int pos, out int charsConsumed)
        {
            var i = pos;
            var length = _chars.Length;
            for (; i < length; i++)
            {
                var c = _chars[i];
                if (!IsNumericUtf16Symbol(c))
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
        private static bool TryFindEndOfUtf16String(ref char cStart, int length, ref int stringLength, out int escapedCharsSize)
        {
            escapedCharsSize = 0;
            while (stringLength < length)
            {
                ref var c = ref Unsafe.Add(ref cStart, ++stringLength);
                if (c == JsonUtf16Constant.ReverseSolidus)
                {
                    escapedCharsSize++;
                    c = ref Unsafe.Add(ref cStart, ++stringLength);
                    if (c == 'u')
                    {
                        escapedCharsSize += 4; // add only 4 and not 5 as we still need one unescaped char
                        stringLength += 4;
                    }
                }
                else if (c == JsonUtf16Constant.String)
                {
                    return true;
                }
            }

            return false;
        }

        private bool SkipUtf16String(ref int pos)
        {
            ref var c = ref MemoryMarshal.GetReference(_chars);
            ref var stringStart = ref Unsafe.Add(ref c, pos++);
            if (stringStart != JsonUtf16Constant.String)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            var stringLength = 0;
            // We should also get info about how many escaped chars exist from here
            if (TryFindEndOfUtf16String(ref stringStart, _length - pos, ref stringLength, out _))
            {
                pos += stringLength; // skip the doublequote too
                return true;
            }

            return false;
        }

        public object ReadUtf16Dynamic()
        {
            return ReadUtf16Dynamic(0);
        }

        public object ReadUtf16Dynamic(int stack)
        {
            ref var pos = ref _pos;
            var nextToken = ReadUtf16NextToken();
            if (stack > JsonSharedConstant.NestingLimit)
            {
                ThrowJsonParserException(JsonParserException.ParserError.NestingTooDeep);
            }

            switch (nextToken)
            {
                case JsonToken.Null:
                {
                    ReadUtf16Null();
                    return null;
                }

                case JsonToken.False:
                case JsonToken.True:
                {
                    return ReadUtf16Boolean();
                }

                case JsonToken.Number:
                {
                    return new SpanJsonDynamicUtf16Number(ReadUtf16NumberInternal());
                }

                case JsonToken.String:
                {
                    var span = ReadUtf16StringSpanWithQuotes();
                    return new SpanJsonDynamicUtf16String(span);
                }

                case JsonToken.BeginObject:
                {
                    pos++;
                    var count = 0;
                    var dictionary = new Dictionary<string, object>();
                    while (!TryReadUtf16IsEndObjectOrValueSeparator(ref count))
                    {
                        var name = ReadUtf16EscapedName();
                        var value = ReadUtf16Dynamic(stack + 1);
                        dictionary[name] = value; // take last one
                    }

                    return new SpanJsonDynamicObject(dictionary);
                }

                case JsonToken.BeginArray:
                {
                    pos++;
                    var count = 0;
                    object[] temp = null;
                    try
                    {
                        temp = ArrayPool<object>.Shared.Rent(4);
                        while (!TryReadUtf16IsEndArrayOrValueSeparator(ref count))
                        {
                            if (count == temp.Length)
                            {
                                FormatterUtils.GrowArray(ref temp);
                            }

                            temp[count - 1] = ReadUtf16Dynamic(stack + 1);
                        }

                        object[] result;
                        if (count == 0)
                        {
                            result = Array.Empty<object>();
                        }
                        else
                        {
                            result = FormatterUtils.CopyArray(temp, count);
                        }

                        return new SpanJsonDynamicArray<TSymbol>(result);
                    }
                    finally
                    {
                        if (temp != null)
                        {
                            ArrayPool<object>.Shared.Return(temp);
                        }
                    }
                }

                default:
                {
                    ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                    return default;
                }
            }
        }

        public JsonToken ReadUtf16NextToken()
        {
            SkipWhitespaceUtf16();
            ref var pos = ref _pos;
            if (pos >= _chars.Length)
            {
                return JsonToken.None;
            }

            var c = _chars[pos];
            switch (c)
            {
                case JsonUtf16Constant.BeginObject:
                    return JsonToken.BeginObject;
                case JsonUtf16Constant.EndObject:
                    return JsonToken.EndObject;
                case JsonUtf16Constant.BeginArray:
                    return JsonToken.BeginArray;
                case JsonUtf16Constant.EndArray:
                    return JsonToken.EndArray;
                case JsonUtf16Constant.String:
                    return JsonToken.String;
                case JsonUtf16Constant.True:
                    return JsonToken.True;
                case JsonUtf16Constant.False:
                    return JsonToken.False;
                case JsonUtf16Constant.Null:
                    return JsonToken.Null;
                case JsonUtf16Constant.ValueSeparator:
                    return JsonToken.ValueSeparator;
                case JsonUtf16Constant.NameSeparator:
                    return JsonToken.NameSeparator;
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

        public byte[] ReadUtf16Base64EncodedArray()
        {
            var stringValue = ReadUtf16StringSpan();
            if (stringValue.IsEmpty)
            {
                return Array.Empty<byte>();
            }
            // this shold not be hit, null checks happen in the formatter
            if (stringValue[0] == JsonUtf16Constant.NullTerminator[0])
            {
                return default;
            }
            var paddingStart = stringValue.IndexOf('=');
            var length = stringValue.Length;
            var padding = paddingStart == -1 ? 0 : length - paddingStart;
            var result = new byte[(length * 3) / 4 - padding];
            if (!Convert.TryFromBase64Chars(stringValue, result, out var written) || written != result.Length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidEncoding, JsonParserException.ValueType.Array, _pos);
            }

            return result;
        }
    }
}