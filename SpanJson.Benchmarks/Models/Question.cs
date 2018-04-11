using System;
using System.Collections.Generic;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class Question : IGenericEquality<Question>
    {
        [ProtoMember(1)] public int? question_id { get; set; }

        [ProtoMember(2)] public DateTime? last_edit_date { get; set; }

        [ProtoMember(3)] public DateTime? creation_date { get; set; }

        [ProtoMember(4)] public DateTime? last_activity_date { get; set; }

        [ProtoMember(5)] public DateTime? locked_date { get; set; }

        [ProtoMember(6)] public int? score { get; set; }

        [ProtoMember(7)] public DateTime? community_owned_date { get; set; }

        [ProtoMember(8)] public int? answer_count { get; set; }

        [ProtoMember(9)] public int? accepted_answer_id { get; set; }

        [ProtoMember(10)] public MigrationInfo migrated_to { get; set; }

        [ProtoMember(11)] public MigrationInfo migrated_from { get; set; }

        [ProtoMember(12)] public DateTime? bounty_closes_date { get; set; }

        [ProtoMember(13)] public int? bounty_amount { get; set; }

        [ProtoMember(14)] public DateTime? closed_date { get; set; }

        [ProtoMember(15)] public DateTime? protected_date { get; set; }

        [ProtoMember(16)] public string body { get; set; }

        [ProtoMember(17)] public string title { get; set; }

        [ProtoMember(18)] public List<string> tags { get; set; }

        [ProtoMember(19)] public string closed_reason { get; set; }

        [ProtoMember(20)] public int? up_vote_count { get; set; }

        [ProtoMember(21)] public int? down_vote_count { get; set; }

        [ProtoMember(22)] public int? favorite_count { get; set; }

        [ProtoMember(23)] public int? view_count { get; set; }

        [ProtoMember(24)] public ShallowUser owner { get; set; }

        [ProtoMember(25)] public List<Comment> comments { get; set; }

        [ProtoMember(26)] public List<Answer> answers { get; set; }

        [ProtoMember(27)] public string link { get; set; }

        [ProtoMember(28)] public bool? is_answered { get; set; }

        [ProtoMember(29)] public int? close_vote_count { get; set; }

        [ProtoMember(30)] public int? reopen_vote_count { get; set; }

        [ProtoMember(31)] public int? delete_vote_count { get; set; }

        [ProtoMember(32)] public Notice notice { get; set; }

        [ProtoMember(33)] public bool? upvoted { get; set; }

        [ProtoMember(34)] public bool? downvoted { get; set; }

        [ProtoMember(35)] public bool? favorited { get; set; }

        [ProtoMember(36)] public ShallowUser last_editor { get; set; }

        [ProtoMember(37)] public int? comment_count { get; set; }

        [ProtoMember(38)] public string body_markdown { get; set; }

        [ProtoMember(39)] public ClosedDetails closed_details { get; set; }

        [ProtoMember(40)] public string share_link { get; set; }

        public bool Equals(Question obj)
        {
            return
                accepted_answer_id.TrueEquals(obj.accepted_answer_id) &&
                answer_count.TrueEquals(obj.answer_count) &&
                answers.TrueEqualsList(obj.answers) &&
                body.TrueEqualsString(obj.body) &&
                body_markdown.TrueEqualsString(obj.body_markdown) &&
                bounty_amount.TrueEquals(obj.bounty_amount) &&
                bounty_closes_date.TrueEquals(obj.bounty_closes_date) &&
                close_vote_count.TrueEquals(obj.close_vote_count) &&
                closed_date.TrueEquals(obj.closed_date) &&
                closed_details.TrueEquals(obj.closed_details) &&
                closed_reason.TrueEqualsString(obj.closed_reason) &&
                comment_count.TrueEquals(obj.comment_count) &&
                comments.TrueEqualsList(obj.comments) &&
                community_owned_date.TrueEquals(obj.community_owned_date) &&
                creation_date.TrueEquals(obj.creation_date) &&
                delete_vote_count.TrueEquals(obj.delete_vote_count) &&
                down_vote_count.TrueEquals(obj.down_vote_count) &&
                downvoted.TrueEquals(obj.downvoted) &&
                favorite_count.TrueEquals(obj.favorite_count) &&
                favorited.TrueEquals(obj.favorited) &&
                is_answered.TrueEquals(obj.is_answered) &&
                last_activity_date.TrueEquals(obj.last_activity_date) &&
                last_edit_date.TrueEquals(obj.last_edit_date) &&
                last_editor.TrueEquals(obj.last_editor) &&
                link.TrueEqualsString(obj.link) &&
                locked_date.TrueEquals(obj.locked_date) &&
                migrated_from.TrueEquals(obj.migrated_from) &&
                migrated_to.TrueEquals(obj.migrated_to) &&
                notice.TrueEquals(obj.notice) &&
                owner.TrueEquals(obj.owner) &&
                protected_date.TrueEquals(obj.protected_date) &&
                question_id.TrueEquals(obj.question_id) &&
                reopen_vote_count.TrueEquals(obj.reopen_vote_count) &&
                score.TrueEquals(obj.score) &&
                share_link.TrueEqualsString(obj.share_link) &&
                tags.TrueEqualsString(obj.tags) &&
                title.TrueEqualsString(obj.title) &&
                up_vote_count.TrueEquals(obj.up_vote_count) &&
                upvoted.TrueEquals(obj.upvoted) &&
                view_count.TrueEquals(obj.view_count);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                accepted_answer_id.TrueEquals((int?) obj.accepted_answer_id) &&
                answer_count.TrueEquals((int?) obj.answer_count) &&
                answers.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.answers) &&
                body.TrueEqualsString((string) obj.body) &&
                body_markdown.TrueEqualsString((string) obj.body_markdown) &&
                bounty_amount.TrueEquals((int?) obj.bounty_amount) &&
                bounty_closes_date.TrueEquals((DateTime?) obj.bounty_closes_date) &&
                close_vote_count.TrueEquals((int?) obj.close_vote_count) &&
                closed_date.TrueEquals((DateTime?) obj.closed_date) &&
                (closed_details == null && obj.closed_details == null ||
                 closed_details.EqualsDynamic(obj.closed_details)) &&
                closed_reason.TrueEqualsString((string) obj.closed_reason) &&
                comment_count.TrueEquals((int?) obj.comment_count) &&
                comments.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.comments) &&
                community_owned_date.TrueEquals((DateTime?) obj.community_owned_date) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                delete_vote_count.TrueEquals((int?) obj.delete_vote_count) &&
                down_vote_count.TrueEquals((int?) obj.down_vote_count) &&
                downvoted.TrueEquals((bool?) obj.downvoted) &&
                favorite_count.TrueEquals((int?) obj.favorite_count) &&
                favorited.TrueEquals((bool?) obj.favorited) &&
                is_answered.TrueEquals((bool?) obj.is_answered) &&
                last_activity_date.TrueEquals((DateTime?) obj.last_activity_date) &&
                last_edit_date.TrueEquals((DateTime?) obj.last_edit_date) &&
                (last_editor == null && obj.last_editor == null || last_editor.EqualsDynamic(obj.last_editor)) &&
                link.TrueEqualsString((string) obj.link) &&
                locked_date.TrueEquals((DateTime?) obj.locked_date) &&
                (migrated_from == null && obj.migrated_from == null ||
                 migrated_from.EqualsDynamic(obj.migrated_from)) &&
                (migrated_to == null && obj.migrated_to == null || migrated_to.EqualsDynamic(obj.migrated_to)) &&
                (notice == null && obj.notice == null || notice.EqualsDynamic(obj.notice)) &&
                (owner == null && obj.owner == null || owner.EqualsDynamic(obj.owner)) &&
                protected_date.TrueEquals((DateTime?) obj.protected_date) &&
                question_id.TrueEquals((int?) obj.question_id) &&
                reopen_vote_count.TrueEquals((int?) obj.reopen_vote_count) &&
                score.TrueEquals((int?) obj.score) &&
                share_link.TrueEqualsString((string) obj.share_link) &&
                tags.TrueEqualsString((IEnumerable<string>) obj.tags) &&
                title.TrueEqualsString((string) obj.title) &&
                up_vote_count.TrueEquals((int?) obj.up_vote_count) &&
                upvoted.TrueEquals((bool?) obj.upvoted) &&
                view_count.TrueEquals((int?) obj.view_count);
        }

        [ProtoContract]
        public class ClosedDetails : IGenericEquality<ClosedDetails>
        {
            [ProtoMember(1)] public bool? on_hold { get; set; }

            [ProtoMember(2)] public string reason { get; set; }

            [ProtoMember(3)] public string description { get; set; }

            [ProtoMember(4)] public List<ShallowUser> by_users { get; set; }

            [ProtoMember(5)] public List<OriginalQuestion> original_questions { get; set; }

            public bool Equals(ClosedDetails obj)
            {
                return
                    by_users.TrueEqualsList(obj.by_users) &&
                    description.TrueEqualsString(obj.description) &&
                    on_hold.TrueEquals(obj.on_hold) &&
                    original_questions.TrueEqualsList(obj.original_questions) &&
                    reason.TrueEqualsString(obj.reason);
            }

            public bool EqualsDynamic(dynamic obj)
            {
                var oq = obj.original_questions;
                var oqI = (IEnumerable<dynamic>) oq;

                return
                    by_users.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.by_users) &&
                    description.TrueEqualsString((string) obj.description) &&
                    on_hold.TrueEquals((bool?) obj.on_hold) &&
                    //this.original_questions.TrueEqualsListDynamic((IEnumerable<dynamic>)obj.original_questions) &&
                    original_questions.TrueEqualsListDynamic(oqI) &&
                    reason.TrueEqualsString((string) obj.reason);
            }

            [ProtoContract]
            public class OriginalQuestion : IGenericEquality<OriginalQuestion>
            {
                [ProtoMember(1)] public int? question_id { get; set; }

                [ProtoMember(2)] public string title { get; set; }

                [ProtoMember(3)] public int? answer_count { get; set; }

                [ProtoMember(4)] public int? accepted_answer_id { get; set; }

                public bool Equals(OriginalQuestion obj)
                {
                    return
                        accepted_answer_id.TrueEquals(obj.accepted_answer_id) &&
                        answer_count.TrueEquals(obj.answer_count) &&
                        question_id.TrueEquals(obj.question_id) &&
                        title.TrueEqualsString(obj.title);
                }

                public bool EqualsDynamic(dynamic obj)
                {
                    return
                        accepted_answer_id.TrueEquals((int?) obj.accepted_answer_id) &&
                        answer_count.TrueEquals((int?) obj.answer_count) &&
                        question_id.TrueEquals((int?) obj.question_id) &&
                        title.TrueEqualsString((string) obj.title);
                }
            }
        }

        [ProtoContract]
        public class Notice : IGenericEquality<Notice>
        {
            [ProtoMember(1)] public string body { get; set; }

            [ProtoMember(2)] public DateTime? creation_date { get; set; }

            [ProtoMember(3)] public int? owner_user_id { get; set; }

            public bool Equals(Notice obj)
            {
                return
                    body.TrueEqualsString(obj.body) &&
                    creation_date.TrueEquals(obj.creation_date) &&
                    owner_user_id.TrueEquals(obj.owner_user_id);
            }

            public bool EqualsDynamic(dynamic obj)
            {
                return
                    body.TrueEqualsString((string) obj.body) &&
                    creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                    owner_user_id.TrueEquals((int?) obj.owner_user_id);
            }
        }

        [ProtoContract]
        public class MigrationInfo : IGenericEquality<MigrationInfo>
        {
            [ProtoMember(1)] public int? question_id { get; set; }

            [ProtoMember(2)] public Info.Site other_site { get; set; }

            [ProtoMember(3)] public DateTime? on_date { get; set; }

            public bool Equals(MigrationInfo obj)
            {
                return
                    on_date.TrueEquals(obj.on_date) &&
                    other_site.TrueEquals(obj.other_site) &&
                    question_id.TrueEquals(obj.question_id);
            }

            public bool EqualsDynamic(dynamic obj)
            {
                return
                    on_date.TrueEquals((DateTime?) obj.on_date) &&
                    (other_site == null && obj.other_site == null || other_site.EqualsDynamic(obj.other_site)) &&
                    question_id.TrueEquals((int?) obj.question_id);
            }
        }
    }
}