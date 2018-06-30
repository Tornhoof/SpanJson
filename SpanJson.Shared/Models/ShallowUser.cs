namespace SpanJson.Shared.Models
{
    public class ShallowUser : IGenericEquality<ShallowUser>
    {
        public int? user_id { get; set; }


        public string display_name { get; set; }


        public int? reputation { get; set; }


        public UserType? user_type { get; set; }


        public string profile_image { get; set; }


        public string link { get; set; }


        public int? accept_rate { get; set; }


        public User.BadgeCount badge_counts { get; set; }

        public bool Equals(ShallowUser obj)
        {
            return
                accept_rate.TrueEquals(obj.accept_rate) &&
                badge_counts.TrueEquals(obj.badge_counts) &&
                display_name.TrueEqualsString(obj.display_name) &&
                link.TrueEqualsString(obj.link) &&
                profile_image.TrueEqualsString(obj.profile_image) &&
                reputation.TrueEquals(obj.reputation) &&
                user_id.TrueEquals(obj.user_id) &&
                user_type.TrueEquals(obj.user_type);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                accept_rate.TrueEquals((int?) obj.accept_rate) &&
                (badge_counts == null && obj.badge_counts == null || badge_counts.EqualsDynamic(obj.badge_counts)) &&
                display_name.TrueEqualsString((string) obj.display_name) &&
                link.TrueEqualsString((string) obj.link) &&
                profile_image.TrueEqualsString((string) obj.profile_image) &&
                reputation.TrueEquals((int?) obj.reputation) &&
                user_id.TrueEquals((int?) obj.user_id) &&
                user_type.TrueEquals((UserType?) obj.user_type);
        }
    }
}