using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson
{
    public class DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T, TSymbol, TResolver>
        where T : IDynamicMetaObjectProvider
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct
    {
        private static readonly Func<T> CreateFunctor = BuildCreateFunctor<T>(null);

        public static readonly DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver> Default =
            new DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver>();

        private static readonly TResolver Resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter = Resolver.GetFormatter<T>();


        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return default;
            }

            reader.ReadUtf16BeginObjectOrThrow();
            var result = CreateFunctor();
            var count = 0;
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16EscapedName();
                SetObjectDynamically(name, result, reader.ReadUtf16Dynamic()); // todo improve?
            }

            return result;
        }


        public void Serialize(ref JsonWriter<TSymbol> writer, T value)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            var memberInfos = Resolver.GetDynamicMemberInfos(value);
            var counter = 0;
            writer.WriteUtf16BeginObject();
            foreach (var memberInfo in memberInfos)
            {
                var child = GetObjectDynamically(memberInfo.MemberName, value);
                if (memberInfo.ExcludeNull && child == null)
                {
                    continue;
                }

                if (counter++ > 0)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Name(memberInfo.Name);
                RuntimeFormatter<TSymbol, TResolver>.Default.Serialize(ref writer, child);
            }

            writer.WriteUtf16EndObject();
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
                new[]
                {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                });
            var callsite = CallSite<Func<CallSite, object, object, object>>.Create(binder);
            callsite.Target(callsite, target, value);
        }
    }
}