using System;

namespace SpanJson
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter<T> GetFormatter<T>();

        IJsonFormatter GetFormatter(Type type);
    }
}