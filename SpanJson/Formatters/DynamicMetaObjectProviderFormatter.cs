using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;
using SpanJson.Formatters.Dynamic;
using SpanJson.Helpers;
using SpanJson.Resolvers;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace SpanJson.Formatters
{
    public class DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T, TSymbol>
        where T : IDynamicMetaObjectProvider
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct
    {
        private static readonly Func<T> CreateFunctor = StandardResolvers.GetResolver<TSymbol, TResolver>().GetCreateFunctor<T>();

        public static readonly DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver> Default =
            new DynamicMetaObjectProviderFormatter<T, TSymbol, TResolver>();

        private static readonly IJsonFormatterResolver<TSymbol, TResolver> Resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
        private static readonly Dictionary<string, DeserializeDelegate> KnownMembersDictionary = BuildKnownMembers();
        private static readonly ConcurrentDictionary<string, Func<T, object>> GetMemberCache = new ConcurrentDictionary<string, Func<T, object>>();


        private static readonly ConcurrentDictionary<string, Action<T, object>> SetMemberCache = new ConcurrentDictionary<string, Action<T, object>>();

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

        public void Serialize(ref JsonWriter<TSymbol> writer, T value)
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
                var cMaxLength = Encoding.UTF8.GetMaxCharCount(bValue.Symbols.Length);
                char[] buffer = null;
                try
                {
                    buffer = ArrayPool<char>.Shared.Rent(cMaxLength); // can't use stackalloc here
                    var written = Encoding.UTF8.GetChars(bValue.Symbols, buffer);
                    writer.WriteUtf16Verbatim(buffer.AsSpan(0, written));
                }
                finally
                {
                    if (buffer != null)
                    {
                        ArrayPool<char>.Shared.Return(buffer);
                    }
                }

            }
            else if (value is ISpanJsonDynamicValue<char> cValue)
            {
                var bMaxLength = Encoding.UTF8.GetMaxCharCount(cValue.Symbols.Length);
                byte[] buffer = null;
                try
                {
                    buffer = ArrayPool<byte>.Shared.Rent(bMaxLength); // can't use stackalloc here
                    var written = Encoding.UTF8.GetBytes(cValue.Symbols, buffer);
                    writer.WriteUtf8Verbatim(buffer.AsSpan(0, written));
                }
                finally
                {
                    if (buffer != null)
                    {
                        ArrayPool<byte>.Shared.Return(buffer);
                    }
                }
            }
            else if (value is ISpanJsonDynamicArray dynamicArray)
            {
                writer.IncrementDepth();
                EnumerableFormatter<IEnumerable<object>, object, TSymbol, TResolver>.Default.Serialize(ref writer, dynamicArray);
                writer.DecrementDepth();
            }
            else
            {
                var memberInfos = Resolver.GetDynamicObjectDescription(value);
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

                    writer.IncrementDepth();
                    writer.WriteName(memberInfo.Name);
                    RuntimeFormatter<TSymbol, TResolver>.Default.Serialize(ref writer, child);
                    writer.DecrementDepth();
                }

                // Some dlr objects also have properties which are defined on the custom type, we also need to serialize/deserialize them
                var definedMembers = Resolver.GetObjectDescription<T>();
                foreach (var memberInfo in definedMembers)
                {
                    var getter = GetOrAddGetDefinedMember(memberInfo.MemberName);
                    var child = getter(value);
                    if (memberInfo.ExcludeNull && child == null)
                    {
                        continue;
                    }

                    if (counter++ > 0)
                    {
                        writer.WriteValueSeparator();
                    }

                    writer.IncrementDepth();
                    writer.WriteName(memberInfo.Name);
                    RuntimeFormatter<TSymbol, TResolver>.Default.Serialize(ref writer, child);
                    writer.DecrementDepth();
                }

                writer.WriteEndObject();
            }
        }

        private static Dictionary<string, DeserializeDelegate> BuildKnownMembers()
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var memberInfos = resolver.GetObjectDescription<T>().ToList();
            var inputParameter = Expression.Parameter(typeof(T), "input");
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");
            var result = new Dictionary<string, DeserializeDelegate>(StringComparer.InvariantCulture);
            // can't deserialize abstract or interface
            foreach (var memberInfo in memberInfos)
            {
                if (!memberInfo.CanWrite)
                {
                    var skipNextMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextSegment));
                    var skipExpression = Expression
                        .Lambda<DeserializeDelegate>(Expression.Call(readerParameter, skipNextMethodInfo), inputParameter, readerParameter).Compile();
                    result.Add(memberInfo.Name, skipExpression);
                    continue;
                }

                // can't deserialize abstract and only support interfaces based on IEnumerable<T> (this includes, IList, IReadOnlyList, IDictionary et al.)
                if (memberInfo.MemberType.IsAbstract && !memberInfo.MemberType.TryGetTypeOfGenericInterface(typeof(IEnumerable<>), out _))
                {
                    var throwExpression = Expression.Lambda<DeserializeDelegate>(Expression.Block(
                            Expression.Throw(Expression.Constant(new NotSupportedException($"{typeof(T).Name} contains abstract members."))),
                            Expression.Default(typeof(T))),
                        inputParameter, readerParameter).Compile();
                    result.Add(memberInfo.Name, throwExpression);
                    continue;
                }

                var formatterType = resolver.GetFormatter(memberInfo).GetType();
                var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                var assignExpression = Expression.Assign(Expression.PropertyOrField(inputParameter, memberInfo.MemberName),
                    Expression.Call(Expression.Field(null, fieldInfo),
                        FindPublicInstanceMethod(formatterType, "Deserialize", readerParameter.Type.MakeByRefType()), readerParameter));
                var lambda = Expression.Lambda<DeserializeDelegate>(assignExpression, inputParameter, readerParameter).Compile();
                result.Add(memberInfo.Name, lambda);
            }

            return result;
        }

        private static Func<T, object> GetOrAddGetDefinedMember(string memberName)
        {
            return GetMemberCache.GetOrAdd(memberName, s =>
            {
                var paramExpression = Expression.Parameter(typeof(T), "input");
                return Expression.Lambda<Func<T, object>>(Expression.Convert(Expression.PropertyOrField(paramExpression, s), typeof(object)), paramExpression)
                    .Compile();
            });
        }


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

        protected delegate void DeserializeDelegate(T input, ref JsonReader<TSymbol> reader);
    }
}