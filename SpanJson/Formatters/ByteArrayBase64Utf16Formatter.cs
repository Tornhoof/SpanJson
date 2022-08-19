using System;
using System.Buffers;

namespace SpanJson.Formatters
{
    public sealed class ByteArrayBase64Utf16Formatter : ByteArrayBase64Formatter, IJsonFormatter<byte[], char>
    {
        public static readonly ByteArrayBase64Utf16Formatter Default = new ByteArrayBase64Utf16Formatter();
        public void Serialize(ref JsonWriter<char> writer, byte[] value)
        {
            char[] pooled = null;
            try
            {
                var expectedLength = CalculatedExpectedBase64Length(value.Length);
                // stackalloc does not yet work in .NET < 7 for this, escape analysis does not like it
                pooled = ArrayPool<char>.Shared.Rent(expectedLength);

                if (!Convert.TryToBase64Chars(value, pooled, out var written) || written != expectedLength)
                {
                    ThrowBadEncoding(value);
                }

                writer.WriteUtf16String(pooled.AsSpan(0, written));
            }
            finally
            {
                if (pooled != null)
                {
                    ArrayPool<char>.Shared.Return(pooled);
                }
            }
        }


        public byte[] Deserialize(ref JsonReader<char> reader)
        {
            var value = reader.ReadUtf16StringSpan();
            if (value.IsEmpty)
            {
                return Array.Empty<byte>();
            }

            int paddingStart = value.IndexOf('=');
            var result = AllocateDecodeArray(value.Length, paddingStart);
            if (!Convert.TryFromBase64Chars(value, result, out var written) || written != result.Length)
            {
                ThrowBadDecoding(value);
            }

            return result;
        }
    }
}