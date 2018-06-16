using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobilePrivilegeUtf16Formatter : BaseGeneratedFormatter<MobilePrivilege, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<MobilePrivilege, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _siteName = "\"site\":";
        private const string _privilege_short_descriptionName = "\"privilege_short_description\":";
        private const string _privilege_long_descriptionName = "\"privilege_long_description\":";
        private const string _privilege_idName = "\"privilege_id\":";
        private const string _reputation_requiredName = "\"reputation_required\":";
        private const string _linkName = "\"link\":";
        private const string _group_idName = "\"group_id\":";
        private const string _added_dateName = "\"added_date\":";
        public static readonly MobilePrivilegeUtf16Formatter Default = new MobilePrivilegeUtf16Formatter();

        public MobilePrivilege Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new MobilePrivilege();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 33214498230894704UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 28992356399972457UL)
                    {
                        if (length == 26 && ReadUInt64(ref b, 16) == 31244186277576805UL && ReadUInt64(ref b, 24) == 28147905699709038UL &&
                            ReadUInt64(ref b, 32) == 32088572554313829UL && ReadUInt64(ref b, 40) == 29555370778165353UL && ReadUInt32(ref b, 48) == 7209071U)
                        {
                            result.privilege_long_description = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 27 && ReadUInt64(ref b, 16) == 29273891505373285UL && ReadUInt64(ref b, 24) == 26740621011189871UL &&
                            ReadUInt64(ref b, 32) == 27866516622213220UL && ReadUInt64(ref b, 40) == 32651578341654642UL && ReadUInt32(ref b, 48) == 7274601U &&
                            ReadUInt16(ref b, 52) == 110)
                        {
                            result.privilege_short_description = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 12 && ReadUInt64(ref b, 16) == 28147948648857701UL)
                        {
                            result.privilege_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 32933053318103154UL && ReadUInt64(ref b, 8) == 29555370777182324UL &&
                    ReadUInt64(ref b, 16) == 32088555374116975UL && ReadUInt64(ref b, 24) == 29555375073198181UL && ReadUInt32(ref b, 32) == 6619250U &&
                    ReadUInt16(ref b, 36) == 100)
                {
                    result.reputation_required = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 32933049023987815UL && ReadUInt64(ref b, 8) == 28147948648857712UL)
                {
                    result.group_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 28429470870863987UL)
                {
                    result.site = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 28429402151059553UL && ReadUInt64(ref b, 8) == 27303502243889252UL &&
                    ReadUInt32(ref b, 16) == 6619252U)
                {
                    result.added_date = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, MobilePrivilege value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.site != null)
            {
                writer.WriteUtf16Verbatim(_siteName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            if (value.privilege_short_description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_privilege_short_descriptionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.privilege_short_description, nestingLimit);
                writeSeparator = true;
            }

            if (value.privilege_long_description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_privilege_long_descriptionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.privilege_long_description, nestingLimit);
                writeSeparator = true;
            }

            if (value.privilege_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_privilege_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.privilege_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_required != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_requiredName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation_required, nestingLimit);
                writeSeparator = true;
            }

            if (value.link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_linkName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.group_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_group_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.group_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.added_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_added_dateName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.added_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}