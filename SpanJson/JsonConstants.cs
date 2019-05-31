using System;

namespace SpanJson
{
    public static class JsonSharedConstant
    {
        public const int MaxNumberBufferSize = 32;
        public const int MaxVersionLength = 45; // 4 * int + 3 . + 2 double quote
        public const int NestingLimit = 256;
        public const int StackAllocByteMaxLength = 256;
        public const int StackAllocCharMaxLength = StackAllocByteMaxLength / sizeof(char);
        public const int MaxDateTimeOffsetLength = 35; // o + 2 double quotes
        public const int MaxDateTimeLength = 35; // o + 2 double quotes
        public const int MaxTimeSpanLength = 27; // c + 2 double quotes
        public const int MaxGuidLength = 42; // d + 2 double quotes
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
    }
}