using System;

namespace SpanJson
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter<T, TResolver> GetFormatter<T, TResolver>() where TResolver : IJsonFormatterResolver, new();

        IJsonFormatter GetFormatter(Type type);
    }

    public interface IJsonFormatterResolver<in TResolver> : IJsonFormatterResolver
        where TResolver : IJsonFormatterResolver, new()
    {
        IJsonFormatter<T, TResolver> GetFormatter<T>();

    }
}