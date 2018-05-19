using System;
using System.Dynamic;
using SpanJson.Resolvers;

namespace SpanJson
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter GetFormatter(Type type);
        JsonObjectDescription GetObjectDescription(Type type);
    }

    public interface IJsonFormatterResolver<TSymbol, in TResolver> : IJsonFormatterResolver
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        IJsonFormatter<T, TSymbol, TResolver> GetFormatter<T>();
        JsonObjectDescription GetObjectDescription<T>();

        JsonObjectDescription GetDynamicObjectDescription(IDynamicMetaObjectProvider provider);
    }
}