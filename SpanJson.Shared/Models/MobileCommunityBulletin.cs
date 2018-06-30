using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public sealed class MobileCommunityBulletin : IMobileFeedBase<MobileCommunityBulletin>
    {
        public enum CommunityBulletinType : byte
        {
            blog_post = 1,
            featured_meta_question = 2,
            upcoming_event = 3
        }


        public string site { get; set; }


        public string title { get; set; }


        public string link { get; set; }


        public CommunityBulletinType? bulletin_type { get; set; }


        public long? begin_date { get; set; }


        public long? end_date { get; set; }


        public string custom_date_string { get; set; }


        public List<string> tags { get; set; }


        public bool? is_deleted { get; set; }


        public bool? has_accepted_answer { get; set; }


        public int? answer_count { get; set; }


        public bool? is_promoted { get; set; }


        public int? group_id { get; set; }


        public long? added_date { get; set; }

        public bool Equals(MobileCommunityBulletin obj)
        {
            return
                added_date == obj.added_date &&
                answer_count == obj.answer_count &&
                begin_date == obj.begin_date &&
                bulletin_type == obj.bulletin_type &&
                custom_date_string == obj.custom_date_string &&
                end_date == obj.end_date &&
                group_id == obj.group_id &&
                has_accepted_answer == obj.has_accepted_answer &&
                is_deleted == obj.is_deleted &&
                is_promoted == obj.is_promoted &&
                link == obj.link &&
                site == obj.site &&
                tags.TrueEqualsString(obj.tags) &&
                title == obj.title;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                answer_count == (int?) obj.answer_count &&
                begin_date == (long?) obj.begin_date &&
                bulletin_type == (CommunityBulletinType?) obj.bulletin_type &&
                custom_date_string == (string) obj.custom_date_string &&
                end_date == (long?) obj.end_date &&
                group_id == (int?) obj.group_id &&
                has_accepted_answer == (bool?) obj.has_accepted_answer &&
                is_deleted == (bool?) obj.is_deleted &&
                is_promoted == (bool?) obj.is_promoted &&
                link == (string) obj.link &&
                site == (string) obj.site &&
                tags.TrueEqualsString((IEnumerable<string>) obj.tags) &&
                title == (string) obj.title;
        }
    }
}