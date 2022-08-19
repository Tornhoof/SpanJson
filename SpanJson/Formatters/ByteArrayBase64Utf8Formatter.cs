using System;
using System.Buffers;
using System.Diagnostics;
using System.Text;
using System.Text.Unicode;

namespace SpanJson.Formatters
{
    public sealed class ByteArrayBase64Utf8Formatter : ByteArrayBase64Formatter, IJsonFormatter<byte[], byte>
    {
        public static readonly ByteArrayBase64Utf8Formatter Default = new ByteArrayBase64Utf8Formatter();
        public void Serialize(ref JsonWriter<byte> writer, byte[] value)
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

                writer.WriteUtf8String(pooled.AsSpan(0, written));
            }
            finally
            {
                if (pooled != null)
                {
                    ArrayPool<char>.Shared.Return(pooled);
                }
            }
        }

        public byte[] Deserialize(ref JsonReader<byte> reader)
        {
            var byteValue = reader.ReadUtf8StringSpan();
            if (byteValue.IsEmpty)
            {
                return Array.Empty<byte>();
            }

            char[] pooled = null;
            try
            {
                int expectedLength = Encoding.UTF8.GetMaxCharCount(byteValue.Length);
                Span<char> scratchBuffer = expectedLength < JsonSharedConstant.StackAllocByteMaxLength
                    ? stackalloc char[JsonSharedConstant.StackAllocCharMaxLength]
                    : pooled = ArrayPool<char>.Shared.Rent(expectedLength);
                var status = Utf8.ToUtf16(byteValue, scratchBuffer, out _, out var charsWritten);
                Debug.Assert(status == OperationStatus.Done);
                scratchBuffer = scratchBuffer.Slice(0, charsWritten);
                int paddingStart = scratchBuffer.IndexOf('=');
                var result = AllocateDecodeArray(scratchBuffer.Length, paddingStart);
                if (!Convert.TryFromBase64Chars(scratchBuffer, result, out var written) || written != result.Length)
                {
                    ThrowBadDecoding(scratchBuffer);
                }

                return result;
            }
            finally
            {
                if (pooled != null)
                {
                    ArrayPool<char>.Shared.Return(pooled);
                }
            }
        }
    }
}