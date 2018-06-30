namespace SpanJson.Shared.Models
{
    public class Privilege : IGenericEquality<Privilege>
    {
        public string short_description { get; set; }


        public string description { get; set; }


        public int? reputation { get; set; }

        public bool Equals(Privilege obj)
        {
            return
                description.TrueEqualsString(obj.description) &&
                reputation.TrueEquals(obj.reputation) &&
                short_description.TrueEqualsString(obj.short_description);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                description.TrueEqualsString((string) obj.description) &&
                reputation.TrueEquals((int?) obj.reputation) &&
                short_description.TrueEqualsString((string) obj.short_description);
        }
    }
}