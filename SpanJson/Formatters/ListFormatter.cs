using System;
using System.Collections.Generic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ListFormatter<TList, T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<TList, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>

    {
        private static readonly Func<TList> CreateFunctor = StandardResolvers.GetResolver<TSymbol, TResolver>().GetCreateFunctor<TList>();
        public static readonly ListFormatter<TList, T, TSymbol, TResolver> Default = new ListFormatter<TList, T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public TList Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginArrayOrThrow();
            var list = CreateFunctor();
            var count = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref count))
            {
                list.Add(ElementFormatter.Deserialize(ref reader));
            }

            return list;
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TList value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var valueLength = value.Count;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0], ElementFormatter, nextNestingLimit);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter, nextNestingLimit);
                }
            }

            writer.WriteEndArray();
        }
    }
}