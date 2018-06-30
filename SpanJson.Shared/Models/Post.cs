using System;
using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class Post : IGenericEquality<Post>
    {
        public int? post_id { get; set; }


        public PostType? post_type { get; set; }


        public string body { get; set; }


        public ShallowUser owner { get; set; }


        public DateTime? creation_date { get; set; }


        public DateTime? last_activity_date { get; set; }


        public DateTime? last_edit_date { get; set; }


        public int? score { get; set; }


        public int? up_vote_count { get; set; }


        public int? down_vote_count { get; set; }


        public List<Comment> comments { get; set; }


        public string link { get; set; }


        public bool? upvoted { get; set; }


        public bool? downvoted { get; set; }


        public ShallowUser last_editor { get; set; }


        public int? comment_count { get; set; }


        public string body_markdown { get; set; }


        public string share_link { get; set; }

        public bool Equals(Post obj)
        {
            return
                body.TrueEqualsString(obj.body) &&
                body_markdown.TrueEqualsString(obj.body_markdown) &&
                comment_count.TrueEquals(obj.comment_count) &&
                comments.TrueEqualsList(obj.comments) &&
                creation_date.TrueEquals(obj.creation_date) &&
                down_vote_count.TrueEquals(obj.down_vote_count) &&
                downvoted.TrueEquals(obj.downvoted) &&
                last_activity_date.TrueEquals(obj.last_activity_date) &&
                last_edit_date.TrueEquals(obj.last_edit_date) &&
                last_editor.TrueEquals(obj.last_editor) &&
                link.TrueEqualsString(obj.link) &&
                owner.TrueEquals(obj.owner) &&
                post_id.TrueEquals(obj.post_id) &&
                post_type.TrueEquals(obj.post_type) &&
                score.TrueEquals(obj.score) &&
                share_link.TrueEqualsString(obj.share_link) &&
                up_vote_count.TrueEquals(obj.up_vote_count) &&
                upvoted.TrueEquals(obj.upvoted);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                body.TrueEqualsString((string) obj.body) &&
                body_markdown.TrueEqualsString((string) obj.body_markdown) &&
                comment_count.TrueEquals((int?) obj.comment_count) &&
                comments.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.comments) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                down_vote_count.TrueEquals((int?) obj.down_vote_count) &&
                downvoted.TrueEquals((bool?) obj.downvoted) &&
                last_activity_date.TrueEquals((DateTime?) obj.last_activity_date) &&
                last_edit_date.TrueEquals((DateTime?) obj.last_edit_date) &&
                (last_editor == null && obj.last_editor == null || last_editor.EqualsDynamic(obj.last_editor)) &&
                link.TrueEqualsString((string) obj.link) &&
                (owner == null && obj.owner == null || owner.EqualsDynamic(obj.owner)) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                post_type.TrueEquals((PostType?) obj.post_type) &&
                score.TrueEquals((int?) obj.score) &&
                share_link.TrueEqualsString((string) obj.share_link) &&
                up_vote_count.TrueEquals((int?) obj.up_vote_count) &&
                upvoted.TrueEquals((bool?) obj.upvoted);
        }
    }
}