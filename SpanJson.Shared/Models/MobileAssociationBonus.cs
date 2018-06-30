namespace SpanJson.Shared.Models
{
    public sealed class MobileAssociationBonus : IMobileFeedBase<MobileAssociationBonus>
    {
        public string site { get; set; }


        public int? amount { get; set; }


        public int? group_id { get; set; }


        public long? added_date { get; set; }

        public bool Equals(MobileAssociationBonus obj)
        {
            return
                added_date == obj.added_date &&
                amount == obj.amount &&
                group_id == obj.group_id &&
                site == obj.site;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                amount == (int?) obj.amount &&
                group_id == (int?) obj.group_id &&
                site == (string) obj.site;
        }
    }
}