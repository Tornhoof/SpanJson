using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;
using SpanJson.Formatters.Dynamic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
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
        private static readonly Dictionary<string, DeserializeDelegate> KnownMembersDictionary = BuildKnownMembers();

        private static Dictionary<string, DeserializeDelegate> BuildKnownMembers()
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var memberInfos = resolver.GetMemberInfos<T>().ToList();
            var inputParameter = Expression.Parameter(typeof(T), "input");
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");
            var result = new Dictionary<string, DeserializeDelegate>(StringComparer.InvariantCulture);
            // can't deserialize abstract or interface
            foreach (var jsonMemberInfo in memberInfos)
            {
                if (!jsonMemberInfo.CanWrite)
                {
                    var skipNextMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextSegment));
                    var skipExpression = Expression.Lambda<DeserializeDelegate>(Expression.Call(readerParameter, skipNextMethodInfo), inputParameter, readerParameter).Compile();
                    result.Add(jsonMemberInfo.Name, skipExpression);
                }
                else if (jsonMemberInfo.MemberType.IsAbstract || jsonMemberInfo.MemberType.IsInterface)
                {
                    var throwExpression = Expression.Lambda<DeserializeDelegate>(Expression.Block(
                            Expression.Throw(Expression.Constant(new NotSupportedException($"{typeof(T).Name} contains abstract or interface members."))),
                            Expression.Default(typeof(T))),
                        inputParameter, readerParameter).Compile();
                    result.Add(jsonMemberInfo.Name, throwExpression);
                }
                else
                {
                    var formatter = ((IJsonFormatterResolver) resolver).GetFormatter(jsonMemberInfo.MemberType);
                    var assignExpression = Expression.Assign(Expression.PropertyOrField(inputParameter, jsonMemberInfo.MemberName),
                        Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"), readerParameter));
                    var lambda = Expression.Lambda<DeserializeDelegate>(assignExpression, inputParameter, readerParameter).Compile();
                    result.Add(jsonMemberInfo.Name, lambda);
                }
            }

            return result;
        }

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
                if (KnownMembersDictionary.TryGetValue(name, out var action))
                {
                    action(result, ref reader); // if we have known members we try to assign them directly without dynamic
                }
                else
                {
                    var setter = GetOrAddSetMember(name);
                    setter(result, reader.ReadDynamic());
                }
            }

            return result;
        }

        private static readonly ConcurrentDictionary<string, Func<T,object>> GetMemberCache = new ConcurrentDictionary<string, Func<T, object>>();
        private static readonly ConcurrentDictionary<string, Action<T, object>> SetMemberCache = new ConcurrentDictionary<string, Action<T, object>>();


        private static Func<T, object> GetOrAddGetMember(string memberName)
        {
            return GetMemberCache.GetOrAdd(memberName, s =>
            {
                var binder = (GetMemberBinder) Binder.GetMember(CSharpBinderFlags.None, s, typeof(T),
                    new[] {CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)});
                var callSite = CallSite<Func<CallSite, object, object>>.Create(binder);
                return target => callSite.Target(callSite, target);
            });
        }

        private static Action<T, object> GetOrAddSetMember(string memberName)
        {
            return SetMemberCache.GetOrAdd(memberName, s =>
            {
                var binder = Binder.SetMember(CSharpBinderFlags.None, memberName, null,
                    new[]
                    {
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                    });
                var callsite = CallSite<Func<CallSite, object, object, object>>.Create(binder);
                return (target, value) => callsite.Target(callsite, target, value);
            });
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            // if we serialize our dynamic value again we simply write the symbols directly if it is the same type
            if (value is ISpanJsonDynamicValue<TSymbol> dynamicValue)
            {
                writer.WriteVerbatim(dynamicValue.Symbols);
            }
            else if (value is ISpanJsonDynamicValue<byte> bValue)
            {
                var chars = Encoding.UTF8.GetChars(bValue.Symbols);
                writer.WriteUtf16Verbatim(chars);
            }
            else if (value is ISpanJsonDynamicValue<char> cValue)
            {
                var bytes = Encoding.UTF8.GetBytes(cValue.Symbols);
                writer.WriteUtf8Verbatim(bytes);
            }
            else
            {
                var memberInfos = Resolver.GetDynamicMemberInfos(value);
                var counter = 0;
                writer.WriteBeginObject();
                foreach (var memberInfo in memberInfos)
                {
                    var getter = GetOrAddGetMember(memberInfo.MemberName);
                    var child = getter(value);
                    if (memberInfo.ExcludeNull && child == null)
                    {
                        continue;
                    }

                    if (counter++ > 0)
                    {
                        writer.WriteValueSeparator();
                    }

                    var nextNestingLimit = RecursionCandidate.LookupRecursionCandidate(memberInfo.MemberType) ? nestingLimit + 1 : nestingLimit;
                    writer.WriteName(memberInfo.Name);
                    RuntimeFormatter<TSymbol, TResolver>.Default.Serialize(ref writer, child, nextNestingLimit);
                }

                writer.WriteEndObject();
            }
        }
        protected delegate void DeserializeDelegate(T input, ref JsonReader<TSymbol> reader);
    }
}