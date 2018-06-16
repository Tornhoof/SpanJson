using System.Collections.Generic;
using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobileFeedUtf16Formatter : BaseGeneratedFormatter<MobileFeed, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<MobileFeed, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _hot_questionsName = "\"hot_questions\":";
        private const string _inbox_itemsName = "\"inbox_items\":";
        private const string _likely_to_answer_questionsName = "\"likely_to_answer_questions\":";
        private const string _reputation_eventsName = "\"reputation_events\":";
        private const string _cross_site_interesting_questionsName = "\"cross_site_interesting_questions\":";
        private const string _badgesName = "\"badges\":";
        private const string _earned_privilegesName = "\"earned_privileges\":";
        private const string _upcoming_privilegesName = "\"upcoming_privileges\":";
        private const string _community_bulletinsName = "\"community_bulletins\":";
        private const string _association_bonusesName = "\"association_bonuses\":";
        private const string _careers_job_adsName = "\"careers_job_ads\":";
        private const string _banner_adsName = "\"banner_ads\":";
        private const string _beforeName = "\"before\":";
        private const string _sinceName = "\"since\":";
        private const string _account_idName = "\"account_id\":";
        private const string _update_noticeName = "\"update_notice\":";
        public static readonly MobileFeedUtf16Formatter Default = new MobileFeedUtf16Formatter();

        public MobileFeed Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new MobileFeed();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 17 && ReadUInt64(ref b, 0) == 32933053318103154UL && ReadUInt64(ref b, 8) == 29555370777182324UL &&
                    ReadUInt64(ref b, 16) == 28429380676878447UL && ReadUInt64(ref b, 24) == 32651569751457910UL && ReadUInt16(ref b, 32) == 115)
                {
                    result.reputation_events =
                        ListFormatter<List<MobileRepChange>, MobileRepChange, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 32 && ReadUInt64(ref b, 0) == 32370099070566499UL && ReadUInt64(ref b, 8) == 29555366482083955UL &&
                    ReadUInt64(ref b, 16) == 29555280583131252UL && ReadUInt64(ref b, 24) == 32088581144313966UL &&
                    ReadUInt64(ref b, 32) == 29555370778361957UL && ReadUInt64(ref b, 40) == 31807080396947566UL &&
                    ReadUInt64(ref b, 48) == 32651591226294389UL && ReadUInt64(ref b, 56) == 32370094775402601UL)
                {
                    result.cross_site_interesting_questions =
                        ListFormatter<List<MobileQuestion>, MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 31244216343658593UL && ReadUInt64(ref b, 8) == 32651513917145187UL &&
                    ReadUInt64(ref b, 16) == 26740595241189481UL && ReadUInt64(ref b, 24) == 32933044728823906UL && ReadUInt32(ref b, 32) == 6619251U &&
                    ReadUInt16(ref b, 36) == 115)
                {
                    result.association_bonuses =
                        ListFormatter<List<MobileAssociationBonus>, MobileAssociationBonus, char, ExcludeNullsOriginalCaseResolver<char>>.Default
                            .Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref b, 0) == 31244160508166242UL && ReadUInt32(ref b, 8) == 6619250U)
                {
                    result.before = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 31244147623985269UL && ReadUInt64(ref b, 8) == 28992395054481517UL &&
                    ReadUInt64(ref b, 16) == 29555362188230751UL && ReadUInt64(ref b, 24) == 28429436511125622UL && ReadUInt32(ref b, 32) == 6619239U &&
                    ReadUInt16(ref b, 36) == 115)
                {
                    result.upcoming_privileges =
                        ListFormatter<List<MobilePrivilege>, MobilePrivilege, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 31244147623133281UL && ReadUInt64(ref b, 8) == 26740621010927733UL &&
                    ReadUInt32(ref b, 16) == 6553705U)
                {
                    result.account_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 31244143328886889UL && ReadUInt64(ref b, 8) == 32651548276228216UL &&
                    ReadUInt32(ref b, 16) == 7143525U && ReadUInt16(ref b, 20) == 115)
                {
                    result.inbox_items =
                        ListFormatter<List<MobileInboxItem>, MobileInboxItem, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 30962737070800997UL && ReadUInt64(ref b, 8) == 31525605420040293UL &&
                    ReadUInt64(ref b, 16) == 29555379367641202UL && ReadUInt64(ref b, 24) == 28429415036026988UL && ReadUInt16(ref b, 32) == 115)
                {
                    result.earned_privileges =
                        ListFormatter<List<MobilePrivilege>, MobilePrivilege, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 30962719890931810UL && ReadUInt64(ref b, 8) == 27303480770297957UL &&
                    ReadUInt32(ref b, 16) == 7536740U)
                {
                    result.banner_ads =
                        ListFormatter<List<MobileBannerAd>, MobileBannerAd, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 30681240620171363UL && ReadUInt64(ref b, 8) == 32651548277211253UL &&
                    ReadUInt64(ref b, 16) == 32932993188167801UL && ReadUInt64(ref b, 24) == 32651531097210988UL && ReadUInt32(ref b, 32) == 7209065U &&
                    ReadUInt16(ref b, 36) == 115)
                {
                    result.community_bulletins =
                        ListFormatter<List<MobileCommunityBulletin>, MobileCommunityBulletin, char, ExcludeNullsOriginalCaseResolver<char>>.Default
                            .Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref b, 0) == 28992352104284258UL && ReadUInt32(ref b, 8) == 7536741U)
                {
                    result.badges =
                        ListFormatter<List<MobileBadgeAward>, MobileBadgeAward, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 28429462280405091UL && ReadUInt64(ref b, 8) == 26740616716222565UL &&
                    ReadUInt64(ref b, 16) == 26740543701581930UL && ReadUInt32(ref b, 24) == 6553697U && ReadUInt16(ref b, 28) == 115)
                {
                    result.careers_job_ads = ListFormatter<List<MobileCareersJobAd>, MobileCareersJobAd, char, ExcludeNullsOriginalCaseResolver<char>>.Default
                        .Deserialize(ref reader);
                    continue;
                }

                if (length == 26 && ReadUInt64(ref b, 0) == 28429432216158316UL && ReadUInt64(ref b, 8) == 32651505328259180UL &&
                    ReadUInt64(ref b, 16) == 30962664056225903UL && ReadUInt64(ref b, 24) == 32088581144510579UL &&
                    ReadUInt64(ref b, 32) == 28429475166355551UL && ReadUInt64(ref b, 40) == 31244173394051187UL && ReadUInt32(ref b, 48) == 7536750U)
                {
                    result.likely_to_answer_questions =
                        ListFormatter<List<MobileQuestion>, MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 27866495147638899UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.since = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303502245003381UL && ReadUInt64(ref b, 8) == 30962655466684532UL &&
                    ReadUInt64(ref b, 16) == 27866473673523311UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.update_notice = MobileUpdateNoticeUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 26740621010993256UL && ReadUInt64(ref b, 8) == 32370056121090161UL &&
                    ReadUInt64(ref b, 16) == 30962724186423412UL && ReadUInt16(ref b, 24) == 115)
                {
                    result.hot_questions =
                        ListFormatter<List<MobileQuestion>, MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, MobileFeed value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.hot_questions != null)
            {
                writer.WriteUtf16Verbatim(_hot_questionsName);
                ListFormatter<List<MobileQuestion>, MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.hot_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.inbox_items != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_inbox_itemsName);
                ListFormatter<List<MobileInboxItem>, MobileInboxItem, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.inbox_items, nestingLimit);
                writeSeparator = true;
            }

            if (value.likely_to_answer_questions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_likely_to_answer_questionsName);
                ListFormatter<List<MobileQuestion>, MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.likely_to_answer_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation_events != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_eventsName);
                ListFormatter<List<MobileRepChange>, MobileRepChange, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.reputation_events, nestingLimit);
                writeSeparator = true;
            }

            if (value.cross_site_interesting_questions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_cross_site_interesting_questionsName);
                ListFormatter<List<MobileQuestion>, MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.cross_site_interesting_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.badges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_badgesName);
                ListFormatter<List<MobileBadgeAward>, MobileBadgeAward, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.badges, nestingLimit);
                writeSeparator = true;
            }

            if (value.earned_privileges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_earned_privilegesName);
                ListFormatter<List<MobilePrivilege>, MobilePrivilege, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.earned_privileges, nestingLimit);
                writeSeparator = true;
            }

            if (value.upcoming_privileges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_upcoming_privilegesName);
                ListFormatter<List<MobilePrivilege>, MobilePrivilege, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.upcoming_privileges, nestingLimit);
                writeSeparator = true;
            }

            if (value.community_bulletins != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_community_bulletinsName);
                ListFormatter<List<MobileCommunityBulletin>, MobileCommunityBulletin, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(
                    ref writer, value.community_bulletins, nestingLimit);
                writeSeparator = true;
            }

            if (value.association_bonuses != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_association_bonusesName);
                ListFormatter<List<MobileAssociationBonus>, MobileAssociationBonus, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.association_bonuses, nestingLimit);
                writeSeparator = true;
            }

            if (value.careers_job_ads != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_careers_job_adsName);
                ListFormatter<List<MobileCareersJobAd>, MobileCareersJobAd, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.careers_job_ads, nestingLimit);
                writeSeparator = true;
            }

            if (value.banner_ads != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_banner_adsName);
                ListFormatter<List<MobileBannerAd>, MobileBannerAd, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.banner_ads, nestingLimit);
                writeSeparator = true;
            }

            if (value.before != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_beforeName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.before, nestingLimit);
                writeSeparator = true;
            }

            if (value.since != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_sinceName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.since, nestingLimit);
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

            if (value.update_notice != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_update_noticeName);
                MobileUpdateNoticeUtf16Formatter.Default.Serialize(ref writer, value.update_notice, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}