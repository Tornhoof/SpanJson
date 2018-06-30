using System;
using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class Answer : IGenericEquality<Answer>
    {
        public int? question_id { get; set; }


        public int? answer_id { get; set; }


        public DateTime? locked_date { get; set; }


        public DateTime? creation_date { get; set; }


        public DateTime? last_edit_date { get; set; }


        public DateTime? last_activity_date { get; set; }


        public int? score { get; set; }


        public DateTime? community_owned_date { get; set; }


        public bool? is_accepted { get; set; }


        public string body { get; set; }


        public ShallowUser owner { get; set; }


        public string title { get; set; }


        public int? up_vote_count { get; set; }


        public int? down_vote_count { get; set; }


        public List<Comment> comments { get; set; }


        public string link { get; set; }


        public List<string> tags { get; set; }


        public bool? upvoted { get; set; }


        public bool? downvoted { get; set; }


        public bool? accepted { get; set; }


        public ShallowUser last_editor { get; set; }


        public int? comment_count { get; set; }


        public string body_markdown { get; set; }


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