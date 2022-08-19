using System;
using System.Text;

namespace SpanJson.Formatters
{
    public abstract class ByteArrayBase64Formatter
    {

        protected static void ThrowBadEncoding(in ReadOnlySpan<byte> value)
        {
            throw new InvalidOperationException($"{Encoding.UTF8.GetString(value)} could not be converted to Base64.");
        }

        protected static void ThrowBadDecoding(in ReadOnlySpan<char> value)
        {
            throw new InvalidOperationException($"{value.ToString()} is not a valid Base64 string.");
        }

        protected static int CalculatedExpectedBase64Length(int length)
        {
            return ((4 * length / 3) + 3) & ~3;
        }

        protected static byte[] AllocateDecodeArray(int length, int paddingStart)
        {
            var padding = paddingStart == -1 ? 0 : length - paddingStart;
            var result = new byte[(length * 3) / 4 - padding];
            return result;
        }
    }
}