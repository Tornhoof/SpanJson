using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class MigrationInfoUtf8Formatter : BaseGeneratedFormatter<Question.MigrationInfo, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Question.MigrationInfo, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MigrationInfoUtf8Formatter Default = new MigrationInfoUtf8Formatter();
        private readonly byte[] _on_dateName = Encoding.UTF8.GetBytes("\"on_date\":");
        private readonly byte[] _other_siteName = Encoding.UTF8.GetBytes("\"other_site\":");
        private readonly byte[] _question_idName = Encoding.UTF8.GetBytes("\"question_id\":");

        public Question.MigrationInfo Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Question.MigrationInfo();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 11 && ReadUInt64(ref b, 0) == 7957695015460107633UL && ReadUInt16(ref b, 8) == 26975 && ReadByte(ref b, 10) == 100)
                {
                    result.question_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7598521941236413551UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.other_site = SiteUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1683975791U && ReadUInt16(ref b, 4) == 29793 && ReadByte(ref b, 6) == 101)
                {
                    result.on_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Question.MigrationInfo value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.question_id != null)
            {
                writer.WriteUtf8Verbatim(_question_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.question_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.other_site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_other_siteName);
                SiteUtf8Formatter.Default.Serialize(ref writer, value.other_site, nestingLimit);
                writeSeparator = true;
            }

            if (value.on_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_on_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.on_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}