using System;

namespace SpanJson.Shared.Models
{
    public class QuestionTimeline : IGenericEquality<QuestionTimeline>
    {
        public QuestionTimelineAction? timeline_type { get; set; }


        public int? question_id { get; set; }


        public int? post_id { get; set; }


        public int? comment_id { get; set; }


        public string revision_guid { get; set; }


        public int? up_vote_count { get; set; }


        public int? down_vote_count { get; set; }


        public DateTime? creation_date { get; set; }


        public ShallowUser user { get; set; }


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