namespace SpanJson.Shared.Models
{
    public interface IMobileFeedBase<T> : IGenericEquality<T>
    {
        int? group_id { get; set; }
        long? added_date { get; set; }
    }
}