using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class WritePermissionUtf16Formatter : BaseGeneratedFormatter<WritePermission, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<WritePermission, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _user_idName = "\"user_id\":";
        private const string _object_typeName = "\"object_type\":";
        private const string _can_addName = "\"can_add\":";
        private const string _can_editName = "\"can_edit\":";
        private const string _can_deleteName = "\"can_delete\":";
        private const string _max_daily_actionsName = "\"max_daily_actions\":";
        private const string _min_seconds_between_actionsName = "\"min_seconds_between_actions\":";
        public static readonly WritePermissionUtf16Formatter Default = new WritePermissionUtf16Formatter();

        public WritePermission Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new WritePermission();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 7 && ReadUInt64(ref b, 0) == 32088581144248437UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.user_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 28429427920732271UL && ReadUInt64(ref b, 8) == 32651505327931491UL &&
                    ReadUInt32(ref b, 16) == 7340153U && ReadUInt16(ref b, 20) == 101)
                {
                    result.object_type = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 26740638189944941UL && ReadUInt64(ref b, 8) == 30399748462674020UL &&
                    ReadUInt64(ref b, 16) == 27866439312408697UL && ReadUInt64(ref b, 24) == 30962724186423412UL && ReadUInt16(ref b, 32) == 115)
                {
                    result.max_daily_actions = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 27 && ReadUInt64(ref b, 0) == 26740595240796269UL && ReadUInt64(ref b, 8) == 31244147623264371UL &&
                    ReadUInt64(ref b, 16) == 26740616715305070UL && ReadUInt64(ref b, 24) == 33496020451393634UL &&
                    ReadUInt64(ref b, 32) == 26740595240534117UL && ReadUInt64(ref b, 40) == 29555370777313377UL && ReadUInt32(ref b, 48) == 7209071U &&
                    ReadUInt16(ref b, 52) == 115)
                {
                    result.min_seconds_between_actions = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 26740595240271971UL)
                {
                    if (length == 8 && ReadUInt64(ref b, 8) == 32651548276555877UL)
                    {
                        result.can_edit = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 10 && ReadUInt64(ref b, 8) == 28429436510863460UL && ReadUInt32(ref b, 16) == 6619252U)
                    {
                        result.can_delete = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 7 && ReadUInt32(ref b, 8) == 6553697U && ReadUInt16(ref b, 12) == 100)
                    {
                        result.can_add = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, WritePermission value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.user_id != null)
            {
                writer.WriteUtf16Verbatim(_user_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.user_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.object_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_object_typeName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.object_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.can_add != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_can_addName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.can_add, nestingLimit);
                writeSeparator = true;
            }

            if (value.can_edit != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_can_editName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.can_edit, nestingLimit);
                writeSeparator = true;
            }

            if (value.can_delete != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_can_deleteName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.can_delete, nestingLimit);
                writeSeparator = true;
            }

            if (value.max_daily_actions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_max_daily_actionsName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.max_daily_actions, nestingLimit);
                writeSeparator = true;
            }

            if (value.min_seconds_between_actions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_min_seconds_between_actionsName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.min_seconds_between_actions,
                    nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}