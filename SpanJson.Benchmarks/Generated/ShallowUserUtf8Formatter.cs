using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class ShallowUserUtf8Formatter<TResolver> : BaseGeneratedFormatter<ShallowUser, byte, TResolver>, IJsonFormatter<ShallowUser, byte, TResolver>
        where TResolver : class, IJsonFormatterResolver<byte, TResolver>, new()
    {
        private readonly byte[] _user_idName = Encoding.UTF8.GetBytes("\"user_id\":");
        private readonly byte[] _display_nameName = Encoding.UTF8.GetBytes("\"display_name\":");
        private readonly byte[] _reputationName = Encoding.UTF8.GetBytes("\"reputation\":");
        private readonly byte[] _user_typeName = Encoding.UTF8.GetBytes("\"user_type\":");
        private readonly byte[] _profile_imageName = Encoding.UTF8.GetBytes("\"profile_image\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private readonly byte[] _accept_rateName = Encoding.UTF8.GetBytes("\"accept_rate\":");
        private readonly byte[] _badge_countsName = Encoding.UTF8.GetBytes("\"badge_counts\":");
        public static readonly ShallowUserUtf8Formatter<TResolver> Default = new ShallowUserUtf8Formatter<TResolver>();

        public ShallowUser Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new ShallowUser();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var c = ref MemoryMarshal.GetReference(name);
                if (length == 11 && ReadUInt64(ref c, 0) == 8241433869197468513UL && ReadUInt16(ref c, 8) == 29793 && ReadByte(ref c, 10) == 101)
                {
                    result.accept_rate = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref c, 0) == 8104636957719884661UL && ReadByte(ref c, 8) == 101)
                {
                    result.user_type = NullableFormatter<UserType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref c, 0) == 8026363850035323234UL && ReadUInt32(ref c, 8) == 1937010293U)
                {
                    result.badge_counts = BadgeCountUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref c, 0) == 7598805624095270258UL && ReadUInt16(ref c, 8) == 28271)
                {
                    result.reputation = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref c, 0) == 6879637024156117348UL && ReadUInt32(ref c, 8) == 1701667182U)
                {
                    result.display_name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 6874019606196875888UL && ReadUInt32(ref c, 8) == 1734438249U && ReadByte(ref c, 12) == 101)
                {
                    result.profile_image = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref c, 0) == 1919251317U && ReadUInt16(ref c, 4) == 26975 && ReadByte(ref c, 6) == 100)
                {
                    result.user_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref c, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, ShallowUser value, int nestingLimit)
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

            if (value.display_name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_display_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.display_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputationName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation, nestingLimit);
                writeSeparator = true;
            }

            if (value.user_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_user_typeName);
                NullableFormatter<UserType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.user_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.profile_image != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_profile_imageName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.profile_image, nestingLimit);
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

            if (value.accept_rate != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_accept_rateName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.accept_rate, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_counts != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_countsName);
                BadgeCountUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_counts, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}