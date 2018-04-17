using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum QuestionTimelineAction : byte
    {
        question = 1,
        answer = 2,
        comment = 3,
        unaccepted_answer = 4,
        accepted_answer = 5,
        vote_aggregate = 6,
        revision = 7,
        post_state_changed = 8
    }

    [ProtoContract]
    public class QuestionTimeline : IGenericEquality<QuestionTimeline>
    {
        [ProtoMember(1)]
        public QuestionTimelineAction? timeline_type { get; set; }

        [ProtoMember(2)]
        public int? question_id { get; set; }

        [ProtoMember(3)]
        public int? post_id { get; set; }

        [ProtoMember(4)]
        public int? comment_id { get; set; }

        [ProtoMember(5)]
        public string revision_guid { get; set; }

        [ProtoMember(6)]
        public int? up_vote_count { get; set; }

        [ProtoMember(7)]
        public int? down_vote_count { get; set; }

        [ProtoMember(8)]
        public DateTime? creation_date { get; set; }

        [ProtoMember(9)]
        public ShallowUser user { get; set; }

        [ProtoMember(10)]
        public ShallowUser owner { get; set; }

        public bool Equals(QuestionTimeline obj)
        {
            return
                comment_id.TrueEquals(obj.comment_id) &&
                creation_date.TrueEquals(obj.creation_date) &&
                down_vote_count.TrueEquals(obj.down_vote_count) &&
                owner.TrueEquals(obj.owner) &&
                post_id.TrueEquals(obj.post_id) &&
                question_id.TrueEquals(obj.question_id) &&
                revision_guid.TrueEqualsString(obj.revision_guid) &&
                timeline_type.TrueEquals(obj.timeline_type) &&
                up_vote_count.TrueEquals(obj.up_vote_count) &&
                user.TrueEquals(obj.user);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                comment_id.TrueEquals((int?) obj.comment_id) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                down_vote_count.TrueEquals((int?) obj.down_vote_count) &&
                (owner == null && obj.owner == null || owner.EqualsDynamic(obj.owner)) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                question_id.TrueEquals((int?) obj.question_id) &&
                revision_guid.TrueEqualsString((string) obj.revision_guid) &&
                timeline_type.TrueEquals((QuestionTimelineAction?) obj.timeline_type) &&
                up_vote_count.TrueEquals((int?) obj.up_vote_count) &&
                (user == null && obj.user == null || user.EqualsDynamic(obj.user));
        }
    }
}