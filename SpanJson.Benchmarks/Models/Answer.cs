using System;
using System.Collections.Generic;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class Answer : IGenericEquality<Answer>
    {
        [ProtoMember(1)]
        public int? question_id { get; set; }

        [ProtoMember(2)]
        public int? answer_id { get; set; }

        [ProtoMember(3)]
        public DateTime? locked_date { get; set; }

        [ProtoMember(4)]
        public DateTime? creation_date { get; set; }

        [ProtoMember(5)]
        public DateTime? last_edit_date { get; set; }

        [ProtoMember(6)]
        public DateTime? last_activity_date { get; set; }

        [ProtoMember(7)]
        public int? score { get; set; }

        [ProtoMember(8)]
        public DateTime? community_owned_date { get; set; }

        [ProtoMember(9)]
        public bool? is_accepted { get; set; }

        [ProtoMember(10)]
        public string body { get; set; }

        [ProtoMember(11)]
        public ShallowUser owner { get; set; }

        [ProtoMember(12)]
        public string title { get; set; }

        [ProtoMember(13)]
        public int? up_vote_count { get; set; }

        [ProtoMember(14)]
        public int? down_vote_count { get; set; }

        [ProtoMember(15)]
        public List<Comment> comments { get; set; }

        [ProtoMember(16)]
        public string link { get; set; }

        [ProtoMember(17)]
        public List<string> tags { get; set; }

        [ProtoMember(18)]
        public bool? upvoted { get; set; }

        [ProtoMember(19)]
        public bool? downvoted { get; set; }

        [ProtoMember(20)]
        public bool? accepted { get; set; }

        [ProtoMember(21)]
        public ShallowUser last_editor { get; set; }

        [ProtoMember(22)]
        public int? comment_count { get; set; }

        [ProtoMember(23)]
        public string body_markdown { get; set; }

        [ProtoMember(24)]
        public string share_link { get; set; }

        public bool Equals(Answer obj)
        {
            return
                accepted.TrueEquals(obj.accepted) &&
                answer_id.TrueEquals(obj.answer_id) &&
                body.TrueEqualsString(obj.body) &&
                body_markdown.TrueEqualsString(obj.body_markdown) &&
                comment_count.TrueEquals(obj.comment_count) &&
                comments.TrueEqualsList(obj.comments) &&
                community_owned_date.TrueEquals(obj.community_owned_date) &&
                creation_date.TrueEquals(obj.creation_date) &&
                down_vote_count.TrueEquals(obj.down_vote_count) &&
                downvoted.TrueEquals(obj.downvoted) &&
                is_accepted.TrueEquals(obj.is_accepted) &&
                last_activity_date.TrueEquals(obj.last_activity_date) &&
                last_edit_date.TrueEquals(obj.last_edit_date) &&
                last_editor.TrueEquals(obj.last_editor) &&
                link.TrueEqualsString(obj.link) &&
                locked_date.TrueEquals(obj.locked_date) &&
                owner.TrueEquals(obj.owner) &&
                question_id.TrueEquals(obj.question_id) &&
                score.TrueEquals(obj.score) &&
                share_link.TrueEqualsString(obj.share_link) &&
                tags.TrueEqualsString(obj.tags) &&
                title.TrueEqualsString(obj.title) &&
                up_vote_count.TrueEquals(obj.up_vote_count) &&
                upvoted.TrueEquals(obj.upvoted);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                accepted.TrueEquals((bool?) obj.accepted) &&
                answer_id.TrueEquals((int?) obj.answer_id) &&
                body.TrueEqualsString((string) obj.body) &&
                body_markdown.TrueEqualsString((string) obj.body_markdown) &&
                comment_count.TrueEquals((int?) obj.comment_count) &&
                comments.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.comments) &&
                community_owned_date.TrueEquals((DateTime?) obj.community_owned_date) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                down_vote_count.TrueEquals((int?) obj.down_vote_count) &&
                downvoted.TrueEquals((bool?) obj.downvoted) &&
                is_accepted.TrueEquals((bool?) obj.is_accepted) &&
                last_activity_date.TrueEquals((DateTime?) obj.last_activity_date) &&
                last_edit_date.TrueEquals((DateTime?) obj.last_edit_date) &&
                (last_editor == null && obj.last_editor == null || last_editor.EqualsDynamic(obj.last_editor)) &&
                link.TrueEqualsString((string) obj.link) &&
                locked_date.TrueEquals((DateTime?) obj.locked_date) &&
                (owner == null && obj.owner == null || owner.EqualsDynamic(obj.owner)) &&
                question_id.TrueEquals((int?) obj.question_id) &&
                score.TrueEquals((int?) obj.score) &&
                share_link.TrueEqualsString((string) obj.share_link) &&
                tags.TrueEqualsString((IEnumerable<string>) obj.tags) &&
                title.TrueEqualsString((string) obj.title) &&
                up_vote_count.TrueEquals((int?) obj.up_vote_count) &&
                upvoted.TrueEquals((bool?) obj.upvoted);
        }
    }
}