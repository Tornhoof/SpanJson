using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class MobileFeedUtf8Formatter : BaseGeneratedFormatter<MobileFeed, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<MobileFeed, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MobileFeedUtf8Formatter Default = new MobileFeedUtf8Formatter();
        private readonly byte[] _account_idName = Encoding.UTF8.GetBytes("\"account_id\":");
        private readonly byte[] _association_bonusesName = Encoding.UTF8.GetBytes("\"association_bonuses\":");
        private readonly byte[] _badgesName = Encoding.UTF8.GetBytes("\"badges\":");
        private readonly byte[] _banner_adsName = Encoding.UTF8.GetBytes("\"banner_ads\":");
        private readonly byte[] _beforeName = Encoding.UTF8.GetBytes("\"before\":");
        private readonly byte[] _careers_job_adsName = Encoding.UTF8.GetBytes("\"careers_job_ads\":");
        private readonly byte[] _community_bulletinsName = Encoding.UTF8.GetBytes("\"community_bulletins\":");
        private readonly byte[] _cross_site_interesting_questionsName = Encoding.UTF8.GetBytes("\"cross_site_interesting_questions\":");
        private readonly byte[] _earned_privilegesName = Encoding.UTF8.GetBytes("\"earned_privileges\":");
        private readonly byte[] _hot_questionsName = Encoding.UTF8.GetBytes("\"hot_questions\":");
        private readonly byte[] _inbox_itemsName = Encoding.UTF8.GetBytes("\"inbox_items\":");
        private readonly byte[] _likely_to_answer_questionsName = Encoding.UTF8.GetBytes("\"likely_to_answer_questions\":");
        private readonly byte[] _reputation_eventsName = Encoding.UTF8.GetBytes("\"reputation_events\":");
        private readonly byte[] _sinceName = Encoding.UTF8.GetBytes("\"since\":");
        private readonly byte[] _upcoming_privilegesName = Encoding.UTF8.GetBytes("\"upcoming_privileges\":");
        private readonly byte[] _update_noticeName = Encoding.UTF8.GetBytes("\"update_notice\":");

        public MobileFeed Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new MobileFeed();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 19 && ReadUInt64(ref b, 0) == 8388357231580376931UL && ReadUInt64(ref b, 8) == 8387229094129065849UL &&
                    ReadUInt16(ref b, 16) == 28265 && ReadByte(ref b, 18) == 115)
                {
                    result.community_bulletins =
                        ListFormatter<List<MobileCommunityBulletin>, MobileCommunityBulletin, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default
                            .Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8388340751823695465UL && ReadUInt16(ref b, 8) == 28005 && ReadByte(ref b, 10) == 115)
                {
                    result.inbox_items =
                        ListFormatter<List<MobileInboxItem>, MobileInboxItem, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 8386099856933090145UL && ReadUInt64(ref b, 8) == 8461823218174291817UL &&
                    ReadUInt16(ref b, 16) == 25971 && ReadByte(ref b, 18) == 115)
                {
                    result.association_bonuses =
                        ListFormatter<List<MobileAssociationBonus>, MobileAssociationBonus, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default
                            .Deserialize(ref reader);
                    continue;
                }

                if (length == 26 && ReadUInt64(ref b, 0) == 8385554537652119916UL && ReadUInt64(ref b, 8) == 8243126030628380527UL &&
                    ReadUInt64(ref b, 16) == 8028075849736876383UL && ReadUInt16(ref b, 24) == 29550)
                {
                    result.likely_to_answer_questions =
                        ListFormatter<List<MobileQuestion>, MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 8315181416802709352UL && ReadUInt32(ref b, 8) == 1852795252U && ReadByte(ref b, 12) == 115)
                {
                    result.hot_questions =
                        ListFormatter<List<MobileQuestion>, MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 8097301041842905445UL && ReadUInt64(ref b, 8) == 7306920436732160370UL &&
                    ReadByte(ref b, 16) == 115)
                {
                    result.earned_privileges =
                        ListFormatter<List<MobilePrivilege>, MobilePrivilege, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7953187017484169333UL && ReadUInt32(ref b, 8) == 1667855471U && ReadByte(ref b, 12) == 101)
                {
                    result.update_notice = MobileUpdateNoticeUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 7598805624095270258UL && ReadUInt64(ref b, 8) == 8389754715019112047UL &&
                    ReadByte(ref b, 16) == 115)
                {
                    result.reputation_events =
                        ListFormatter<List<MobileRepChange>, MobileRepChange, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 32 && ReadUInt64(ref b, 0) == 7598521945766720099UL && ReadUInt64(ref b, 8) == 8243122710534645108UL &&
                    ReadUInt64(ref b, 16) == 8169361972986671973UL && ReadUInt64(ref b, 24) == 8317708060515853685UL)
                {
                    result.cross_site_interesting_questions =
                        ListFormatter<List<MobileQuestion>, MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 7453010352085889141UL && ReadUInt64(ref b, 8) == 7308332252611637343UL &&
                    ReadUInt16(ref b, 16) == 25959 && ReadByte(ref b, 18) == 115)
                {
                    result.upcoming_privileges =
                        ListFormatter<List<MobilePrivilege>, MobilePrivilege, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7016452524436513122UL && ReadUInt16(ref b, 8) == 29540)
                {
                    result.banner_ads =
                        ListFormatter<List<MobileBannerAd>, MobileBannerAd, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 6878243981560603489UL && ReadUInt16(ref b, 8) == 25705)
                {
                    result.account_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 6877966835744137571UL && ReadUInt32(ref b, 8) == 1600286570U && ReadUInt16(ref b, 12) == 25697 &&
                    ReadByte(ref b, 14) == 115)
                {
                    result.careers_job_ads = ListFormatter<List<MobileCareersJobAd>, MobileCareersJobAd, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default
                        .Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1868981602U && ReadUInt16(ref b, 4) == 25970)
                {
                    result.before = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1734631778U && ReadUInt16(ref b, 4) == 29541)
                {
                    result.badges =
                        ListFormatter<List<MobileBadgeAward>, MobileBadgeAward, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1668180339U && ReadByte(ref b, 4) == 101)
                {
                    result.since = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, MobileFeed value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.hot_questions != null)
            {
                writer.WriteUtf8Verbatim(_hot_questionsName);
                ListFormatter<List<MobileQuestion>, MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.hot_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.inbox_items != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_inbox_itemsName);
                ListFormatter<List<MobileInboxItem>, MobileInboxItem, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.inbox_items, nestingLimit);
                writeSeparator = true;
            }

            if (value.likely_to_answer_questions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_likely_to_answer_questionsName);
                ListFormatter<List<MobileQuestion>, MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.likely_to_answer_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_events != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputation_eventsName);
                ListFormatter<List<MobileRepChange>, MobileRepChange, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.reputation_events, nestingLimit);
                writeSeparator = true;
            }

            if (value.cross_site_interesting_questions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_cross_site_interesting_questionsName);
                ListFormatter<List<MobileQuestion>, MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.cross_site_interesting_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.badges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badgesName);
                ListFormatter<List<MobileBadgeAward>, MobileBadgeAward, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.badges, nestingLimit);
                writeSeparator = true;
            }

            if (value.earned_privileges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_earned_privilegesName);
                ListFormatter<List<MobilePrivilege>, MobilePrivilege, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.earned_privileges, nestingLimit);
                writeSeparator = true;
            }

            if (value.upcoming_privileges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_upcoming_privilegesName);
                ListFormatter<List<MobilePrivilege>, MobilePrivilege, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.upcoming_privileges, nestingLimit);
                writeSeparator = true;
            }

            if (value.community_bulletins != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_community_bulletinsName);
                ListFormatter<List<MobileCommunityBulletin>, MobileCommunityBulletin, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(
                    ref writer, value.community_bulletins, nestingLimit);
                writeSeparator = true;
            }

            if (value.association_bonuses != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_association_bonusesName);
                ListFormatter<List<MobileAssociationBonus>, MobileAssociationBonus, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.association_bonuses, nestingLimit);
                writeSeparator = true;
            }

            if (value.careers_job_ads != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_careers_job_adsName);
                ListFormatter<List<MobileCareersJobAd>, MobileCareersJobAd, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.careers_job_ads, nestingLimit);
                writeSeparator = true;
            }

            if (value.banner_ads != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_banner_adsName);
                ListFormatter<List<MobileBannerAd>, MobileBannerAd, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.banner_ads, nestingLimit);
                writeSeparator = true;
            }

            if (value.before != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_beforeName);
                NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.before, nestingLimit);
                writeSeparator = true;
            }

            if (value.since != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_sinceName);
                NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.since, nestingLimit);
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

            if (value.update_notice != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_update_noticeName);
                MobileUpdateNoticeUtf8Formatter.Default.Serialize(ref writer, value.update_notice, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}