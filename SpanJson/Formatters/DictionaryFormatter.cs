using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// The integer key part can be further optimized by adding some kind of WriteIntegerAsString and ReadIntegerFromString methods to the JsonWriter to get around string allocations
    /// </summary>
    public sealed class DictionaryFormatter<TDictionary, TWritableDictionary, TKey, TValue, TSymbol, TResolver> : BaseFormatter,
        IJsonFormatter<TDictionary, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TDictionary : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private static readonly Func<TWritableDictionary> CreateFunctor =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetCreateFunctor<TWritableDictionary>();

        private static readonly Func<TDictionary, int> CountFunctor = BuildCountFunctor();
        private static readonly WriteKeyDelegate WriteKeyFunctor = BuildKeyToNameDelegate();
        private static readonly ReadKeyDelegate ReadKeyFunctor = BuildNameToKeyDelegate();
        private static readonly AssignKvpDelegate AssignKvpFunctor = BuildAssignKvpFunctor();

        public static readonly DictionaryFormatter<TDictionary, TWritableDictionary, TKey, TValue, TSymbol, TResolver> Default =
            new DictionaryFormatter<TDictionary, TWritableDictionary, TKey, TValue, TSymbol, TResolver>();

        private static readonly IJsonFormatter<TValue, TSymbol> ElementFormatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<TValue>();

        private static readonly Func<TWritableDictionary, TDictionary> Converter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetEnumerableConvertFunctor<TWritableDictionary, TDictionary>();

        private static readonly bool IsRecursionCandidate = RecursionCandidate<TValue>.IsRecursionCandidate;

        private static Func<TDictionary, int> BuildCountFunctor()
        {
            var paramExpression = Expression.Parameter(typeof(TDictionary), "input");
            var propertyInfo = paramExpression.Type.GetPublicProperties().First(x => x.Name == nameof(ICollection.Count));
            var propertyExpression = Expression.Property(paramExpression, propertyInfo);
            return Expression.Lambda<Func<TDictionary, int>>(propertyExpression, paramExpression).Compile();
        }

        private static AssignKvpDelegate BuildAssignKvpFunctor()
        {
            var dictionaryParameterExpression = Expression.Parameter(typeof(TWritableDictionary), "dictionary");
            var keyParameterExpression = Expression.Parameter(typeof(TKey), "key");
            var valueParameterExpression = Expression.Parameter(typeof(TValue), "value");

            var indexerProperty = dictionaryParameterExpression.Type.GetProperty("Item", valueParameterExpression.Type, new[] {keyParameterExpression.Type});

            var lambda = Expression.Lambda<AssignKvpDelegate>(
                Expression.Assign(Expression.Property(dictionaryParameterExpression, indexerProperty, keyParameterExpression), valueParameterExpression),
                dictionaryParameterExpression, keyParameterExpression, valueParameterExpression);
            return lambda.Compile();
        }

        private static ReadKeyDelegate BuildNameToKeyDelegate()
        {
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");

            if (typeof(TKey) == typeof(string))
            {
                return input => Unsafe.As<string, TKey>(ref input);
            }

            if (typeof(TKey).IsEnum)
            {
                var paramExpression = Expression.Parameter(typeof(string), "input");
                var switchResult = Expression.Variable(typeof(TKey), "switchResult");
                var cases = new List<SwitchCase>();
                foreach (var name in Enum.GetNames(typeof(TKey)))
                {
                    var switchValue = Expression.Constant(GetFormattedValue(name));
                    var assignValue = Expression.Constant(Enum.Parse(typeof(TKey), name));
                    var switchCase = Expression.SwitchCase(Expression.Assign(switchResult, assignValue), switchValue);
                    cases.Add(switchCase);
                }

                var switchExpression = Expression.Switch(typeof(void), paramExpression,
                    Expression.Throw(Expression.Constant(new InvalidOperationException())), null, cases.ToArray());
                var body = Expression.Block(new[] {switchResult}, switchExpression, switchResult);
                return Expression.Lambda<ReadKeyDelegate>(body, paramExpression).Compile();
            }

            throw new NotSupportedException();
        }

        private static WriteKeyDelegate BuildKeyToNameDelegate()
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter<TSymbol>).MakeByRefType(), "writer");
            var inputParameter = Expression.Parameter(typeof(TKey), "input");
            var expressions = new List<Expression>();
            if (typeof(TKey) == typeof(string))
            {
                var propertyNameWriterMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteString), typeof(string));
                expressions.Add(Expression.Call(writerParameter, propertyNameWriterMethodInfo, inputParameter));
            }
            else if (typeof(TKey).IsEnum)
            {
                var propertyNameWriterMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteString), typeof(string));
                var cases = new List<SwitchCase>();
                foreach (var name in Enum.GetNames(typeof(TKey)))
                {
                    var assignValue = Expression.Constant(GetFormattedValue(name));
                    var switchValue = Expression.Constant(Enum.Parse(typeof(TKey), name));
                    var switchCase = Expression.SwitchCase(Expression.Call(writerParameter, propertyNameWriterMethodInfo, assignValue), switchValue);
                    cases.Add(switchCase);
                }

                var switchExpression = Expression.Switch(typeof(void), inputParameter,
                    Expression.Throw(Expression.Constant(new InvalidOperationException())), null, cases.ToArray());
                expressions.Add(switchExpression);
            }
            else if (typeof(TKey).IsInteger())
            {
                var tKeyType = typeof(TKey);
                var doubleQuoteWriterMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteDoubleQuote));
                expressions.Add(Expression.Call(writerParameter, doubleQuoteWriterMethodInfo));
                var integerWriterMethodInfo = FindPublicInstanceMethod(writerParameter.Type, $"Write{tKeyType.Name}", tKeyType);
                expressions.Add(Expression.Call(writerParameter, integerWriterMethodInfo, inputParameter));
                expressions.Add(Expression.Call(writerParameter, doubleQuoteWriterMethodInfo));
            }
            else
            {
                throw new NotSupportedException();
            }

            var colonWriterMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteColon));
            expressions.Add(Expression.Call(writerParameter, colonWriterMethodInfo));
            var body = Expression.Block(expressions);
            return Expression.Lambda<WriteKeyDelegate>(body, writerParameter, inputParameter).Compile();
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
                var key = ReadKeyFunctor(ref reader);
                var value = ElementFormatter.Deserialize(ref reader);
                AssignKvpFunctor(result, key, value); // No shared interface for IReadOnlyDictionary and IDictionary to set the value via indexer (to make sure that for duplicated keys, we use the last one)
            }

            return Converter(result);
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

            var valueLength = CountFunctor(value); // IReadOnlyDictionary and IDictionary don't share the same interface with .Count, use expression trees to optimize it
            writer.WriteBeginObject();
            if (valueLength > 0)
            {
                var counter = 0;
                foreach (var kvp in value)
                {
                    WriteKeyFunctor(ref writer, kvp.Key);
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

        private delegate string WriteKeyDelegate(ref JsonWriter<TSymbol> writer, TKey input);

        private delegate TKey ReadKeyDelegate(ref JsonReader<TSymbol> reader);

        private delegate void AssignKvpDelegate(TWritableDictionary dictionary, TKey key, TValue value);
    }
}