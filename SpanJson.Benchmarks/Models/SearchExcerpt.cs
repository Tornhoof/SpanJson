using System;
using System.Collections.Generic;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum SearchExcerptItemType : byte
    {
        question = 1,
        answer = 2
    }

    [ProtoContract]
    public class SearchExcerpt : IGenericEquality<SearchExcerpt>
    {
        [ProtoMember(1)]
        public string title { get; set; }

        [ProtoMember(2)]
        public string excerpt { get; set; }

        [ProtoMember(3)]
        public DateTime? community_owned_date { get; set; }

        [ProtoMember(4)]
        public DateTime? locked_date { get; set; }

        [ProtoMember(5)]
        public DateTime? creation_date { get; set; }

        [ProtoMember(6)]
        public DateTime? last_activity_date { get; set; }

        [ProtoMember(7)]
        public ShallowUser owner { get; set; }

        [ProtoMember(8)]
        public ShallowUser last_activity_user { get; set; }

        [ProtoMember(9)]
        public int? score { get; set; }

        [ProtoMember(10)]
        public SearchExcerptItemType? item_type { get; set; }

        [ProtoMember(11)]
        public string body { get; set; }

        [ProtoMember(12)]
        public int? question_id { get; set; }

        [ProtoMember(13)]
        public bool? is_answered { get; set; }

        [ProtoMember(14)]
        public int? answer_count { get; set; }

        [ProtoMember(15)]
        public List<string> tags { get; set; }

        [ProtoMember(16)]
        public DateTime? closed_date { get; set; }

        [ProtoMember(17)]
        public int? answer_id { get; set; }

        [ProtoMember(18)]
        public bool? is_accepted { get; set; }

        public bool Equals(SearchExcerpt obj)
        {
            return
                answer_count.TrueEquals(obj.answer_count) &&
                answer_id.TrueEquals(obj.answer_id) &&
                body.TrueEqualsString(obj.body) &&
                closed_date.TrueEquals(obj.closed_date) &&
                community_owned_date.TrueEquals(obj.community_owned_date) &&
                creation_date.TrueEquals(obj.creation_date) &&
                excerpt.TrueEqualsString(obj.excerpt) &&
                is_accepted.TrueEquals(obj.is_accepted) &&
                is_answered.TrueEquals(obj.is_answered) &&
                item_type.TrueEquals(obj.item_type) &&
                last_activity_date.TrueEquals(obj.last_activity_date) &&
                last_activity_user.TrueEquals(obj.last_activity_user) &&
                locked_date.TrueEquals(obj.locked_date) &&
                owner.TrueEquals(obj.owner) &&
                question_id.TrueEquals(obj.question_id) &&
                score.TrueEquals(obj.score) &&
                tags.TrueEqualsString(obj.tags) &&
                title.TrueEqualsString(obj.title);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                answer_count.TrueEquals((int?) obj.answer_count) &&
                answer_id.TrueEquals((int?) obj.answer_id) &&
                body.TrueEqualsString((string) obj.body) &&
                closed_date.TrueEquals((DateTime?) obj.closed_date) &&
                community_owned_date.TrueEquals((DateTime?) obj.community_owned_date) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                excerpt.TrueEqualsString((string) obj.excerpt) &&
                is_accepted.TrueEquals((bool?) obj.is_accepted) &&
                is_answered.TrueEquals((bool?) obj.is_answered) &&
                item_type.TrueEquals((SearchExcerptItemType?) obj.item_type) &&
                last_activity_date.TrueEquals((DateTime?) obj.last_activity_date) &&
                (last_activity_user == null && obj.last_activity_user == null ||
                 last_activity_user.EqualsDynamic(obj.last_activity_user)) &&
                locked_date.TrueEquals((DateTime?) obj.locked_date) &&
                (owner == null && obj.owner == null || owner.EqualsDynamic(obj.owner)) &&
                question_id.TrueEquals((int?) obj.question_id) &&
                score.TrueEquals((int?) obj.score) &&
                tags.TrueEqualsString((IEnumerable<string>) obj.tags) &&
                title.TrueEqualsString((string) obj.title);
        }
    }
}