using System;
using System.Globalization;

namespace SpanJson
{
    public static class JsonSharedConstant
    {
        public const int MaxNumberBufferSize = 32;
        public const int NestingLimit = 256;
        public const int StackAllocByteMaxLength = 256;
        public const int StackAllocCharMaxLength = StackAllocByteMaxLength / sizeof(char);

        // Max lengths of JSON strings; all include 2 double quotes.
        public const int MaxVersionLength = 45; // "{int.MaxValue}.{int.MaxValue}.{int.MaxValue}.{int.MaxValue}" => "2147483647.2147483647.2147483647.2147483647"
        public const int MaxGuidLength = 40; // B or P format specifier, e.g. "{nnnnnnnn-nnnn-nnnn-nnnn-nnnnnnnnnnnn}" or "(nnnnnnnn-nnnn-nnnn-nnnn-nnnnnnnnnnnn)" respectively
        public const int MaxDateTimeOffsetLength = 35; // o format specifier; e.g. "9999-12-31T23:59:59.9999999-00:00"
        public const int MaxDateTimeLength = 35; // o format specifier; e.g. "9999-12-31T23:59:59.9999999-00:00"
        public const int MaxTimeSpanLength = 28; // c format specifier; e.g. "{TimeSpan.MinValue:c}" => "-10675199.02:48:05.4775808"
        public const int MaxDateOnlyLength = 12; // "9999-12-31"
        public const int MaxTimeOnlyLength = 18; // "23:59:59.9999999"
    }

    public static class JsonUtf8Constant
    {
        public const byte BeginArray = (byte) '[';
        public const byte BeginObject = (byte) '{';
        public const byte DoubleQuote = (byte) '"';
        public const byte EndArray = (byte) ']';
        public const byte EndObject = (byte) '}';
        public const byte False = (byte) 'f';
        public const byte NameSeparator = (byte) ':';
        public const byte Null = (byte) 'n';
        public const byte ReverseSolidus = (byte) '\\';
        public const byte Solidus = (byte) '/';
        public const byte String = (byte) '"';
        public const byte True = (byte) 't';
        public const byte ValueSeparator = (byte) ',';

        public static ReadOnlySpan<byte> NewLine => new[] {(byte) '\r', (byte) '\n'};
        public static ReadOnlySpan<byte> NullTerminator => new byte[] {0};
    }

    public static class JsonUtf16Constant
    {
        public const char BeginArray = '[';
        public const char BeginObject = '{';
        public const char DoubleQuote = '"';
        public const char EndArray = ']';
        public const char EndObject = '}';
        public const char False = 'f';
        public const char NameSeparator = ':';
        public const char Null = 'n';
        public const char ReverseSolidus = '\\';
        public const char Solidus = '/';
        public const char String = '"';
        public const char True = 't';
        public const char ValueSeparator = ',';

        public static ReadOnlySpan<char> NewLine => new[] {'\r', '\n'};
        public static ReadOnlySpan<char> NullTerminator => new[] {'\0'};

        public const NumberStyles Float = NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign;
    }
}