using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class ErrorUtf8Formatter : BaseGeneratedFormatter<Error, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Error, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly ErrorUtf8Formatter Default = new ErrorUtf8Formatter();
        private readonly byte[] _descriptionName = Encoding.UTF8.GetBytes("\"description\":");
        private readonly byte[] _error_idName = Encoding.UTF8.GetBytes("\"error_id\":");
        private readonly byte[] _error_nameName = Encoding.UTF8.GetBytes("\"error_name\":");

        public Error Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Error();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 11 && ReadUInt64(ref b, 0) == 8390322045806929252UL && ReadUInt16(ref b, 8) == 28521 && ReadByte(ref b, 10) == 110)
                {
                    result.description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7235419221448094309UL)
                {
                    result.error_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7020653814217863781UL && ReadUInt16(ref b, 8) == 25965)
                {
                    result.error_name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Error value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.error_id != null)
            {
                writer.WriteUtf8Verbatim(_error_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.error_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.error_name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_error_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.error_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_descriptionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.description, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}