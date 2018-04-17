using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class User : IGenericEquality<User>
    {
        [ProtoMember(1)]
        public int? user_id { get; set; }

        [ProtoMember(2)]
        public UserType? user_type { get; set; }

        [ProtoMember(3)]
        public DateTime? creation_date { get; set; }

        [ProtoMember(4)]
        public string display_name { get; set; }

        [ProtoMember(5)]
        public string profile_image { get; set; }

        [ProtoMember(6)]
        public int? reputation { get; set; }

        [ProtoMember(7)]
        public int? reputation_change_day { get; set; }

        [ProtoMember(8)]
        public int? reputation_change_week { get; set; }

        [ProtoMember(9)]
        public int? reputation_change_month { get; set; }

        [ProtoMember(10)]
        public int? reputation_change_quarter { get; set; }

        [ProtoMember(11)]
        public int? reputation_change_year { get; set; }

        [ProtoMember(12)]
        public int? age { get; set; }

        [ProtoMember(13)]
        public DateTime? last_access_date { get; set; }

        [ProtoMember(14)]
        public DateTime? last_modified_date { get; set; }

        [ProtoMember(15)]
        public bool? is_employee { get; set; }

        [ProtoMember(16)]
        public string link { get; set; }

        [ProtoMember(17)]
        public string website_url { get; set; }

        [ProtoMember(18)]
        public string location { get; set; }

        [ProtoMember(19)]
        public int? account_id { get; set; }

        [ProtoMember(20)]
        public DateTime? timed_penalty_date { get; set; }

        [ProtoMember(21)]
        public BadgeCount badge_counts { get; set; }

        [ProtoMember(22)]
        public int? question_count { get; set; }

        [ProtoMember(23)]
        public int? answer_count { get; set; }

        [ProtoMember(24)]
        public int? up_vote_count { get; set; }

        [ProtoMember(25)]
        public int? down_vote_count { get; set; }

        [ProtoMember(26)]
        public string about_me { get; set; }

        [ProtoMember(27)]
        public int? view_count { get; set; }

        [ProtoMember(28)]
        public int? accept_rate { get; set; }

        public bool Equals(User obj)
        {
            return
                about_me.TrueEqualsString(obj.about_me) &&
                accept_rate.TrueEquals(obj.accept_rate) &&
                account_id.TrueEquals(obj.account_id) &&
                age.TrueEquals(obj.age) &&
                answer_count.TrueEquals(obj.answer_count) &&
                badge_counts.TrueEquals(obj.badge_counts) &&
                creation_date.TrueEquals(obj.creation_date) &&
                display_name.TrueEqualsString(obj.display_name) &&
                down_vote_count.TrueEquals(obj.down_vote_count) &&
                is_employee.TrueEquals(obj.is_employee) &&
                last_access_date.TrueEquals(obj.last_access_date) &&
                last_modified_date.TrueEquals(obj.last_modified_date) &&
                link.TrueEqualsString(obj.link) &&
                location.TrueEqualsString(obj.location) &&
                profile_image.TrueEqualsString(obj.profile_image) &&
                question_count.TrueEquals(obj.question_count) &&
                reputation.TrueEquals(obj.reputation) &&
                reputation_change_day.TrueEquals(obj.reputation_change_day) &&
                reputation_change_month.TrueEquals(obj.reputation_change_month) &&
                reputation_change_quarter.TrueEquals(obj.reputation_change_quarter) &&
                reputation_change_week.TrueEquals(obj.reputation_change_week) &&
                reputation_change_year.TrueEquals(obj.reputation_change_year) &&
                timed_penalty_date.TrueEquals(obj.timed_penalty_date) &&
                up_vote_count.TrueEquals(obj.up_vote_count) &&
                user_id.TrueEquals(obj.user_id) &&
                user_type.TrueEquals(obj.user_type) &&
                view_count.TrueEquals(obj.view_count) &&
                website_url.TrueEqualsString(obj.website_url);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                about_me.TrueEqualsString((string) obj.about_me) &&
                accept_rate.TrueEquals((int?) obj.accept_rate) &&
                account_id.TrueEquals((int?) obj.account_id) &&
                age.TrueEquals((int?) obj.age) &&
                answer_count.TrueEquals((int?) obj.answer_count) &&
                (badge_counts == null && obj.badge_counts == null || badge_counts.EqualsDynamic(obj.badge_counts)) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                display_name.TrueEqualsString((string) obj.display_name) &&
                down_vote_count.TrueEquals((int?) obj.down_vote_count) &&
                is_employee.TrueEquals((bool?) obj.is_employee) &&
                last_access_date.TrueEquals((DateTime?) obj.last_access_date) &&
                last_modified_date.TrueEquals((DateTime?) obj.last_modified_date) &&
                link.TrueEqualsString((string) obj.link) &&
                location.TrueEqualsString((string) obj.location) &&
                profile_image.TrueEqualsString((string) obj.profile_image) &&
                question_count.TrueEquals((int?) obj.question_count) &&
                reputation.TrueEquals((int?) obj.reputation) &&
                reputation_change_day.TrueEquals((int?) obj.reputation_change_day) &&
                reputation_change_month.TrueEquals((int?) obj.reputation_change_month) &&
                reputation_change_quarter.TrueEquals((int?) obj.reputation_change_quarter) &&
                reputation_change_week.TrueEquals((int?) obj.reputation_change_week) &&
                reputation_change_year.TrueEquals((int?) obj.reputation_change_year) &&
                timed_penalty_date.TrueEquals((DateTime?) obj.timed_penalty_date) &&
                up_vote_count.TrueEquals((int?) obj.up_vote_count) &&
                user_id.TrueEquals((int?) obj.user_id) &&
                user_type.TrueEquals((UserType?) obj.user_type) &&
                view_count.TrueEquals((int?) obj.view_count) &&
                website_url.TrueEqualsString((string) obj.website_url);
        }

        [ProtoContract]
        public class BadgeCount : IGenericEquality<BadgeCount>
        {
            [ProtoMember(1)]
            public int? gold { get; set; }

            [ProtoMember(2)]
            public int? silver { get; set; }

            [ProtoMember(3)]
            public int? bronze { get; set; }

            public bool Equals(BadgeCount obj)
            {
                return
                    bronze.TrueEquals(obj.bronze) &&
                    silver.TrueEquals(obj.silver) &&
                    gold.TrueEquals(obj.gold);
            }

            public bool EqualsDynamic(dynamic obj)
            {
                return
                    bronze.TrueEquals((int?) obj.bronze) &&
                    silver.TrueEquals((int?) obj.silver) &&
                    gold.TrueEquals((int?) obj.gold);
            }
        }
    }
}