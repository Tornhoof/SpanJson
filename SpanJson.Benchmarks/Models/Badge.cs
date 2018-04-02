using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum BadgeRank : byte
    {
        bronze = 3,
        silver = 2,
        gold = 1
    }

    public enum BadgeType
    {
        named = 1,
        tag_based = 2
    }

    [ProtoContract]
    public class Badge : IGenericEquality<Badge>
    {
        [ProtoMember(1)]
        public int? badge_id { get; set; }

        [ProtoMember(2)]
        public BadgeRank? rank { get; set; }

        [ProtoMember(3)]
        public string name { get; set; }

        [ProtoMember(4)]
        public string description { get; set; }

        [ProtoMember(5)]
        public int? award_count { get; set; }

        [ProtoMember(6)]
        public BadgeType? badge_type { get; set; }

        [ProtoMember(7)]
        public ShallowUser user { get; set; }

        [ProtoMember(8)]
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