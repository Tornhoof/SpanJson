using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum NotificationType : byte
    {
        generic = 1,
        accounts_associated = 8,
        badge_earned = 5,
        profile_activity = 2,
        bounty_expired = 3,
        bounty_expires_in_one_day = 4,
        bounty_expires_in_three_days = 6,
        edit_suggested = 22,
        new_privilege = 9,
        post_migrated = 10,
        moderator_message = 11,
        registration_reminder = 12,
        substantive_edit = 23,
        reputation_bonus = 7,
        bounty_grace_period_started = 24
    }

    [ProtoContract]
    public class Notification : IGenericEquality<Notification>
    {
        [ProtoMember(1)] public NotificationType? notification_type { get; set; }

        [ProtoMember(2)] public Info.Site site { get; set; }

        [ProtoMember(3)] public DateTime? creation_date { get; set; }

        [ProtoMember(4)] public string body { get; set; }

        [ProtoMember(5)] public int? post_id { get; set; }

        [ProtoMember(6)] public bool? is_unread { get; set; }

        public bool Equals(Notification obj)
        {
            return
                body.TrueEqualsString(obj.body) &&
                site.TrueEquals(obj.site) &&
                creation_date.TrueEquals(obj.creation_date) &&
                post_id.TrueEquals(obj.post_id) &&
                is_unread.TrueEquals(obj.is_unread);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                body.TrueEqualsString((string) obj.body) &&
                (site == null && obj.site == null || site.EqualsDynamic(obj.site)) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                is_unread.TrueEquals((bool?) obj.is_unread);
        }
    }
}