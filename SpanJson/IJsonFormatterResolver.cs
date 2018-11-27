using System;
using System.Dynamic;
using SpanJson.Resolvers;

namespace SpanJson
{
    public interface IJsonFormatterResolver
    {
        IJsonFormatter GetFormatter(Type type);
        IJsonFormatter GetFormatter(JsonMemberInfo info, Type overrideMemberType = null);
        JsonObjectDescription GetDynamicObjectDescription(IDynamicMetaObjectProvider provider);
    }

    public interface IJsonFormatterResolver<TSymbol, in TResolver> : IJsonFormatterResolver
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        IJsonFormatter<T, TSymbol> GetFormatter<T>();
        JsonObjectDescription GetObjectDescription<T>();

        Func<T> GetCreateFunctor<T>();
        Func<T, TConverted> GetEnumerableConvertFunctor<T, TConverted>();
    }
}