using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class MobileFeed : IGenericEquality<MobileFeed>
    {
        public List<MobileQuestion> hot_questions { get; set; }


        public List<MobileInboxItem> inbox_items { get; set; }


        public List<MobileQuestion> likely_to_answer_questions { get; set; }


        public List<MobileRepChange> reputation_events { get; set; }


        public List<MobileQuestion> cross_site_interesting_questions { get; set; }


        public List<MobileBadgeAward> badges { get; set; }


        public List<MobilePrivilege> earned_privileges { get; set; }


        public List<MobilePrivilege> upcoming_privileges { get; set; }


        public List<MobileCommunityBulletin> community_bulletins { get; set; }


        public List<MobileAssociationBonus> association_bonuses { get; set; }


        public List<MobileCareersJobAd> careers_job_ads { get; set; }


        public List<MobileBannerAd> banner_ads { get; set; }


        public long? before { get; set; }


        public long? since { get; set; }


        public int? account_id { get; set; }


        public MobileUpdateNotice update_notice { get; set; }

        public bool Equals(MobileFeed obj)
        {
            return
                account_id == obj.account_id &&
                association_bonuses.TrueEqualsList(obj.association_bonuses) &&
                badges.TrueEqualsList(obj.badges) &&
                banner_ads.TrueEqualsList(obj.banner_ads) &&
                before == obj.before &&
                careers_job_ads.TrueEqualsList(obj.careers_job_ads) &&
                community_bulletins.TrueEqualsList(obj.community_bulletins) &&
                cross_site_interesting_questions.TrueEqualsList(obj.cross_site_interesting_questions) &&
                earned_privileges.TrueEqualsList(obj.earned_privileges) &&
                hot_questions.TrueEqualsList(obj.hot_questions) &&
                inbox_items.TrueEqualsList(obj.inbox_items) &&
                likely_to_answer_questions.TrueEqualsList(obj.likely_to_answer_questions) &&
                reputation_events.TrueEqualsList(obj.reputation_events) &&
                since == obj.since &&
                upcoming_privileges.TrueEqualsList(obj.upcoming_privileges) &&
                update_notice.TrueEquals(obj.update_notice);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                account_id == (int?) obj.account_id &&
                association_bonuses.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.association_bonuses) &&
                badges.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.badges) &&
                banner_ads.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.banner_ads) &&
                before == (long?) obj.before &&
                careers_job_ads.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.careers_job_ads) &&
                community_bulletins.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.community_bulletins) &&
                cross_site_interesting_questions.TrueEqualsListDynamic(
                    (IEnumerable<dynamic>) obj.cross_site_interesting_questions) &&
                earned_privileges.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.earned_privileges) &&
                hot_questions.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.hot_questions) &&
                inbox_items.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.inbox_items) &&
                likely_to_answer_questions.TrueEqualsListDynamic(
                    (IEnumerable<dynamic>) obj.likely_to_answer_questions) &&
                reputation_events.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.reputation_events) &&
                since == (long?) obj.since &&
                upcoming_privileges.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.upcoming_privileges) &&
                (update_notice == null && obj.update_notice == null || update_notice.EqualsDynamic(obj.update_notice));
        }
    }
}