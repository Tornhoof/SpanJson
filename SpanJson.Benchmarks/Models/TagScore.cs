using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class TagScore : IGenericEquality<TagScore>
    {
        [ProtoMember(1)]
        public ShallowUser user { get; set; }

        [ProtoMember(2)]
        public int? score { get; set; }

        [ProtoMember(3)]
        public int? post_count { get; set; }

        public bool Equals(TagScore obj)
        {
            return
                post_count.TrueEquals(obj.post_count) &&
                score.TrueEquals(obj.score) &&
                user.TrueEquals(obj.user);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                post_count.TrueEquals((int?) obj.post_count) &&
                score.TrueEquals((int?) obj.score) &&
                (user == null && obj.user == null || user.EqualsDynamic(obj.user));
        }
    }
}