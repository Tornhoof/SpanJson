using System;
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
            return (sbyte) ReadUtf8NumberInt64();
        }

        public short ReadUtf8Int16()
        {
            return (short) ReadUtf8NumberInt64();
        }

        public int ReadUtf8Int32()
        {
            return (int) ReadUtf8NumberInt64();
        }

        public long ReadUtf8Int64()
        {
            return ReadUtf8NumberInt64();
        }

        public byte ReadUtf8Byte()
        {
            return (byte) ReadUtf8NumberUInt64();
        }

        public ushort ReadUtf8UInt16()
        {
            return (ushort) ReadUtf8NumberUInt64();
        }

        public uint ReadUtf8UInt32()
        {
            return (uint) ReadUtf8NumberUInt64();
        }

        public ulong ReadUtf8UInt64()
        {
            return ReadUtf8NumberUInt64();
        }

        public float ReadUtf8Single()
        {
            Utf8Parser.TryParse(ReadUtf8NumberInternal(), out float value, out var consumed); // todo find out if correct
            return value;
        }

        public double ReadUtf8Double()
        {
            Utf8Parser.TryParse(ReadUtf8NumberInternal(), out double value, out var consumed); // todo find out if correct
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
            return null;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long ReadUtf8NumberInt64()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos >= _length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                return default;
            }

            ref readonly var firstChar = ref _bytes[pos];
            var neg = false;
            if (firstChar == '-')
            {
                pos++;
                neg = true;
            }

            if (pos >= _length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                return default;
            }

            var result = _bytes[pos++] - 48L;
            uint value;
            while (pos < _length && (value = _bytes[pos] - 48U) <= 9)
            {
                result = unchecked(result * 10 + value);
                pos++;
            }

            return neg ? unchecked(-result) : result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong ReadUtf8NumberUInt64()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos >= _length)
            {
                ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                return default;
            }

            ref readonly var firstChar = ref _bytes[pos];
            if (firstChar == '-')
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat);
                return default;
            }

            var result = _bytes[pos++] - 48UL;
            if (result > 9)
            {
                ThrowJsonParserException(JsonParserException.ParserError.InvalidNumberFormat);
                return default;
            }

            uint value;
            while (pos < _length && (value = _bytes[pos] - 48U) <= 9)
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
            if (_bytes[pos] == (byte) JsonConstant.True) // just peek the byte
            {
                if (_bytes[pos + 1] != 'r')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
                }

                if (_bytes[pos + 2] != 'u')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
                }

                if (_bytes[pos + 3] != 'e')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
                }

                pos += 4;
                return true;
            }

            if (_bytes[pos] == (byte) JsonConstant.False) // just peek the byte
            {
                if (_bytes[pos + 1] != 'a')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
                }

                if (_bytes[pos + 2] != 'l')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
                }

                if (_bytes[pos + 3] != 's')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
                }

                if (_bytes[pos + 4] != 'e')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
                }

                pos += 5;
                return false;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(bool));
            return false;
        }

        public char ReadUtf8Char()
        {
            var span = ReadUtf8StringSpan();
            return ReadUtf8CharInternal(span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char ReadUtf8CharInternal(ReadOnlySpan<byte> span)
        {
            int pos = 0;
            if (span.Length == 1)
            {
                return (char) span[pos];
            }

            if (span[pos] == JsonConstant.ReverseSolidus)
            {
                pos++;
                switch (span[pos++])
                {
                    case (byte) JsonConstant.DoubleQuote:
                        return JsonConstant.DoubleQuote;
                    case (byte) JsonConstant.ReverseSolidus:
                        return JsonConstant.ReverseSolidus;
                    case (byte) JsonConstant.Solidus:
                        return JsonConstant.Solidus;
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
                    case (byte) 'U':
                    case (byte) 'u':
                        if (span.Length == 6)
                        {
                            if (Utf8Parser.TryParse(span.Slice(2, 4), out int value, out var bytesConsumed, 'X'))
                            {
                                return (char) value;
                            }
                        }

                        break;
                }
            }
            else
            {
                Span<char> charSpan = stackalloc char[1];
                Encoding.UTF8.GetChars(span, charSpan);
                return charSpan[0];
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(byte));
            return default;
        }

        public DateTime ReadUtf8DateTime()
        {
            var span = ReadUtf8StringSpan();
            if (DateTimeParser.TryParseDateTime(span, out var value, out var bytesConsumed))
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(DateTime));
            return default;
        }

        public DateTimeOffset ReadUtf8DateTimeOffset()
        {
            var span = ReadUtf8StringSpan();
            if (DateTimeParser.TryParseDateTimeOffset(span, out var value, out var bytesConsumed))
            {
                return value;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(DateTimeOffset));
            return default;
        }

        public TimeSpan ReadUtf8TimeSpan()
        {
            var span = ReadUtf8StringSpan();
            if (Utf8Parser.TryParse(span, out TimeSpan result, out var bytesConsumed))
            {
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(TimeSpan));
            return default;
        }

        public Guid ReadUtf8Guid()
        {
            var span = ReadUtf8StringSpan();
            if (Utf8Parser.TryParse(span, out Guid result, out var bytesConsumed))
            {
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol, typeof(Guid));
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<byte> ReadUtf8NameSpan()
        {
            var span = ReadUtf8StringSpan();
            if (_bytes[_pos++] != (byte) JsonConstant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadUtf8EscapedName()
        {
            var span = ReadUtf8StringSpanInternal(out var escapedCharsSize);
            if (_bytes[_pos++] != (byte) JsonConstant.NameSeparator)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            return escapedCharsSize == 0 ? span.ToString() : UnescapeUtf8(span, escapedCharsSize);
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

        private static string ConvertToString(ReadOnlySpan<byte> span)
        {
            return Encoding.UTF8.GetString(span);
        }

        private string UnescapeUtf8(in ReadOnlySpan<byte> span, int escapedCharsSize)
        {
            var unescapedLength = span.Length - escapedCharsSize;
            var result = new string('\0', unescapedLength);
            ref var c = ref MemoryMarshal.GetReference(result.AsSpan());
            var unescapedIndex = 0;
            var index = 0;
            while (index < span.Length)
            {
                var current = span[index++];
                var unescaped = (char) current;
                if (current == JsonConstant.ReverseSolidus)
                {
                    current = span[index++];
                    switch (current)
                    {
                        case (byte) JsonConstant.DoubleQuote:
                            unescaped = JsonConstant.DoubleQuote;
                            break;
                        case (byte) JsonConstant.ReverseSolidus:
                            unescaped = JsonConstant.ReverseSolidus;
                            break;
                        case (byte) JsonConstant.Solidus:
                            unescaped = JsonConstant.Solidus;
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
                        case (byte) 'U':
                        case (byte) 'u':
                        {
                            if (Utf8Parser.TryParse(span.Slice(index, 4), out int value, out var bytesConsumed, 'X'))
                            {
                                index += bytesConsumed;
                                unescaped = (char) value;
                                break;
                            }

                            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                            break;
                        }

                        default:
                            ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                            break;
                    }
                }

                Unsafe.Add(ref c, unescapedIndex++) = unescaped;
            }

            return result;
        }


        /// <summary>
        ///     Not escaped
        /// </summary>
        public ReadOnlySpan<byte> ReadUtf8StringSpan()
        {
            if (ReadUtf8IsNull())
            {
                return JsonConstant.NullTerminatorUtf8;
            }

            return ReadUtf8StringSpanInternal(out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<byte> ReadUtf8StringSpanInternal(out int escapedCharsSize)
        {
            ref var pos = ref _pos;
            if (_bytes[pos] != (byte) JsonConstant.String)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            pos++;
            // We should also get info about how many escaped chars exist from here
            if (TryFindEndOfUtf8String(pos, out var bytesConsumed, out escapedCharsSize))
            {
                var result = _bytes.Slice(pos, bytesConsumed);
                pos += bytesConsumed + 1; // skip the JsonConstant.DoubleQuote too
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            return null;
        }

        /// <summary>
        ///     Includes the quotes on each end
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ReadOnlySpan<byte> ReadUtf8StringSpanWithQuotes(out int escapedCharsSize)
        {
            ref var pos = ref _pos;
            if (_bytes[pos] != (byte) JsonConstant.String)
            {
                ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            }

            // We should also get info about how many escaped chars exist from here
            if (TryFindEndOfUtf8String(pos + 1, out var bytesConsumed, out escapedCharsSize))
            {
                var result = _bytes.Slice(pos, bytesConsumed + 2); // we include quotes in this version
                pos += bytesConsumed + 2; // include both JsonConstant.DoubleQuote too 
                return result;
            }

            ThrowJsonParserException(JsonParserException.ParserError.ExpectedDoubleQuote);
            return default;
        }

        public decimal ReadUtf8Decimal()
        {
            if (Utf8Parser.TryParse(ReadUtf8NumberInternal(), out decimal result, out var bytesConsumed))
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
            if (pos < _length && _bytes[pos] == (byte) JsonConstant.Null) // just peek the byte
            {
                if (_bytes[pos + 1] != 'u')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                }

                if (_bytes[pos + 2] != 'l')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                }

                if (_bytes[pos + 3] != 'l')
                {
                    ThrowJsonParserException(JsonParserException.ParserError.InvalidSymbol);
                }

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
                ref readonly var c = ref _bytes[pos];
                switch (c)
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
            if (pos < _length && _bytes[pos] == JsonConstant.BeginArray)
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
            if (pos < _length && _bytes[pos] == JsonConstant.EndArray)
            {
                pos++;
                return true;
            }

            if (count++ > 0)
            {
                if (pos < _length && _bytes[pos] == JsonConstant.ValueSeparator)
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
            if (_bytes[pos] == JsonConstant.BeginObject)
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
        public bool ReadUtf8IsEndObject()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (_bytes[pos] == JsonConstant.EndObject)
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
            if (pos < _length && _bytes[pos] == JsonConstant.EndObject)
            {
                pos++;
                return true;
            }

            if (count++ > 0)
            {
                if (_bytes[pos] == JsonConstant.ValueSeparator)
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
        public void SkipNextUtf8Segment()
        {
            SkipNextUtf8Segment(0);
        }

        private void SkipNextUtf8Segment(int stack)
        {
            ref var pos = ref _pos;
            var token = ReadUtf8NextToken();
            switch (token)
            {
                case JsonToken.None:
                    break;
                case JsonToken.BeginArray:
                case JsonToken.BeginObject:
                {
                    pos++;
                    SkipNextUtf8Segment(stack + 1);
                    break;
                }
                case JsonToken.EndObject:
                case JsonToken.EndArray:
                {
                    pos++;
                    if (stack - 1 > 0)
                    {
                        SkipNextUtf8Segment(stack - 1);
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
                        SkipNextUtf8Value(token);
                        token = ReadUtf8NextToken();
                    } while (stack > 0 && (byte) token > 4); // No None or the Begin/End-Array/Object tokens

                    if (stack > 0)
                    {
                        SkipNextUtf8Segment(stack);
                    }

                    break;
                }
            }
        }

        private void SkipNextUtf8Value(JsonToken token)
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
                    pos++;
                    if (TryFindEndOfUtf8String(pos, out var bytesConsumed, out _))
                    {
                        pos += bytesConsumed + 1; // skip JsonConstant.DoubleQuote too
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
                ref readonly var c = ref _bytes[i];
                if (!IsNumericUtf8Symbol(c))
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
        private bool TryFindEndOfUtf8String(int pos, out int bytesConsumed, out int escapedCharsSize)
        {
            escapedCharsSize = 0;
            for (var i = pos; i < _bytes.Length; i++)
            {
                ref readonly var c = ref _bytes[i];
                if (c == JsonConstant.ReverseSolidus)
                {
                    escapedCharsSize++;
                    i++;
                    ref readonly var nextByte = ref _bytes[i]; // check what type of escaped char it is
                    if (nextByte == 'u' || nextByte == 'U')
                    {
                        escapedCharsSize += 4; // add only 4 and not 5 as we still need one unescaped char
                        i += 4;
                    }

                }
                else if (c == JsonConstant.String)
                {
                    bytesConsumed = i - pos;
                    return true;
                }
            }

            bytesConsumed = default;
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
            if (stack > 256)
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
                    var span = ReadUtf8StringSpanWithQuotes(out _);
                    return new SpanJsonDynamicUtf8String(span);
                }
                case JsonToken.BeginObject:
                {
                    pos++;
                    var count = 0;
                    var dictionary = new Dictionary<string, object>();
                    while (!TryReadUtf8IsEndObjectOrValueSeparator(ref count))
                    {
                        var name = ConvertToString(ReadUtf8NameSpan());
                        var value = ReadUtf8Dynamic(stack + 1);
                        dictionary[name] = value; // take last one
                    }

                    return new SpanJsonDynamicObject(dictionary);
                }
                case JsonToken.BeginArray:
                {
                    pos++;
                    var count = 0;
                    var list = new List<object>();
                    while (!TryReadUtf8IsEndArrayOrValueSeparator(ref count))
                    {
                        var value = ReadUtf8Dynamic(stack + 1);
                        list.Add(value);
                    }

                    return new SpanJsonDynamicArray<TSymbol>(list.ToArray());
                }
                default:
                {
                    ThrowJsonParserException(JsonParserException.ParserError.EndOfData);
                    return default;
                }
            }
        }

        private JsonToken ReadUtf8NextToken()
        {
            SkipWhitespaceUtf8();
            ref var pos = ref _pos;
            if (pos >= _bytes.Length)
            {
                return JsonToken.None;
            }

            ref readonly var c = ref _bytes[pos];
            switch (c)
            {
                case (byte) JsonConstant.BeginObject:
                    return JsonToken.BeginObject;
                case (byte) JsonConstant.EndObject:
                    return JsonToken.EndObject;
                case (byte) JsonConstant.BeginArray:
                    return JsonToken.BeginArray;
                case (byte) JsonConstant.EndArray:
                    return JsonToken.EndArray;
                case (byte) JsonConstant.String:
                    return JsonToken.String;
                case (byte) JsonConstant.True:
                    return JsonToken.True;
                case (byte) JsonConstant.False:
                    return JsonToken.False;
                case (byte) JsonConstant.Null:
                    return JsonToken.Null;
                case (byte) JsonConstant.ValueSeparator:
                    return JsonToken.ValueSeparator;
                case (byte) JsonConstant.NameSeparator:
                    return JsonToken.NameSeparator;
                case (byte) '+':
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