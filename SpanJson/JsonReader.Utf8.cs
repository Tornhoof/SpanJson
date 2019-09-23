using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Formatters.Dynamic;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref partial struct JsonReader<TSymbol> where TSymbol : struct
    {
        public sbyte ReadUtf8SByte()
        {
            return checked((sbyte) ReadUtf8NumberInt64());
        }

        public short ReadUtf8Int16()
        {
            return checked((short) ReadUtf8NumberInt64());
        }

        public int ReadUtf8Int32()
        {
            return checked((int) ReadUtf8NumberInt64());
        }

        public long ReadUtf8Int64()
        {
            return ReadUtf8NumberInt64();
        }

        public byte ReadUtf8Byte()
        {
            return checked((byte) ReadUtf8NumberUInt64());
        }

        public ushort ReadUtf8UInt16()
        {
            return checked((ushort) ReadUtf8NumberUInt64());
        }

        public uint ReadUtf8UInt32()
        {
            return checked((uint) ReadUtf8NumberUInt64());
        }

        public ulong ReadUtf8UInt64()
        {
            return ReadUtf8NumberUInt64();
        }

        public float ReadUtf8Single()
        {
            var span = ReadUtf8NumberInternal();
            if (!Utf8Parser.TryParse(span, out float value, out var consumed) || span.Length != consumed)
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat);
            }

            return value;
        }

        public double ReadUtf8Double()
        {
            var span = ReadUtf8NumberInternal();
            if (!Utf8Parser.TryParse(span, out double value, out var consumed) || span.Length != consumed)
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat);
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<byte> ReadUtf8NumberInternal()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (TryFindEndOfUtf8Number(pos, out var bytesConsumed))
            {
                var result = _bytes.Slice(pos, bytesConsumed);
                pos += bytesConsumed;
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
            return default;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long ReadUtf8NumberInt64()
        {
            SkipWhitespaceUtf8();
            if (_pos >= _length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                return default;
            }

            ref var b = ref MemoryMarshal.GetReference(_bytes);
            var neg = false;
            if (Unsafe.Add(ref b, _pos) == (byte) '-')
            {
                _pos++;
                neg = true;

                if (_pos >= _length) // we still need one digit
                {
                    ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                    return default;
                }
            }

            var result = ReadUtf8NumberDigits(ref b, ref _pos);
            return neg ? unchecked(-(long) result) : checked((long) result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong ReadUtf8NumberUInt64()
        {
            SkipWhitespaceUtf8();
            if (_pos >= _length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                return default;
            }

            ref var b = ref MemoryMarshal.GetReference(_bytes);
            return ReadUtf8NumberDigits(ref b, ref _pos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong ReadUtf8NumberDigits(ref byte b, ref int pos)
        {
            uint value;
            var result = Unsafe.Add(ref b, pos) - 48UL;
            if (result > 9) // this includes '-'
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat);
                return default;
            }

            pos++;
            while (pos < _length && (value = Unsafe.Add(ref b, pos) - 48U) <= 9)
            {
                result = checked(result * 10 + value);
                pos++;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsNumericUtf8Symbol(byte c)
        {
            switch (c)
            {
                case (byte) '0':
                case (byte) '1':
                case (byte) '2':
                case (byte) '3':
                case (byte) '4':
                case (byte) '5':
                case (byte) '6':
                case (byte) '7':
                case (byte) '8':
                case (byte) '9':
                case (byte) '+':
                case (byte) '-':
                case (byte) '.':
                case (byte) 'E':
                case (byte) 'e':
                    return true;
            }

            return false;
        }

        public bool ReadUtf8Boolean()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos <= _length - 4)
            {
                ref var b = ref MemoryMarshal.GetReference(_bytes);
                ref var start = ref Unsafe.Add(ref b, pos);
                var value = Unsafe.ReadUnaligned<uint>(ref start);
                if (value == 0x65757274U /*eurt */)
                {
                    pos += 4;
                    return true;
                }

                if (pos <= _length - 5 && value == 0x736C6166U /* slaf */ && Unsafe.Add(ref b, pos + 4) == (byte) 'e')
                {
                    pos += 5;
                    return false;
                }
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.Bool);
            return false;
        }

        public char ReadUtf8Char()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out _);
            return ReadUtf8CharInternal(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char ReadUtf8CharInternal(in ReadOnlySpan<byte> span)
        {
            if (span.Length == 1)
            {
                return (char) span[0];
            }

            if (span[0] == JsonUtf8Constant.ReverseSolidus)
            {
                switch (span[1])
                {
                    case JsonUtf8Constant.DoubleQuote:
                        return JsonUtf16Constant.DoubleQuote;
                    case JsonUtf8Constant.ReverseSolidus:
                        return JsonUtf16Constant.ReverseSolidus;
                    case JsonUtf8Constant.Solidus:
                        return JsonUtf16Constant.Solidus;
                    case (byte) 'b':
                        return '\b';
                    case (byte) 'f':
                        return '\f';
                    case (byte) 'n':
                        return '\n';
                    case (byte) 'r':
                        return '\r';
                    case (byte) 't':
                        return '\t';
                    case (byte) 'u':
                        if (Utf8Parser.TryParse(span.Slice(2, 4), out int value, out _, 'X'))
                        {
                            return (char) value;
                        }

                        ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                        break;
                }
            }

            Span<char> charSpan = stackalloc char[1];
            Encoding.UTF8.GetChars(span, charSpan);
            return charSpan[0];
        }

        public DateTime ReadUtf8DateTime()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ParseUtf8DateTime(span) : ParseUtf8DateTimeAllocating(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DateTime ParseUtf8DateTime(in ReadOnlySpan<byte> span)
        {
            if (DateTimeParser.TryParseDateTime(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTime);
            return default;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private DateTime ParseUtf8DateTimeAllocating(in ReadOnlySpan<byte> input)
        {
            Span<byte> span = stackalloc byte[JsonSharedConstant.MaxDateTimeLength];
            UnescapeUtf8Bytes(input, ref span);
            if (DateTimeParser.TryParseDateTime(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTime);
            return default;
        }

        public DateTimeOffset ReadUtf8DateTimeOffset()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ParseUtf8DateTimeOffset(span) : ParseUtf8DateTimeOffsetAllocating(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private DateTimeOffset ParseUtf8DateTimeOffset(in ReadOnlySpan<byte> span)
        {
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTimeOffset);
            return default;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private DateTimeOffset ParseUtf8DateTimeOffsetAllocating(in ReadOnlySpan<byte> input)
        {
            Span<byte> span = stackalloc byte[JsonSharedConstant.MaxDateTimeOffsetLength];
            UnescapeUtf8Bytes(input, ref span);
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.DateTimeOffset);
            return default;
        }

        public TimeSpan ReadUtf8TimeSpan()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ParseUtf8TimeSpan(span) : ParseUtf8TimeSpanAllocating(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private TimeSpan ParseUtf8TimeSpan(in ReadOnlySpan<byte> span)
        {
            if (Utf8Parser.TryParse(span, out TimeSpan result, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.TimeSpan);
            return default;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private TimeSpan ParseUtf8TimeSpanAllocating(in ReadOnlySpan<byte> input)
        {
            Span<byte> span = stackalloc byte[JsonSharedConstant.MaxTimeSpanLength];
            UnescapeUtf8Bytes(input, ref span);
            if (Utf8Parser.TryParse(span, out TimeSpan result, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.TimeSpan);
            return default;
        }

        public Guid ReadUtf8Guid()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ParseUtf8Guid(span) : ParseUtf8GuidAllocating(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Guid ParseUtf8Guid(in ReadOnlySpan<byte> span)
        {
            if (Utf8Parser.TryParse(span, out Guid result, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.Guid);
            return default;
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private Guid ParseUtf8GuidAllocating(in ReadOnlySpan<byte> input)
        {
            Span<byte> span = stackalloc byte[JsonSharedConstant.MaxGuidLength];
            UnescapeUtf8Bytes(input, ref span);
            if (Utf8Parser.TryParse(span, out Guid result, out var bytesConsumed) && span.Length == bytesConsumed)
            {
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, JsonParserException.ValueType.Guid);
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadUtf8EscapedName()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            SkipWhitespaceUtf8();
            if (_bytes[_pos++] != JsonUtf8Constant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return escapedCharsSize == 0 ? ConvertToString(span) : UnescapeUtf8(span, escapedCharsSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<byte> ReadUtf8EscapedNameSpan()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            SkipWhitespaceUtf8();
            if (_bytes[_pos++] != JsonUtf8Constant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return escapedCharsSize == 0 ? span : UnescapeUtf8Bytes(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<byte> ReadUtf8VerbatimNameSpan()
        {
            SkipWhitespaceUtf8();
            var span = ReadUtf8StringSpanInternal(out _);
            SkipWhitespaceUtf8();
            if (_bytes[_pos++] != JsonUtf8Constant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadUtf8String()
        {
            if (ReadUtf8IsNull())
            {
                return null;
            }

            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? ConvertToString(span) : UnescapeUtf8(span, escapedCharsSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string ConvertToString(in ReadOnlySpan<byte> span)
        {
            return Encoding.UTF8.GetString(span);
        }

        /// <summary>
        ///     This is simply said pretty much twice as slow as the Utf16 version
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string UnescapeUtf8(in ReadOnlySpan<byte> span, int escapedCharsSize)
        {
            var unescapedLength = Encoding.UTF8.GetCharCount(span) - escapedCharsSize;
            var result = new string('\0', unescapedLength);
            var charOffset = 0;
            // We create a writeable span of the chars in the string (currently there is no string.create overload taking a span as state so this the solution for now).
            var writeableSpan = MemoryMarshal.CreateSpan(ref MemoryMarshal.GetReference(result.AsSpan()), unescapedLength);
            var from = 0;
            var index = 0;
            while (index < span.Length)
            {
                ref readonly var current = ref span[index];
                if (current == JsonUtf8Constant.ReverseSolidus)
                {
                    // We copy everything up to the escaped char as utf8 to the string
                    charOffset += Encoding.UTF8.GetChars(span.Slice(from, index - from), writeableSpan.Slice(charOffset));
                    index++;
                    current = ref span[index++];
                    char unescaped = default;
                    switch (current)
                    {
                        case JsonUtf8Constant.DoubleQuote:
                            unescaped = JsonUtf16Constant.DoubleQuote;
                            break;
                        case JsonUtf8Constant.ReverseSolidus:
                            unescaped = JsonUtf16Constant.ReverseSolidus;
                            break;
                        case JsonUtf8Constant.Solidus:
                            unescaped = JsonUtf16Constant.Solidus;
                            break;
                        case (byte) 'b':
                            unescaped = '\b';
                            break;
                        case (byte) 'f':
                            unescaped = '\f';
                            break;
                        case (byte) 'n':
                            unescaped = '\n';
                            break;
                        case (byte) 'r':
                            unescaped = '\r';
                            break;
                        case (byte) 't':
                            unescaped = '\t';
                            break;
                        case (byte) 'u':
                        {
                            if (Utf8Parser.TryParse(span.Slice(index, 4), out uint value, out var bytesConsumed, 'X'))
                            {
                                index += bytesConsumed;
                                unescaped = (char) value;
                                break;
                            }

                            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                            break;
                        }

                        default:
                        {
                            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                            break;
                        }
                    }

                    writeableSpan[charOffset++] = unescaped;
                    from = index;
                }
                else
                {
                    index++;
                }
            }

            if (from < span.Length) // still data to copy
            {
                Encoding.UTF8.GetChars(span.Slice(from), writeableSpan.Slice(charOffset));
            }

            return result;
        }

        private static ReadOnlySpan<byte> UnescapeUtf8Bytes(in ReadOnlySpan<byte> span)
        {
            // not necessarily correct, just needs to be a good upper bound
            // this gets slightly too high, as the normal escapes are two bytes, and the \u1234 escapes are 6 bytes, but we only need 4
            var unescapedLength = span.Length;
            Span<byte> result = new byte[unescapedLength];
            UnescapeUtf8Bytes(span, ref result);
            return result;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void UnescapeUtf8Bytes(in ReadOnlySpan<byte> span, ref Span<byte> result)
        {
            var byteOffset = 0;
            var from = 0;
            var index = 0;
            while (index < span.Length)
            {
                ref readonly var current = ref span[index];
                if (current == JsonUtf8Constant.ReverseSolidus)
                {
                    // We copy everything up to the escaped char as utf8 to the string
                    var sliceLength = index - from;
                    span.Slice(from, sliceLength).CopyTo(result.Slice(byteOffset));
                    byteOffset += sliceLength;
                    index++;
                    current = ref span[index++];
                    byte unescaped = default;
                    switch (current)
                    {
                        case JsonUtf8Constant.DoubleQuote:
                            unescaped = JsonUtf8Constant.DoubleQuote;
                            break;
                        case JsonUtf8Constant.ReverseSolidus:
                            unescaped = JsonUtf8Constant.ReverseSolidus;
                            break;
                        case JsonUtf8Constant.Solidus:
                            unescaped = JsonUtf8Constant.Solidus;
                            break;
                        case (byte) 'b':
                            unescaped = (byte) '\b';
                            break;
                        case (byte) 'f':
                            unescaped = (byte) '\f';
                            break;
                        case (byte) 'n':
                            unescaped = (byte) '\n';
                            break;
                        case (byte) 'r':
                            unescaped = (byte) '\r';
                            break;
                        case (byte) 't':
                            unescaped = (byte) '\t';
                            break;
                        case (byte) 'u':
                        {
                            if (Utf8Parser.TryParse(span.Slice(index, 4), out uint value, out var bytesConsumed, 'X'))
                            {
                                index += bytesConsumed;
                                var c = (char) value;
                                var stack = MemoryMarshal.CreateSpan(ref c, 1);
                                byteOffset += Encoding.UTF8.GetBytes(stack, result.Slice(byteOffset));
                                from = index;
                                continue;
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

                    result[byteOffset++] = unescaped;
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
                span.Slice(from, sliceLength).CopyTo(result.Slice(byteOffset));
                byteOffset += sliceLength;
            }

            result = result.Slice(0, byteOffset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<byte> ReadUtf8StringSpan()
        {
            if (ReadUtf8IsNull())
            {
                return JsonUtf8Constant.NullTerminator;
            }

            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            return escapedCharsSize == 0 ? span : UnescapeUtf8Bytes(span);
        }

        private ReadOnlySpan<byte> ReadUtf8StringSpanInternal(out int escapedCharsSize)
        {
            ref var pos = ref _pos;
            if (pos <= _length - 2)
            {
                ref var b = ref MemoryMarshal.GetReference(_bytes);
                ref var stringStart = ref Unsafe.Add(ref b, pos++);
                if (stringStart != JsonUtf8Constant.String)
                {
                    ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
                }

                var stringLength = 0;
                // We should also get info about how many escaped chars exist from here
                if (TryFindEndOfUtf8String(ref stringStart, _length - pos, ref stringLength, out escapedCharsSize))
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
        private ReadOnlySpan<byte> ReadUtf8StringSpanWithQuotes()
        {
            ref var pos = ref _pos;
            if (pos <= _length - 2)
            {
                ref var b = ref MemoryMarshal.GetReference(_bytes);
                ref var stringStart = ref Unsafe.Add(ref b, pos++);
                if (stringStart != JsonUtf8Constant.String)
                {
                    ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
                }

                var stringLength = 0;
                // We should also get info about how many escaped chars exist from here
                if (TryFindEndOfUtf8String(ref stringStart, _length - pos, ref stringLength, out _))
                {
                    var result = MemoryMarshal.CreateReadOnlySpan(ref stringStart, stringLength + 1);
                    pos += stringLength; // skip the doublequote too
                    return result;
                }
            }

            ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            return default;
        }

        public decimal ReadUtf8Decimal()
        {
            if (Utf8Parser.TryParse(ReadUtf8NumberInternal(), out decimal result, out _))
            {
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat);
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf8IsNull()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            ref var b = ref MemoryMarshal.GetReference(_bytes);
            ref var start = ref Unsafe.Add(ref b, pos);
            if (pos <= _length - 4 && Unsafe.ReadUnaligned<uint>(ref start) == 0x6C6C756EU /* llun */)
            {
                pos += 4;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf8Null()
        {
            if (!ReadUtf8IsNull())
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SkipWhitespaceUtf8()
        {
            ref var pos = ref _pos;
            while (pos < _length)
            {
                ref readonly var b = ref _bytes[pos];
                switch (b)
                {
                    case (byte) ' ':
                    case (byte) '\t':
                    case (byte) '\r':
                    case (byte) '\n':
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
        public void ReadUtf8BeginArrayOrThrow()
        {
            if (!ReadUtf8BeginArray())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedBeginArray);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf8BeginArray()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos < _length && _bytes[pos] == JsonUtf8Constant.BeginArray)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadUtf8IsEndArrayOrValueSeparator(ref int count)
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos < _length && _bytes[pos] == JsonUtf8Constant.EndArray)
            {
                pos++;
                return true;
            }

            if (count++ > 0)
            {
                if (pos < _length && _bytes[pos] == JsonUtf8Constant.ValueSeparator)
                {
                    pos++;
                    return false;
                }

                ThrowJsonParserException(JsonParserException.ParserError.ExpectedSeparator);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf8IsBeginObject()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (_bytes[pos] == JsonUtf8Constant.BeginObject)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf8BeginObjectOrThrow()
        {
            if (!ReadUtf8IsBeginObject())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedBeginObject);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf8EndObjectOrThrow()
        {
            if (!ReadUtf8IsEndObject())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedEndObject);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf8EndArrayOrThrow()
        {
            if (!ReadUtf8IsEndArray())
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedEndArray);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf8IsEndObject()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (_bytes[pos] == JsonUtf8Constant.EndObject)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf8IsEndArray()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (_bytes[pos] == JsonUtf8Constant.EndArray)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadUtf8IsEndObjectOrValueSeparator(ref int count)
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos < _length && _bytes[pos] == JsonUtf8Constant.EndObject)
            {
                pos++;
                return true;
            }

            if (count++ > 0)
            {
                if (_bytes[pos] == JsonUtf8Constant.ValueSeparator)
                {
                    pos++;
                    return false;
                }

                ThrowJsonParserException(JsonParserException.ParserError.ExpectedSeparator);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Version ReadUtf8Version()
        {
            var stringValue = ReadUtf8String();
            if (stringValue == null)
            {
                return default;
            }

            return Version.Parse(stringValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Uri ReadUtf8Uri()
        {
            var stringValue = ReadUtf8String();
            if (stringValue == null)
            {
                return default;
            }

            return new Uri(stringValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadUtf8SymbolOrThrow(byte constant)
        {
            if (!ReadUtf8IsSymbol(constant))
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadUtf8IsSymbol(byte constant)
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos < _length && _bytes[pos] == constant)
            {
                pos++;
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipNextUtf8Segment()
        {
            SkipNextUtf8Segment(0);
        }

        private void SkipNextUtf8Segment(int stack)
        {
            ref var pos = ref _pos;
            while (pos < _length)
            {
                var token = ReadUtf8NextToken();
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
                            SkipNextUtf8Value(token);
                            token = ReadUtf8NextToken();
                        } while (stack > 0 && (byte) token > 4); // No None or the Begin/End-Array/Object tokens

                        if (stack > 0)
                        {
                            continue;
                        }

                        return;
                    }
                }
            }
        }

        public void SkipNextUtf8Value(JsonToken token)
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
                    if (TryFindEndOfUtf8Number(pos, out var bytesConsumed))
                    {
                        pos += bytesConsumed;
                    }

                    break;
                }

                case JsonToken.String:
                {
                    if (SkipUtf8String(ref pos))
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
        private bool TryFindEndOfUtf8Number(int pos, out int bytesConsumed)
        {
            var i = pos;
            var length = _bytes.Length;
            for (; i < length; i++)
            {
                ref readonly var b = ref _bytes[i];
                if (!IsNumericUtf8Symbol(b))
                {
                    break;
                }
            }

            if (i > pos)
            {
                bytesConsumed = i - pos;
                return true;
            }

            bytesConsumed = default;
            return false;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool TryFindEndOfUtf8String(ref byte bStart, int length, ref int stringLength, out int escapedCharsSize)
        {
            escapedCharsSize = 0;
            while (stringLength < length)
            {
                ref var b = ref Unsafe.Add(ref bStart, ++stringLength);
                if (b == JsonUtf8Constant.ReverseSolidus)
                {
                    escapedCharsSize++;
                    b = ref Unsafe.Add(ref bStart, ++stringLength);
                    if (b == (byte) 'u')
                    {
                        escapedCharsSize += 4; // add only 4 and not 5 as we still need one unescaped char
                        stringLength += 4;
                    }
                }
                else if (b == JsonUtf8Constant.String)
                {
                    return true;
                }
            }

            return false;
        }

        private bool SkipUtf8String(ref int pos)
        {
            ref var b = ref MemoryMarshal.GetReference(_bytes);
            ref var stringStart = ref Unsafe.Add(ref b, pos++);
            var stringLength = 0;
            // We should also get info about how many escaped chars exist from here
            if (TryFindEndOfUtf8String(ref stringStart, _length - pos, ref stringLength, out _))
            {
                pos += stringLength; // skip the doublequote too
                return true;
            }

            return false;
        }

        public object ReadUtf8Dynamic()
        {
            return ReadUtf8Dynamic(0);
        }

        public object ReadUtf8Dynamic(int stack)
        {
            ref var pos = ref _pos;
            var nextToken = ReadUtf8NextToken();
            if (stack > JsonSharedConstant.NestingLimit)
            {
                ThrowJsonParserException(JsonParserException.ParserError.NestingTooDeep);
            }

            switch (nextToken)
            {
                case JsonToken.Null:
                {
                    ReadUtf8Null();
                    return null;
                }

                case JsonToken.False:
                case JsonToken.True:
                {
                    return ReadUtf8Boolean();
                }

                case JsonToken.Number:
                {
                    return new SpanJsonDynamicUtf8Number(ReadUtf8NumberInternal());
                }

                case JsonToken.String:
                {
                    var span = ReadUtf8StringSpanWithQuotes();
                    return new SpanJsonDynamicUtf8String(span);
                }

                case JsonToken.BeginObject:
                {
                    pos++;
                    var count = 0;
                    var dictionary = new Dictionary<string, object>();
                    while (!TryReadUtf8IsEndObjectOrValueSeparator(ref count))
                    {
                        var name = ReadUtf8EscapedName();
                        var value = ReadUtf8Dynamic(stack + 1);
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
                        while (!TryReadUtf8IsEndArrayOrValueSeparator(ref count))
                        {
                            if (count == temp.Length)
                            {
                                FormatterUtils.GrowArray(ref temp);
                            }

                            temp[count - 1] = ReadUtf8Dynamic(stack + 1);
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

        public JsonToken ReadUtf8NextToken()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos >= _bytes.Length)
            {
                return JsonToken.None;
            }

            ref readonly var b = ref _bytes[pos];
            switch (b)
            {
                case JsonUtf8Constant.BeginObject:
                    return JsonToken.BeginObject;
                case JsonUtf8Constant.EndObject:
                    return JsonToken.EndObject;
                case JsonUtf8Constant.BeginArray:
                    return JsonToken.BeginArray;
                case JsonUtf8Constant.EndArray:
                    return JsonToken.EndArray;
                case JsonUtf8Constant.String:
                    return JsonToken.String;
                case JsonUtf8Constant.True:
                    return JsonToken.True;
                case JsonUtf8Constant.False:
                    return JsonToken.False;
                case JsonUtf8Constant.Null:
                    return JsonToken.Null;
                case JsonUtf8Constant.ValueSeparator:
                    return JsonToken.ValueSeparator;
                case JsonUtf8Constant.NameSeparator:
                    return JsonToken.NameSeparator;
                case (byte) '-':
                case (byte) '1':
                case (byte) '2':
                case (byte) '3':
                case (byte) '4':
                case (byte) '5':
                case (byte) '6':
                case (byte) '7':
                case (byte) '8':
                case (byte) '9':
                case (byte) '0':
                    return JsonToken.Number;
                default:
                    return JsonToken.None;
            }
        }
    }
}