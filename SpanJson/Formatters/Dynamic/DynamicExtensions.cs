namespace SpanJson.Formatters.Dynamic
{
    static class DynamicExtensions
    {
        public static string ToJsonValue(this object input)
        {
            if (input is ISpanJsonDynamic dyn)
            {
                return dyn.ToJsonValue();
            }

            return input?.ToString();
        }
    }
}
