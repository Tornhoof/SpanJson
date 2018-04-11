using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum UserTimelineType : byte
    {
        commented = 1,
        asked = 2,
        answered = 3,
        badge = 4,
        revision = 5,
        accepted = 6,
        reviewed = 7,
        suggested = 8
    }

    [ProtoContract]
    public class UserTimeline : IGenericEquality<UserTimeline>
    {
        [ProtoMember(1)] public DateTime? creation_date { get; set; }

        [ProtoMember(2)] public PostType? post_type { get; set; }

        [ProtoMember(3)] public UserTimelineType? timeline_type { get; set; }

        [ProtoMember(4)] public int? user_id { get; set; }

        [ProtoMember(5)] public int? post_id { get; set; }

        [ProtoMember(6)] public int? comment_id { get; set; }

        [ProtoMember(7)] public int? suggested_edit_id { get; set; }

        [ProtoMember(8)] public int? badge_id { get; set; }

        [ProtoMember(9)] public string title { get; set; }

        [ProtoMember(10)] public string detail { get; set; }

        [ProtoMember(11)] public string link { get; set; }

        public bool Equals(UserTimeline obj)
        {
            return
                badge_id.TrueEquals(obj.badge_id) &&
                comment_id.TrueEquals(obj.comment_id) &&
                creation_date.TrueEquals(obj.creation_date) &&
                detail.TrueEqualsString(obj.detail) &&
                link.TrueEqualsString(obj.link) &&
                post_id.TrueEquals(obj.post_id) &&
                post_type.TrueEquals(obj.post_type) &&
                suggested_edit_id.TrueEquals(obj.suggested_edit_id) &&
                timeline_type.TrueEquals(obj.timeline_type) &&
                title.TrueEqualsString(obj.title) &&
                user_id.TrueEquals(obj.user_id);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                badge_id.TrueEquals((int?) obj.badge_id) &&
                comment_id.TrueEquals((int?) obj.comment_id) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                detail.TrueEqualsString((string) obj.detail) &&
                link.TrueEqualsString((string) obj.link) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                post_type.TrueEquals((PostType?) obj.post_type) &&
                suggested_edit_id.TrueEquals((int?) obj.suggested_edit_id) &&
                timeline_type.TrueEquals((UserTimelineType?) obj.timeline_type) &&
                title.TrueEqualsString((string) obj.title) &&
                user_id.TrueEquals((int?) obj.user_id);
        }
    }
}