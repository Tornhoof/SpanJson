using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class ShallowUserUtf16Formatter : BaseGeneratedFormatter<ShallowUser, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<ShallowUser, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _user_idName = "\"user_id\":";
        private const string _display_nameName = "\"display_name\":";
        private const string _reputationName = "\"reputation\":";
        private const string _user_typeName = "\"user_type\":";
        private const string _profile_imageName = "\"profile_image\":";
        private const string _linkName = "\"link\":";
        private const string _accept_rateName = "\"accept_rate\":";
        private const string _badge_countsName = "\"badge_counts\":";
        public static readonly ShallowUserUtf16Formatter Default = new ShallowUserUtf16Formatter();

        public ShallowUser Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new ShallowUser();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 10 && ReadUInt64(ref b, 0) == 32933053318103154UL && ReadUInt64(ref b, 8) == 29555370777182324UL &&
                    ReadUInt32(ref b, 16) == 7209071U)
                {
                    result.reputation = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32088581144248437UL)
                {
                    if (length == 9 && ReadUInt64(ref b, 8) == 31525717090238559UL && ReadUInt16(ref b, 16) == 101)
                    {
                        result.user_type = NullableFormatter<UserType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 7 && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                    {
                        result.user_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 31525691319713892UL && ReadUInt64(ref b, 8) == 26740642484912236UL &&
                    ReadUInt64(ref b, 16) == 28429440805568622UL)
                {
                    result.display_name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 28992352104284258UL && ReadUInt64(ref b, 8) == 31244147622871141UL &&
                    ReadUInt64(ref b, 16) == 32370120545140853UL)
                {
                    result.badge_counts = BadgeCountUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 28710924373327984UL && ReadUInt64(ref b, 8) == 26740556586287209UL &&
                    ReadUInt64(ref b, 16) == 28992339220168809UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.profile_image = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 28429397856026721UL && ReadUInt64(ref b, 8) == 32088555374510192UL &&
                    ReadUInt32(ref b, 16) == 7602273U && ReadUInt16(ref b, 20) == 101)
                {
                    result.accept_rate = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, ShallowUser value, int nestingLimit)
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

            if (value.display_name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_display_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.display_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputationName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation, nestingLimit);
                writeSeparator = true;
            }

            if (value.user_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_user_typeName);
                NullableFormatter<UserType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.user_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.profile_image != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_profile_imageName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.profile_image, nestingLimit);
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

            if (value.accept_rate != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_accept_rateName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.accept_rate, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_counts != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_badge_countsName);
                BadgeCountUtf16Formatter.Default.Serialize(ref writer, value.badge_counts, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}