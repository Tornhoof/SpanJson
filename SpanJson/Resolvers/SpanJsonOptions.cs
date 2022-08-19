namespace SpanJson.Resolvers
{
    public class SpanJsonOptions
    {
        public NamingConventions NamingConvention { get; set; }
        public NullOptions NullOption { get; set; }
        public EnumOptions EnumOption { get; set; }
        public ByteArrayOptions ByteArrayOption { get; set; }
    }
}