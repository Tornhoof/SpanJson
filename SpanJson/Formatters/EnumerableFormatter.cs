﻿using System;
using System.Collections.Generic;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class EnumerableFormatter<TEnumerable, T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<TEnumerable, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TEnumerable : class, IEnumerable<T>

    {
        private static readonly Func<T[], TEnumerable> Converter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetEnumerableConvertFunctor<T[], TEnumerable>();

        public static readonly EnumerableFormatter<TEnumerable, T, TSymbol, TResolver> Default = new EnumerableFormatter<TEnumerable, T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public TEnumerable Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            var array = ArrayFormatter<T, TSymbol, TResolver>.Default.Deserialize(ref reader);
            return Converter(array);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TEnumerable value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            IEnumerator<T> enumerator = null;
            try
            {
                enumerator = value.GetEnumerator();
                writer.WriteBeginArray();
                if (enumerator.MoveNext())
                {
                    // first one, so we can write the separator prior to every following one
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, enumerator.Current, ElementFormatter, nextNestingLimit);
                    // write all the other ones
                    while (enumerator.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, enumerator.Current, ElementFormatter, nextNestingLimit);
                    }
                }

                writer.WriteEndArray();
            }
            finally
            {
                enumerator?.Dispose();
            }
        }
    }
}