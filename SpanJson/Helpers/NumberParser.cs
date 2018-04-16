using System;
using System.Globalization;

namespace SpanJson.Helpers
{
    public static class NumberParser
    {
        public static bool TryParseDouble(in ReadOnlySpan<char> span, out double result)
        {
            return double.TryParse(span, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        public static bool TryParseSingle(in ReadOnlySpan<char> span, out float result)
        {
            return float.TryParse(span, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        public static bool TryParseDecimal(in ReadOnlySpan<char> span, out decimal result)
        {
            return decimal.TryParse(span, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }


        public static bool TryParseSbyte(in ReadOnlySpan<char> span, out sbyte result)
        {
            const int length = 4;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            if (TryParseSignedNumber(span, out var temp))
            {
                result = (sbyte) temp;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseInt16(in ReadOnlySpan<char> span, out short result)
        {
            const int length = 6;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            if (TryParseSignedNumber(span, out var temp))
            {
                result = (short) temp;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseInt32(in ReadOnlySpan<char> span, out int result)
        {
            const int length = 11;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            if (TryParseSignedNumber(span, out var temp))
            {
                result = (int) temp;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseInt64(in ReadOnlySpan<char> span, out long result)
        {
            const int length = 21;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            return TryParseSignedNumber(span, out result);
        }

        public static bool TryParseByte(in ReadOnlySpan<char> span, out byte result)
        {
            const int length = 4;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            if (TryParseUnsignedNumber(span, out var temp))
            {
                result = (byte) temp;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseUInt16(in ReadOnlySpan<char> span, out ushort result)
        {
            const int length = 6;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            if (TryParseUnsignedNumber(span, out var temp))
            {
                result = (ushort) temp;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseUInt32(in ReadOnlySpan<char> span, out uint result)
        {
            const int length = 11;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            if (TryParseUnsignedNumber(span, out var temp))
            {
                result = (uint) temp;
                return true;
            }

            result = default;
            return false;
        }

        public static bool TryParseUInt64(in ReadOnlySpan<char> span, out ulong result)
        {
            const int length = 21;
            if (span.Length > length)
            {
                result = default;
                return false;
            }

            return TryParseUnsignedNumber(span, out result);
        }


        private static bool TryParseUnsignedNumber(in ReadOnlySpan<char> span, out ulong result)
        {
            var pos = 0;
            ref readonly var firstChar = ref span[pos];
            if (firstChar == '+')
            {
                pos++;
            }
            else if (firstChar == '-')
            {
                result = default;
                return false;
            }

            result = span[pos++] - 48UL;
            if (result > 9)
            {
                result = default;
                return false;
            }

            uint value;
            while (pos < span.Length && (value = span[pos] - 48U) <= 9)
            {
                result = checked(result * 10 + value);
                pos++;
            }

            return true;
        }

        private static bool TryParseSignedNumber(in ReadOnlySpan<char> span, out long result)
        {
            var pos = 0;
            ref readonly var firstChar = ref span[pos];
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

            result = span[pos++] - 48L;
            if (result > 9)
            {
                result = default;
                return false;
            }

            uint value;
            while (pos < span.Length && (value = span[pos] - 48U) <= 9)
            {
                result = checked(result * 10 + value);
                pos++;
            }

            result = neg ? unchecked(-result) : result;
            return true;
        }
    }
}