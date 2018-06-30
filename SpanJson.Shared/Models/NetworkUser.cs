using System;

namespace SpanJson.Shared.Models
{
    public class NetworkUser : IGenericEquality<NetworkUser>
    {
        public string site_name { get; set; }


        public string site_url { get; set; }


        public int? user_id { get; set; }


        public int? reputation { get; set; }


        public int? account_id { get; set; }


        public DateTime? creation_date { get; set; }


        public UserType? user_type { get; set; }


        public User.BadgeCount badge_counts { get; set; }


        public DateTime? last_access_date { get; set; }


        public int? answer_count { get; set; }


        public int? question_count { get; set; }

        public bool Equals(NetworkUser obj)
        {
            return
                account_id.TrueEquals(obj.account_id) &&
                answer_count.TrueEquals(obj.answer_count) &&
                badge_counts.TrueEquals(obj.badge_counts) &&
                creation_date.TrueEquals(obj.creation_date) &&
                last_access_date.TrueEquals(obj.last_access_date) &&
                question_count.TrueEquals(obj.question_count) &&
                reputation.TrueEquals(obj.reputation) &&
                site_name.TrueEqualsString(obj.site_name) &&
                site_url.TrueEqualsString(obj.site_url) &&
                user_id.TrueEquals(obj.user_id) &&
                user_type.TrueEquals(obj.user_type);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                account_id.TrueEquals((int?) obj.account_id) &&
                answer_count.TrueEquals((int?) obj.answer_count) &&
                (badge_counts == null && obj.badge_counts == null || badge_counts.EqualsDynamic(obj.badge_counts)) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                last_access_date.TrueEquals((DateTime?) obj.last_access_date) &&
                question_count.TrueEquals((int?) obj.question_count) &&
                reputation.TrueEquals((int?) obj.reputation) &&
                site_name.TrueEqualsString((string) obj.site_name) &&
                site_url.TrueEqualsString((string) obj.site_url) &&
                user_id.TrueEquals((int?) obj.user_id) &&
                user_type.TrueEquals((UserType?) obj.user_type);
        }
    }
}