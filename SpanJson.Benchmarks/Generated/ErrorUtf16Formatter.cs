using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class ErrorUtf16Formatter : BaseGeneratedFormatter<Error, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Error, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _error_idName = "\"error_id\":";
        private const string _error_nameName = "\"error_name\":";
        private const string _descriptionName = "\"description\":";
        public static readonly ErrorUtf16Formatter Default = new ErrorUtf16Formatter();

        public Error Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Error();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 31244212048625765UL)
                {
                    if (length == 8 && ReadUInt64(ref b, 8) == 28147948648857714UL)
                    {
                        result.error_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 10 && ReadUInt64(ref b, 8) == 27303545193562226UL && ReadUInt32(ref b, 16) == 6619245U)
                    {
                        result.error_name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 27866516622213220UL && ReadUInt64(ref b, 8) == 32651578341654642UL &&
                    ReadUInt32(ref b, 16) == 7274601U && ReadUInt16(ref b, 20) == 110)
                {
                    result.description = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Error value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.error_id != null)
            {
                writer.WriteUtf16Verbatim(_error_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.error_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.error_name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_error_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.error_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_descriptionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.description, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}