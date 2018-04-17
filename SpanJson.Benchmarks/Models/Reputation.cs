using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum VoteType : byte
    {
        up_votes = 2,
        down_votes = 3,
        spam = 12,
        accepts = 1,
        bounties_won = 9,
        bounties_offered = 8,
        suggested_edits = 16
    }

    [ProtoContract]
    public class Reputation : IGenericEquality<Reputation>
    {
        [ProtoMember(1)]
        public int? user_id { get; set; }

        [ProtoMember(2)]
        public int? post_id { get; set; }

        [ProtoMember(3)]
        public PostType? post_type { get; set; }

        [ProtoMember(4)]
        public VoteType? vote_type { get; set; }

        [ProtoMember(5)]
        public string title { get; set; }

        [ProtoMember(6)]
        public string link { get; set; }

        [ProtoMember(7)]
        public int? reputation_change { get; set; }

        [ProtoMember(8)]
        public DateTime? on_date { get; set; }

        public bool Equals(Reputation obj)
        {
            return
                link.TrueEqualsString(obj.link) &&
                on_date.TrueEquals(obj.on_date) &&
                post_id.TrueEquals(obj.post_id) &&
                post_type.TrueEquals(obj.post_type) &&
                reputation_change.TrueEquals(obj.reputation_change) &&
                title.TrueEqualsString(obj.title) &&
                user_id.TrueEquals(obj.user_id) &&
                vote_type.TrueEquals(obj.vote_type);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                link.TrueEqualsString((string) obj.link) &&
                on_date.TrueEquals((DateTime?) obj.on_date) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                post_type.TrueEquals((PostType?) obj.post_type) &&
                reputation_change.TrueEquals((int?) obj.reputation_change) &&
                title.TrueEqualsString((string) obj.title) &&
                user_id.TrueEquals((int?) obj.user_id) &&
                vote_type.TrueEquals((VoteType?) obj.vote_type);
        }
    }
}