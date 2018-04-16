using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SpanJson.Helpers
{
    public static class StringParser
    {
        public static bool TryParseChar(in ReadOnlySpan<char> span, out char value)
        {
            var pos = 0;
            if (span.Length == 1)
            {
                value = span[0];
                return true;
            }

            if (span[pos] == JsonConstant.Escape)
            {
                switch (span[pos + 1])
                {
                    case JsonConstant.DoubleQuote:
                        value = JsonConstant.DoubleQuote;
                        return true;
                    case JsonConstant.Escape:
                        value = JsonConstant.Escape;
                        return true;
                    case 'b':
                        value = '\b';
                        return true;
                    case 'f':
                        value = '\f';
                        return true;
                    case 'n':
                        value = '\n';
                        return true;
                    case 'r':
                        value = '\r';
                        return true;
                    case 't':
                        value = '\t';
                        return true;
                    case 'U':
                    case 'u':
                        if (span.Length == 6)
                        {
                            value = (char) int.Parse(span.Slice(2, 4), NumberStyles.AllowHexSpecifier,
                                CultureInfo.InvariantCulture);
                            return true;
                        }

                        break;
                }
            }

            value = default;
            return false;
        }

        public static bool TryParseDateTime(in ReadOnlySpan<char> span, out DateTime value)
        {
            if (DateTimeParser.TryParseDateTime(span, out var dt, out _))
            {
                value = dt;
                return true;
            }

            value = default;
            return false;
        }

        public static bool TryParseDateTimeOffset(in ReadOnlySpan<char> span, out DateTimeOffset value)
        {
            if (DateTimeParser.TryParseDateTimeOffset(span, out var dto, out _))
            {
                value = dto;
                return true;
            }

            value = default;
            return false;
        }

        public static bool TryParseTimeSpan(in ReadOnlySpan<char> span, out TimeSpan value)
        {
            if (TimeSpan.TryParse(span, out var ts))
            {
                value = ts;
                return true;
            }

            value = default;
            return false;
        }

        public static bool TryParseGuid(in ReadOnlySpan<char> span, out Guid value)
        {
            if (Guid.TryParse(span, out var guid))
            {
                value = guid;
                return true;
            }

            value = default;
            return false;
        }

        public static bool TryParseVersion(in ReadOnlySpan<char> span, out Version value)
        {
            if (Version.TryParse(span, out var version))
            {
                value = version;
                return true;
            }

            value = default;
            return false;
        }

        public static bool TryParseUri(in ReadOnlySpan<char> span, out Uri value)
        {
            var data = new string(span);
            if (Uri.TryCreate(data, UriKind.RelativeOrAbsolute, out var uri))
            {
                value = uri;
                return true;
            }

            value = default;
            return false;
        }

        public static bool TryParseEnum(in ReadOnlySpan<char> span, Type enumType, out object value)
        {
            var data = new string(span);
            if (Enum.TryParse(enumType, data, out var enumValue))
            {
                value = enumValue;
                return true;
            }

            value = default;
            return false;
        }
    }
}
