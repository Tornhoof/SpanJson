using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class NetworkUserUtf8Formatter : BaseGeneratedFormatter<NetworkUser, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<NetworkUser, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly NetworkUserUtf8Formatter Default = new NetworkUserUtf8Formatter();
        private readonly byte[] _account_idName = Encoding.UTF8.GetBytes("\"account_id\":");
        private readonly byte[] _answer_countName = Encoding.UTF8.GetBytes("\"answer_count\":");
        private readonly byte[] _badge_countsName = Encoding.UTF8.GetBytes("\"badge_counts\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _last_access_dateName = Encoding.UTF8.GetBytes("\"last_access_date\":");
        private readonly byte[] _question_countName = Encoding.UTF8.GetBytes("\"question_count\":");
        private readonly byte[] _reputationName = Encoding.UTF8.GetBytes("\"reputation\":");
        private readonly byte[] _site_nameName = Encoding.UTF8.GetBytes("\"site_name\":");
        private readonly byte[] _site_urlName = Encoding.UTF8.GetBytes("\"site_url\":");
        private readonly byte[] _user_idName = Encoding.UTF8.GetBytes("\"user_id\":");
        private readonly byte[] _user_typeName = Encoding.UTF8.GetBytes("\"user_type\":");

        public NetworkUser Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new NetworkUser();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 9 && ReadUInt64(ref b, 0) == 8104636957719884661UL && ReadByte(ref b, 8) == 101)
                {
                    result.user_type = NullableFormatter<UserType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 8026363850035323234UL && ReadUInt32(ref b, 8) == 1937010293U)
                {
                    result.badge_counts = BadgeCountUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 7957695015460107633UL && ReadUInt32(ref b, 8) == 1970234207U && ReadUInt16(ref b, 12) == 29806)
                {
                    result.question_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7957695015158116963UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 7881702178878155123UL && ReadByte(ref b, 8) == 101)
                {
                    result.site_name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7814437356025702771UL)
                {
                    result.site_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7598805624095270258UL && ReadUInt16(ref b, 8) == 28271)
                {
                    result.reputation = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 7161674895052726636UL && ReadUInt64(ref b, 8) == 7310575178855183205UL)
                {
                    result.last_access_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 7160567712663694945UL && ReadUInt32(ref b, 8) == 1953396079U)
                {
                    result.answer_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 6878243981560603489UL && ReadUInt16(ref b, 8) == 25705)
                {
                    result.account_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1919251317U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.user_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, NetworkUser value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.site_name != null)
            {
                writer.WriteUtf8Verbatim(_site_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.site_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_site_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site_url, nestingLimit);
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

            if (value.account_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_account_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.account_id, nestingLimit);
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

            if (value.badge_counts != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_countsName);
                BadgeCountUtf8Formatter.Default.Serialize(ref writer, value.badge_counts, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_access_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_access_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_access_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.answer_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_answer_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.answer_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.question_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_question_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.question_count, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}