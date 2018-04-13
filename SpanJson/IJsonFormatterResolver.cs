using System;
using SpanJson.Resolvers;

namespace SpanJson
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter GetFormatter(Type type);
        JsonMemberInfo[] GetMemberInfos(Type type);
    }

    public interface IJsonFormatterResolver<in TResolver> : IJsonFormatterResolver
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        IJsonFormatter<T, TResolver> GetFormatter<T>();
        JsonMemberInfo[] GetMemberInfos<T>();
    }
}