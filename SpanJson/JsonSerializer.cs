namespace SpanJson
{
    public static class JsonSerializer
    {
        public static string Serialize<T>(T input)
        {
            return TypedSerializer<T>.Serialize(input);
        }
    }
}
