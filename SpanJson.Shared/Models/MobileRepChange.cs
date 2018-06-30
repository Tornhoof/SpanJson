namespace SpanJson.Shared.Models
{
    public sealed class MobileRepChange : IMobileFeedBase<MobileRepChange>
    {
        public string site { get; set; }


        public string title { get; set; }


        public string link { get; set; }


        public int? rep_change { get; set; }


        public int? group_id { get; set; }


        public long? added_date { get; set; }

        public bool Equals(MobileRepChange obj)
        {
            return
                added_date == obj.added_date &&
                group_id == obj.group_id &&
                link == obj.link &&
                rep_change == obj.rep_change &&
                site == obj.site &&
                title == obj.title;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                group_id == (int?) obj.group_id &&
                link == (string) obj.link &&
                rep_change == (int?) obj.rep_change &&
                site == (string) obj.site &&
                title == (string) obj.title;
        }
    }
}