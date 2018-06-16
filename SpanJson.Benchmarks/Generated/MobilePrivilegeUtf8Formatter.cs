using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobilePrivilegeUtf8Formatter : BaseGeneratedFormatter<MobilePrivilege, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<MobilePrivilege, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MobilePrivilegeUtf8Formatter Default = new MobilePrivilegeUtf8Formatter();
        private readonly byte[] _added_dateName = Encoding.UTF8.GetBytes("\"added_date\":");
        private readonly byte[] _group_idName = Encoding.UTF8.GetBytes("\"group_id\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private readonly byte[] _privilege_idName = Encoding.UTF8.GetBytes("\"privilege_id\":");
        private readonly byte[] _privilege_long_descriptionName = Encoding.UTF8.GetBytes("\"privilege_long_description\":");
        private readonly byte[] _privilege_short_descriptionName = Encoding.UTF8.GetBytes("\"privilege_short_description\":");
        private readonly byte[] _reputation_requiredName = Encoding.UTF8.GetBytes("\"reputation_required\":");
        private readonly byte[] _siteName = Encoding.UTF8.GetBytes("\"site\":");

        public MobilePrivilege Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new MobilePrivilege();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 19 && ReadUInt64(ref b, 0) == 7598805624095270258UL && ReadUInt64(ref b, 8) == 7599104626782072431UL &&
                    ReadUInt16(ref b, 16) == 25970 && ReadByte(ref b, 18) == 100)
                {
                    result.reputation_required = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 7450480358768341616UL)
                {
                    if (length == 26 && ReadUInt64(ref b, 8) == 7232613250593742693UL && ReadUInt64(ref b, 16) == 7598822069473866597UL &&
                        ReadUInt16(ref b, 24) == 28271)
                    {
                        result.privilege_long_description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 27 && ReadUInt64(ref b, 8) == 6878248353720917861UL && ReadUInt64(ref b, 16) == 8390322045806929252UL &&
                        ReadUInt16(ref b, 24) == 28521 && ReadByte(ref b, 26) == 110)
                    {
                        result.privilege_short_description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 12 && ReadUInt32(ref b, 8) == 1684627301U)
                    {
                        result.privilege_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7235419212958626407UL)
                {
                    result.group_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7017839004152521825UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.added_date = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1702127987U)
                {
                    result.site = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, MobilePrivilege value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.site != null)
            {
                writer.WriteUtf8Verbatim(_siteName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            if (value.privilege_short_description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_privilege_short_descriptionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.privilege_short_description, nestingLimit);
                writeSeparator = true;
            }

            if (value.privilege_long_description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_privilege_long_descriptionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.privilege_long_description, nestingLimit);
                writeSeparator = true;
            }

            if (value.privilege_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_privilege_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.privilege_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_required != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputation_requiredName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation_required, nestingLimit);
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

            if (value.group_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_group_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.group_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.added_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_added_dateName);
                NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.added_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}