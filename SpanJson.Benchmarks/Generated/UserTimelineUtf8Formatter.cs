using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class UserTimelineUtf8Formatter : BaseGeneratedFormatter<UserTimeline, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<UserTimeline, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly UserTimelineUtf8Formatter Default = new UserTimelineUtf8Formatter();
        private readonly byte[] _badge_idName = Encoding.UTF8.GetBytes("\"badge_id\":");
        private readonly byte[] _comment_idName = Encoding.UTF8.GetBytes("\"comment_id\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _detailName = Encoding.UTF8.GetBytes("\"detail\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private readonly byte[] _post_idName = Encoding.UTF8.GetBytes("\"post_id\":");
        private readonly byte[] _post_typeName = Encoding.UTF8.GetBytes("\"post_type\":");
        private readonly byte[] _suggested_edit_idName = Encoding.UTF8.GetBytes("\"suggested_edit_id\":");
        private readonly byte[] _timeline_typeName = Encoding.UTF8.GetBytes("\"timeline_type\":");
        private readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");
        private readonly byte[] _user_idName = Encoding.UTF8.GetBytes("\"user_id\":");

        public UserTimeline Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new UserTimeline();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 9 && ReadUInt64(ref b, 0) == 8104636957754355568UL && ReadByte(ref b, 8) == 101)
                {
                    result.post_type = NullableFormatter<PostType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7957695015158116963UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 7310594974492882291UL && ReadUInt64(ref b, 8) == 7592915492774698852UL &&
                    ReadByte(ref b, 16) == 100)
                {
                    result.suggested_edit_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7308895159547947380UL && ReadUInt32(ref b, 8) == 1887007839U && ReadByte(ref b, 12) == 101)
                {
                    result.timeline_type = NullableFormatter<UserTimelineType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7235419165478379874UL)
                {
                    result.badge_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 6878243912808230755UL && ReadUInt16(ref b, 8) == 25705)
                {
                    result.comment_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1953722224U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.post_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1919251317U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.user_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1819568500U && ReadByte(ref b, 4) == 101)
                {
                    result.title = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1635018084U && ReadUInt16(ref b, 4) == 27753)
                {
                    result.detail = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, UserTimeline value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.creation_date != null)
            {
                writer.WriteUtf8Verbatim(_creation_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_post_typeName);
                NullableFormatter<PostType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.post_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.timeline_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_timeline_typeName);
                NullableFormatter<UserTimelineType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.timeline_type,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.user_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_user_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.user_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_post_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.post_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.comment_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_comment_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.comment_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.suggested_edit_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_suggested_edit_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.suggested_edit_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.title != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_titleName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.title, nestingLimit);
                writeSeparator = true;
            }

            if (value.detail != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_detailName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.detail, nestingLimit);
                writeSeparator = true;
            }

            if (value.link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_linkName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}