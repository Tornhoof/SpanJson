using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class UserUtf8Formatter : BaseGeneratedFormatter<User, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<User, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly UserUtf8Formatter Default = new UserUtf8Formatter();
        private readonly byte[] _about_meName = Encoding.UTF8.GetBytes("\"about_me\":");
        private readonly byte[] _accept_rateName = Encoding.UTF8.GetBytes("\"accept_rate\":");
        private readonly byte[] _account_idName = Encoding.UTF8.GetBytes("\"account_id\":");
        private readonly byte[] _ageName = Encoding.UTF8.GetBytes("\"age\":");
        private readonly byte[] _answer_countName = Encoding.UTF8.GetBytes("\"answer_count\":");
        private readonly byte[] _badge_countsName = Encoding.UTF8.GetBytes("\"badge_counts\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _display_nameName = Encoding.UTF8.GetBytes("\"display_name\":");
        private readonly byte[] _down_vote_countName = Encoding.UTF8.GetBytes("\"down_vote_count\":");
        private readonly byte[] _is_employeeName = Encoding.UTF8.GetBytes("\"is_employee\":");
        private readonly byte[] _last_access_dateName = Encoding.UTF8.GetBytes("\"last_access_date\":");
        private readonly byte[] _last_modified_dateName = Encoding.UTF8.GetBytes("\"last_modified_date\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private readonly byte[] _locationName = Encoding.UTF8.GetBytes("\"location\":");
        private readonly byte[] _profile_imageName = Encoding.UTF8.GetBytes("\"profile_image\":");
        private readonly byte[] _question_countName = Encoding.UTF8.GetBytes("\"question_count\":");
        private readonly byte[] _reputation_change_dayName = Encoding.UTF8.GetBytes("\"reputation_change_day\":");
        private readonly byte[] _reputation_change_monthName = Encoding.UTF8.GetBytes("\"reputation_change_month\":");
        private readonly byte[] _reputation_change_quarterName = Encoding.UTF8.GetBytes("\"reputation_change_quarter\":");
        private readonly byte[] _reputation_change_weekName = Encoding.UTF8.GetBytes("\"reputation_change_week\":");
        private readonly byte[] _reputation_change_yearName = Encoding.UTF8.GetBytes("\"reputation_change_year\":");
        private readonly byte[] _reputationName = Encoding.UTF8.GetBytes("\"reputation\":");
        private readonly byte[] _timed_penalty_dateName = Encoding.UTF8.GetBytes("\"timed_penalty_date\":");
        private readonly byte[] _up_vote_countName = Encoding.UTF8.GetBytes("\"up_vote_count\":");
        private readonly byte[] _user_idName = Encoding.UTF8.GetBytes("\"user_id\":");
        private readonly byte[] _user_typeName = Encoding.UTF8.GetBytes("\"user_type\":");
        private readonly byte[] _view_countName = Encoding.UTF8.GetBytes("\"view_count\":");
        private readonly byte[] _website_urlName = Encoding.UTF8.GetBytes("\"website_url\":");

        public User Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new User();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 10 && ReadUInt64(ref b, 0) == 8462091486528629110UL && ReadUInt16(ref b, 8) == 29806)
                {
                    result.view_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 8390054783061815140UL && ReadUInt32(ref b, 8) == 1868783461U && ReadUInt16(ref b, 12) == 28277 &&
                    ReadByte(ref b, 14) == 116)
                {
                    result.down_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8241433869197468513UL && ReadUInt16(ref b, 8) == 29793 && ReadByte(ref b, 10) == 101)
                {
                    result.accept_rate = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 8104636957719884661UL && ReadByte(ref b, 8) == 101)
                {
                    result.user_type = NullableFormatter<UserType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8028915850849252201UL && ReadUInt16(ref b, 8) == 25977 && ReadByte(ref b, 10) == 101)
                {
                    result.is_employee = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
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

                if (length == 8 && ReadUInt64(ref b, 0) == 7957695015157985132UL)
                {
                    result.location = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 7598805624095270258UL)
                {
                    if (length >= 16 && ReadUInt64(ref b, 8) == 7453001534316441199UL)
                    {
                        if (length == 25 && ReadUInt64(ref b, 16) == 7310593858036916069UL && ReadByte(ref b, 24) == 114)
                        {
                            result.reputation_change_quarter =
                                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 23 && ReadUInt32(ref b, 16) == 1869438821U && ReadUInt16(ref b, 20) == 29806 && ReadByte(ref b, 22) == 104)
                        {
                            result.reputation_change_month = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 22 && ReadUInt32(ref b, 16) == 1702453093U && ReadUInt16(ref b, 20) == 29281)
                        {
                            result.reputation_change_year = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 22 && ReadUInt32(ref b, 16) == 1702322021U && ReadUInt16(ref b, 20) == 27493)
                        {
                            result.reputation_change_week = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 21 && ReadUInt32(ref b, 16) == 1633967973U && ReadByte(ref b, 20) == 121)
                        {
                            result.reputation_change_day = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf8Segment();
                        continue;
                    }

                    if (length == 10 && ReadUInt16(ref b, 8) == 28271)
                    {
                        result.reputation = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 7309447080025352564UL && ReadUInt64(ref b, 8) == 7017839094599016814UL &&
                    ReadUInt16(ref b, 16) == 25972)
                {
                    result.timed_penalty_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7308602724083262049UL)
                {
                    result.about_me = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 7237123382950715756UL && ReadUInt64(ref b, 8) == 7017839004152850025UL &&
                    ReadUInt16(ref b, 16) == 25972)
                {
                    result.last_modified_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
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

                if (length == 12 && ReadUInt64(ref b, 0) == 6879637024156117348UL && ReadUInt32(ref b, 8) == 1701667182U)
                {
                    result.display_name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 6878243981560603489UL && ReadUInt16(ref b, 8) == 25705)
                {
                    result.account_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 6874028428327088245UL && ReadUInt32(ref b, 8) == 1853189987U && ReadByte(ref b, 12) == 116)
                {
                    result.up_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 6874028402507146615UL && ReadUInt16(ref b, 8) == 29301 && ReadByte(ref b, 10) == 108)
                {
                    result.website_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 6874019606196875888UL && ReadUInt32(ref b, 8) == 1734438249U && ReadByte(ref b, 12) == 101)
                {
                    result.profile_image = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1919251317U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.user_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 3 && ReadUInt16(ref b, 0) == 26465 && ReadByte(ref b, 2) == 101)
                {
                    result.age = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, User value, int nestingLimit)
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

            if (value.reputation_change_day != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputation_change_dayName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation_change_day, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_week != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputation_change_weekName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation_change_week, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_month != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputation_change_monthName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation_change_month, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_quarter != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputation_change_quarterName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation_change_quarter, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_year != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputation_change_yearName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation_change_year, nestingLimit);
                writeSeparator = true;
            }

            if (value.age != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_ageName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.age, nestingLimit);
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

            if (value.last_modified_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_modified_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_modified_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_employee != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_employeeName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_employee, nestingLimit);
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

            if (value.website_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_website_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.website_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.location != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_locationName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.location, nestingLimit);
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

            if (value.timed_penalty_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_timed_penalty_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.timed_penalty_date, nestingLimit);
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

            if (value.up_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_up_vote_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.up_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.down_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_down_vote_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.down_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.about_me != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_about_meName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.about_me, nestingLimit);
                writeSeparator = true;
            }

            if (value.view_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_view_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.view_count, nestingLimit);
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

            writer.WriteUtf8EndObject();
        }
    }
}