using System;

namespace SpanJson.Shared.Models
{
    public class Reputation : IGenericEquality<Reputation>
    {
        public int? user_id { get; set; }


        public int? post_id { get; set; }


        public PostType? post_type { get; set; }


        public VoteType? vote_type { get; set; }


        public string title { get; set; }


        public string link { get; set; }


        public int? reputation_change { get; set; }


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