using System;
using System.Dynamic;
using SpanJson.Resolvers;

namespace SpanJson
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter GetFormatter(Type type);
        JsonMemberInfo[] GetMemberInfos(Type type);
    }

    public interface IJsonFormatterResolver<TSymbol, in TResolver> : IJsonFormatterResolver
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        IJsonFormatter<T, TSymbol, TResolver> GetFormatter<T>();
        JsonMemberInfo[] GetMemberInfos<T>();

        JsonMemberInfo[] GetDynamicMemberInfos(IDynamicMetaObjectProvider provider);
    }
}