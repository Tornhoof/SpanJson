using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class SuggestedEditUtf8Formatter : BaseGeneratedFormatter<SuggestedEdit, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<SuggestedEdit, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly SuggestedEditUtf8Formatter Default = new SuggestedEditUtf8Formatter();
        private readonly byte[] _approval_dateName = Encoding.UTF8.GetBytes("\"approval_date\":");
        private readonly byte[] _bodyName = Encoding.UTF8.GetBytes("\"body\":");
        private readonly byte[] _commentName = Encoding.UTF8.GetBytes("\"comment\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _post_idName = Encoding.UTF8.GetBytes("\"post_id\":");
        private readonly byte[] _post_typeName = Encoding.UTF8.GetBytes("\"post_type\":");
        private readonly byte[] _proposing_userName = Encoding.UTF8.GetBytes("\"proposing_user\":");
        private readonly byte[] _rejection_dateName = Encoding.UTF8.GetBytes("\"rejection_date\":");
        private readonly byte[] _suggested_edit_idName = Encoding.UTF8.GetBytes("\"suggested_edit_id\":");
        private readonly byte[] _tagsName = Encoding.UTF8.GetBytes("\"tags\":");
        private readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");

        public SuggestedEdit Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new SuggestedEdit();
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

                if (length == 14 && ReadUInt64(ref b, 0) == 8028075781016675698UL && ReadUInt32(ref b, 8) == 1633967982U && ReadUInt16(ref b, 12) == 25972)
                {
                    result.rejection_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7957695015158116963UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 7956017139191607920UL && ReadUInt32(ref b, 8) == 1937071975U && ReadUInt16(ref b, 12) == 29285)
                {
                    result.proposing_user = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7809653449870569569UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.approval_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 7310594974492882291UL && ReadUInt64(ref b, 8) == 7592915492774698852UL &&
                    ReadByte(ref b, 16) == 100)
                {
                    result.suggested_edit_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 2036625250U)
                {
                    result.body = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1953722224U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.post_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1936154996U)
                {
                    result.tags = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1835888483U && ReadUInt16(ref b, 4) == 28261 && ReadByte(ref b, 6) == 116)
                {
                    result.comment = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1819568500U && ReadByte(ref b, 4) == 101)
                {
                    result.title = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, SuggestedEdit value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.suggested_edit_id != null)
            {
                writer.WriteUtf8Verbatim(_suggested_edit_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.suggested_edit_id, nestingLimit);
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

            if (value.body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_bodyName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.body, nestingLimit);
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

            if (value.tags != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_tagsName);
                StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.tags, nestingLimit);
                writeSeparator = true;
            }

            if (value.comment != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_commentName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.comment, nestingLimit);
                writeSeparator = true;
            }

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_creation_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.approval_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_approval_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.approval_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.rejection_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_rejection_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.rejection_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.proposing_user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_proposing_userName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.proposing_user, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}