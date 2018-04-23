using Microsoft.CSharp.RuntimeBinder;
using SpanJson.Formatters;
using SpanJson.Resolvers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace SpanJson
{
    public class DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T, TSymbol, TResolver> where T : IDynamicMetaObjectProvider
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct  
    {
        private static readonly Func<T> CreateFunctor = BuildCreateFunctor<T>(null);

        public static readonly DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver> Default =
            new DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver>();

        private static readonly TResolver Resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter = Resolver.GetFormatter<T>();


        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return default;
            }

            reader.ReadBeginObjectOrThrow();
            var result = CreateFunctor();
            var count = 0;
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadEscapedName();
                SetObjectDynamically(name, result, reader.ReadDynamic()); // todo improve?
            }

            return result;
        }


        public void Serialize(ref JsonWriter<TSymbol> writer, T value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var memberInfos = Resolver.GetDynamicMemberInfos(value);
            int counter = 0;
            writer.WriteBeginObject();
            foreach (var memberInfo in memberInfos)
            {
                var child = GetObjectDynamically(memberInfo.MemberName, value);
                if (memberInfo.ExcludeNull && child == null)
                {
                    continue;
                }

                if (counter++ > 0)
                {
                    writer.WriteValueSeparator();
                }
                writer.WriteName(memberInfo.Name);
                RuntimeFormatter<TSymbol, TResolver>.Default.Serialize(ref writer, child);
            }
            writer.WriteEndObject();
        }

        private static object GetObjectDynamically(string memberName, T target)
        {
            var binder = Binder.GetMember(CSharpBinderFlags.None, memberName, null,
                new[] {CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)});
            var callsite = CallSite<Func<CallSite, object, object>>.Create(binder);
            return callsite.Target(callsite, target);
        }


        private static void SetObjectDynamically(string memberName, T target, object value)
        {
            var binder = Binder.SetMember(CSharpBinderFlags.None, memberName, null,
                new[]{
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                });
            var callsite = CallSite<Func<CallSite, object, object, object>>.Create(binder);
            callsite.Target(callsite, target, value);
        }
    }
}