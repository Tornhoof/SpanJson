namespace SpanJson.Shared.Models
{
    public class Error : IGenericEquality<Error>
    {
        public int? error_id { get; set; }


        public string error_name { get; set; }


        public string description { get; set; }

        public bool Equals(Error obj)
        {
            return
                error_id.TrueEquals(obj.error_id) &&
                error_name.TrueEqualsString(obj.error_name) &&
                description.TrueEqualsString(obj.description);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                error_id.TrueEquals((int?) obj.error_id) &&
                error_name.TrueEqualsString((string) obj.error_name) &&
                description.TrueEqualsString((string) obj.description);
        }
    }
}