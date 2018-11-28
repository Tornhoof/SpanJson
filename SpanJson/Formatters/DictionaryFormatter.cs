using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class DictionaryFormatter<TDictionary, TKey, TValue, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<TDictionary, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private static readonly Func<TDictionary> CreateFunctor = StandardResolvers.GetResolver<TSymbol, TResolver>().GetCreateFunctor<TDictionary>();
        private static readonly Func<TDictionary, int> CountFunctor = BuildCountFunctor();
        private static readonly KeyToNameDelegate KeyToNameFunctor = BuildKeyToNameFunctor();
        private static readonly NameToKeyDelegate NameToKeyFunctor = BuildNameToKeyFunctor();

        public static readonly DictionaryFormatter<TDictionary, TKey, TValue, TSymbol, TResolver> Default = new DictionaryFormatter<TDictionary, TKey, TValue, TSymbol, TResolver>();
        private static readonly IJsonFormatter<TValue, TSymbol> ElementFormatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<TValue>();
        private static readonly bool IsRecursionCandidate = RecursionCandidate<TValue>.IsRecursionCandidate;

        private static Func<TDictionary, int> BuildCountFunctor()
        {
            var paramExpression = Expression.Parameter(typeof(TDictionary), "input");
            var propertyExpression = Expression.Property(paramExpression, nameof(IDictionary<string, string>.Count));
            return Expression.Lambda<Func<TDictionary, int>>(propertyExpression, paramExpression).Compile();
        }

        private static NameToKeyDelegate BuildNameToKeyFunctor()
        {
            if (typeof(TKey) == typeof(string))
            {
                return input => Unsafe.As<string, TKey>(ref input);
            }
            if (typeof(TKey).IsEnum)
            {
                return input => Enum.p
            }
        }

        private static KeyToNameDelegate BuildKeyToNameFunctor()
        {

            if (typeof(TKey) == typeof(string))
            {
                return input => Unsafe.As<TKey, string>(ref input);
            }

            if (typeof(TKey).IsEnum)
            {
                var paramExpression = Expression.Parameter(typeof(TKey), "input");
                var switchResult = Expression.Variable(typeof(string), "switchResult");
                var cases = new List<SwitchCase>();
                foreach (var name in Enum.GetNames(typeof(TKey)))
                {
                    var valueConstant = Expression.Constant(GetFormattedValue(name));
                    var value = Enum.Parse(typeof(TKey), name);
                    var switchCase = Expression.SwitchCase(Expression.Assign(switchResult, valueConstant), Expression.Constant(value));
                    cases.Add(switchCase);
                }

                var switchExpression = Expression.Switch(typeof(void), paramExpression,
                    Expression.Throw(Expression.Constant(new InvalidOperationException())), null, cases.ToArray());
                var body = Expression.Block(new[] {switchResult}, switchExpression, switchResult);
                return Expression.Lambda<KeyToNameDelegate>(body, paramExpression).Compile();
            }

            throw new NotSupportedException();
        }

        private static string GetFormattedValue(string enumValue)
        {
            return typeof(TKey).GetMember(enumValue)?.FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? enumValue;
        }

        public TDictionary Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return default;
            }

            reader.ReadBeginObjectOrThrow();
            var result = CreateFunctor(); // using new T() is 5-10 times slower
            var count = 0;
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var key = reader.ReadEscapedName();
                var value = ElementFormatter.Deserialize(ref reader);
                //result[key] = value;
            }

            return result;
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TDictionary value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            if (IsRecursionCandidate)
            {
                writer.IncrementDepth();
            }
            var valueLength = CountFunctor(value);
            writer.WriteBeginObject();
            if (valueLength > 0)
            {
                var counter = 0;
                foreach (var kvp in value)
                {
                    writer.WriteName(KeyToNameFunctor(kvp.Key));
                    SerializeRuntimeDecisionInternal<TValue, TSymbol, TResolver>(ref writer, kvp.Value, ElementFormatter);
                    if (counter++ < valueLength - 1)
                    {
                        writer.WriteValueSeparator();
                    }
                }
            }
            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }
            writer.WriteEndObject();
        }

        private delegate string KeyToNameDelegate(TKey input);
        private delegate TKey NameToKeyDelegate(string input);
    }
}