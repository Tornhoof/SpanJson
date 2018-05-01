using System.Text;

namespace SpanJson
{
    public static class JsonConstant
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

        public static readonly char[] NullTerminatorUtf16 = { '\0' };
        public static readonly byte[] NullTerminatorUtf8 = { (byte)NullTerminatorUtf16[0] };

        public static readonly char[] LongMinValueUtf16 = long.MinValue.ToString().ToCharArray();
        public static readonly byte[] LongMinValueUtf8 = Encoding.UTF8.GetBytes(long.MinValue.ToString());

        public const int MaxNumberBufferSize = 32;
        public const int MaxVersionLength = 45;  // 4 * int + 3 . + 2 double quote
    }
}