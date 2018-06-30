namespace SpanJson.Shared.Models
{
    public class Badge : IGenericEquality<Badge>
    {
        public int? badge_id { get; set; }


        public BadgeRank? rank { get; set; }


        public string name { get; set; }


        public string description { get; set; }


        public int? award_count { get; set; }


        public BadgeType? badge_type { get; set; }


        public ShallowUser user { get; set; }


        public string link { get; set; }

        public bool Equals(Badge obj)
        {
            return
                award_count.TrueEquals(obj.award_count) &&
                badge_id.TrueEquals(obj.badge_id) &&
                badge_type.TrueEquals(obj.badge_type) &&
                description.TrueEqualsString(obj.description) &&
                link.TrueEqualsString(obj.link) &&
                name.TrueEqualsString(obj.name) &&
                rank.TrueEquals(obj.rank) &&
                user.TrueEquals(obj.user);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                award_count.TrueEquals((int?) obj.award_count) &&
                badge_id.TrueEquals((int?) obj.badge_id) &&
                badge_type.TrueEquals((BadgeType?) obj.badge_type) &&
                description.TrueEqualsString((string) obj.description) &&
                link.TrueEqualsString((string) obj.link) &&
                name.TrueEqualsString((string) obj.name) &&
                rank.TrueEquals((BadgeRank?) obj.rank) &&
                (user == null && obj.user == null || user.EqualsDynamic(obj.user));
        }
    }
}