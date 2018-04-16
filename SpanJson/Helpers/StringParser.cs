using System;
using System.Collections.Generic;
using System.Text;

namespace SpanJson.Helpers
{
    public static class StringParser
    {
        public static bool TryParseChar(in ReadOnlySpan<char> span, out char value)
        {
            if (span.Length == 1)
            {
                value = span[0];
                return true;
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
