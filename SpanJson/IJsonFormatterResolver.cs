using System;

namespace SpanJson
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter GetFormatter(Type type);
    }

    public interface IJsonFormatterResolver<in TResolver> : IJsonFormatterResolver
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        IJsonFormatter<T, TResolver> GetFormatter<T>();
    }
}