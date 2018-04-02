namespace SpanJson.Benchmarks
{
    public interface IGenericEquality<in T>
    {
        bool Equals(T obj);
        bool EqualsDynamic(dynamic obj);
    }
}