using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class WritePermissionUtf8Formatter : BaseGeneratedFormatter<WritePermission, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<WritePermission, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly WritePermissionUtf8Formatter Default = new WritePermissionUtf8Formatter();
        private readonly byte[] _can_addName = Encoding.UTF8.GetBytes("\"can_add\":");
        private readonly byte[] _can_deleteName = Encoding.UTF8.GetBytes("\"can_delete\":");
        private readonly byte[] _can_editName = Encoding.UTF8.GetBytes("\"can_edit\":");
        private readonly byte[] _max_daily_actionsName = Encoding.UTF8.GetBytes("\"max_daily_actions\":");
        private readonly byte[] _min_seconds_between_actionsName = Encoding.UTF8.GetBytes("\"min_seconds_between_actions\":");
        private readonly byte[] _object_typeName = Encoding.UTF8.GetBytes("\"object_type\":");
        private readonly byte[] _user_idName = Encoding.UTF8.GetBytes("\"user_id\":");

        public WritePermission Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new WritePermission();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 8 && ReadUInt64(ref b, 0) == 8388346167509803363UL)
                {
                    result.can_edit = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8385549001439208047UL && ReadUInt16(ref b, 8) == 28793 && ReadByte(ref b, 10) == 101)
                {
                    result.object_type = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 27 && ReadUInt64(ref b, 0) == 8026370507101071725UL && ReadUInt64(ref b, 8) == 8607616260994458734UL &&
                    ReadUInt64(ref b, 16) == 7598807741144917349UL && ReadUInt16(ref b, 24) == 28271 && ReadByte(ref b, 26) == 115)
                {
                    result.min_seconds_between_actions = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 7811882112377184621UL && ReadUInt64(ref b, 8) == 7957695015191404409UL &&
                    ReadByte(ref b, 16) == 115)
                {
                    result.max_daily_actions = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7308327777087676771UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.can_delete = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1919251317U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.user_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1601069411U && ReadUInt16(ref b, 4) == 25697 && ReadByte(ref b, 6) == 100)
                {
                    result.can_add = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, WritePermission value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.user_id != null)
            {
                writer.WriteUtf8Verbatim(_user_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.user_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.object_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_object_typeName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.object_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.can_add != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_can_addName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.can_add, nestingLimit);
                writeSeparator = true;
            }

            if (value.can_edit != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_can_editName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.can_edit, nestingLimit);
                writeSeparator = true;
            }

            if (value.can_delete != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_can_deleteName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.can_delete, nestingLimit);
                writeSeparator = true;
            }

            if (value.max_daily_actions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_max_daily_actionsName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.max_daily_actions, nestingLimit);
                writeSeparator = true;
            }

            if (value.min_seconds_between_actions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_min_seconds_between_actionsName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.min_seconds_between_actions,
                    nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}