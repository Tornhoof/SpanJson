using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class UserUtf16Formatter : BaseGeneratedFormatter<User, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<User, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _user_idName = "\"user_id\":";
        private const string _user_typeName = "\"user_type\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _display_nameName = "\"display_name\":";
        private const string _profile_imageName = "\"profile_image\":";
        private const string _reputationName = "\"reputation\":";
        private const string _reputation_change_dayName = "\"reputation_change_day\":";
        private const string _reputation_change_weekName = "\"reputation_change_week\":";
        private const string _reputation_change_monthName = "\"reputation_change_month\":";
        private const string _reputation_change_quarterName = "\"reputation_change_quarter\":";
        private const string _reputation_change_yearName = "\"reputation_change_year\":";
        private const string _ageName = "\"age\":";
        private const string _last_access_dateName = "\"last_access_date\":";
        private const string _last_modified_dateName = "\"last_modified_date\":";
        private const string _is_employeeName = "\"is_employee\":";
        private const string _linkName = "\"link\":";
        private const string _website_urlName = "\"website_url\":";
        private const string _locationName = "\"location\":";
        private const string _account_idName = "\"account_id\":";
        private const string _timed_penalty_dateName = "\"timed_penalty_date\":";
        private const string _badge_countsName = "\"badge_counts\":";
        private const string _question_countName = "\"question_count\":";
        private const string _answer_countName = "\"answer_count\":";
        private const string _up_vote_countName = "\"up_vote_count\":";
        private const string _down_vote_countName = "\"down_vote_count\":";
        private const string _about_meName = "\"about_me\":";
        private const string _view_countName = "\"view_count\":";
        private const string _accept_rateName = "\"accept_rate\":";
        public static readonly UserUtf16Formatter Default = new UserUtf16Formatter();

        public User Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new User();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 12 && ReadUInt64(ref b, 0) == 33496016157016161UL && ReadUInt64(ref b, 8) == 27866430723719269UL &&
                    ReadUInt64(ref b, 16) == 32651569752506479UL)
                {
                    result.answer_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 33495956027146358UL && ReadUInt64(ref b, 8) == 32933049023004767UL &&
                    ReadUInt32(ref b, 16) == 7602286U)
                {
                    result.view_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 33214455281090677UL && ReadUInt64(ref b, 8) == 26740556586811503UL &&
                    ReadUInt64(ref b, 16) == 30962749956620387UL && ReadUInt16(ref b, 24) == 116)
                {
                    result.up_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32933053318103154UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 29555370777182324UL)
                    {
                        if (length >= 12 && ReadUInt64(ref b, 16) == 27866430723457135UL)
                        {
                            if (length >= 16 && ReadUInt64(ref b, 24) == 28992395053957224UL)
                            {
                                if (length == 25 && ReadUInt64(ref b, 32) == 32933057612677221UL && ReadUInt64(ref b, 40) == 28429470871453793UL &&
                                    ReadUInt16(ref b, 48) == 114)
                                {
                                    result.reputation_change_quarter =
                                        NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                                    continue;
                                }

                                if (length == 23 && ReadUInt64(ref b, 32) == 31244190572544101UL && ReadUInt32(ref b, 40) == 7602286U &&
                                    ReadUInt16(ref b, 44) == 104)
                                {
                                    result.reputation_change_month =
                                        NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                                    continue;
                                }

                                if (length == 22 && ReadUInt64(ref b, 32) == 28429492345045093UL && ReadUInt32(ref b, 40) == 7471201U)
                                {
                                    result.reputation_change_year =
                                        NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                                    continue;
                                }

                                if (length == 22 && ReadUInt64(ref b, 32) == 28429483755110501UL && ReadUInt32(ref b, 40) == 7012453U)
                                {
                                    result.reputation_change_week =
                                        NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                                    continue;
                                }

                                if (length == 21 && ReadUInt64(ref b, 32) == 27303502243889253UL && ReadUInt16(ref b, 40) == 121)
                                {
                                    result.reputation_change_day =
                                        NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                                    continue;
                                }

                                reader.SkipNextUtf16Segment();
                                continue;
                            }

                            reader.SkipNextUtf16Segment();
                            continue;
                        }

                        if (length == 10 && ReadUInt32(ref b, 16) == 7209071U)
                        {
                            result.reputation = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 32933049022939233UL && ReadUInt64(ref b, 8) == 28429440805437556UL)
                {
                    result.about_me = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32651591226032236UL)
                {
                    if (length == 18 && ReadUInt64(ref b, 8) == 28147974419578975UL && ReadUInt64(ref b, 16) == 28429423626027113UL &&
                        ReadUInt64(ref b, 24) == 27303502243889252UL && ReadUInt32(ref b, 32) == 6619252U)
                    {
                        result.last_modified_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 16 && ReadUInt64(ref b, 8) == 27866447902474335UL && ReadUInt64(ref b, 16) == 26740616716288101UL &&
                        ReadUInt64(ref b, 24) == 28429470870339684UL)
                    {
                        result.last_access_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 32370056121090161UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 32933049023004767UL && ReadUInt32(ref b, 24) == 7602286U)
                {
                    result.question_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 32370043235139703UL && ReadUInt64(ref b, 8) == 26740556586811497UL &&
                    ReadUInt32(ref b, 16) == 7471221U && ReadUInt16(ref b, 20) == 108)
                {
                    result.website_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

                if (length == 10 && ReadUInt64(ref b, 0) == 31244147623133281UL && ReadUInt64(ref b, 8) == 26740621010927733UL &&
                    ReadUInt32(ref b, 16) == 6553705U)
                {
                    result.account_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 30962758546554980UL && ReadUInt64(ref b, 8) == 32651574047539295UL &&
                    ReadUInt64(ref b, 16) == 31244147622871141UL && ReadUInt32(ref b, 24) == 7209077U && ReadUInt16(ref b, 28) == 116)
                {
                    result.down_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

                if (length == 18 && ReadUInt64(ref b, 0) == 28429440806092916UL && ReadUInt64(ref b, 8) == 28429453690339428UL &&
                    ReadUInt64(ref b, 16) == 32651561161261166UL && ReadUInt64(ref b, 24) == 27303502243889273UL && ReadUInt32(ref b, 32) == 6619252U)
                {
                    result.timed_penalty_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 28429397856026721UL && ReadUInt64(ref b, 8) == 32088555374510192UL &&
                    ReadUInt32(ref b, 16) == 7602273U && ReadUInt16(ref b, 20) == 101)
                {
                    result.accept_rate = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 28429380677206121UL && ReadUInt64(ref b, 8) == 31244186278690925UL &&
                    ReadUInt32(ref b, 16) == 6619257U && ReadUInt16(ref b, 20) == 101)
                {
                    result.is_employee = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303506540101731UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 32651513916817503UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 27303497949970540UL && ReadUInt64(ref b, 8) == 30962724186423412UL)
                {
                    result.location = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 3 && ReadUInt32(ref b, 0) == 6750305U && ReadUInt16(ref b, 4) == 101)
                {
                    result.age = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, User value, int nestingLimit)
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

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_creation_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
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

            if (value.reputation_change_day != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_change_dayName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation_change_day, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_week != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_change_weekName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation_change_week, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_month != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_change_monthName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation_change_month, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_quarter != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_change_quarterName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation_change_quarter,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_change_year != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_change_yearName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation_change_year, nestingLimit);
                writeSeparator = true;
            }

            if (value.age != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_ageName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.age, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_access_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_access_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_access_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_modified_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_modified_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_modified_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_employee != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_employeeName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_employee, nestingLimit);
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

            if (value.website_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_website_urlName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.website_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.location != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_locationName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.location, nestingLimit);
                writeSeparator = true;
            }

            if (value.account_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_account_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.account_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.timed_penalty_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_timed_penalty_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.timed_penalty_date, nestingLimit);
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

            if (value.question_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_question_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.question_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.answer_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_answer_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.answer_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.up_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_up_vote_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.up_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.down_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_down_vote_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.down_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.about_me != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_about_meName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.about_me, nestingLimit);
                writeSeparator = true;
            }

            if (value.view_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_view_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.view_count, nestingLimit);
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

            writer.WriteUtf16EndObject();
        }
    }
}