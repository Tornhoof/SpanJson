using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class MigrationInfoUtf16Formatter : BaseGeneratedFormatter<Question.MigrationInfo, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Question.MigrationInfo, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _question_idName = "\"question_id\":";
        private const string _other_siteName = "\"other_site\":";
        private const string _on_dateName = "\"on_date\":";
        public static readonly MigrationInfoUtf16Formatter Default = new MigrationInfoUtf16Formatter();

        public Question.MigrationInfo Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Question.MigrationInfo();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 11 && ReadUInt64(ref b, 0) == 32370056121090161UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt32(ref b, 16) == 6881375U && ReadUInt16(ref b, 20) == 100)
                {
                    result.question_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 28429419331977327UL && ReadUInt64(ref b, 8) == 29555366482083954UL &&
                    ReadUInt32(ref b, 16) == 6619252U)
                {
                    result.other_site = SiteUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 28147905700167791UL && ReadUInt32(ref b, 8) == 7602273U && ReadUInt16(ref b, 12) == 101)
                {
                    result.on_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Question.MigrationInfo value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.question_id != null)
            {
                writer.WriteUtf16Verbatim(_question_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.question_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.other_site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_other_siteName);
                SiteUtf16Formatter.Default.Serialize(ref writer, value.other_site, nestingLimit);
                writeSeparator = true;
            }

            if (value.on_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_on_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.on_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}