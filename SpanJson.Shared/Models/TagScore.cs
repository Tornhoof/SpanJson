namespace SpanJson.Shared.Models
{
    public class TagScore : IGenericEquality<TagScore>
    {
        public ShallowUser user { get; set; }


        public int? score { get; set; }


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