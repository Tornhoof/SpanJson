using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class TwoDimensionalArrayFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T[,], TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly TwoDimensionalArrayFormatter<T, TSymbol, TResolver> Default = new TwoDimensionalArrayFormatter<T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol> ElementFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        private static readonly IJsonFormatter<T[], TSymbol> OneDimensionalFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T[]>();

        public void Serialize(ref JsonWriter<TSymbol> writer, T[,] value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var firstLength = value.GetLength(0);
            var secondLength = value.GetLength(1);
            writer.WriteBeginArray();
            if (firstLength > 0)
            {
                writer.WriteBeginArray();
                if (secondLength > 0)
                {
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0, 0], ElementFormatter, nextNestingLimit);
                    for (var k = 1; k < secondLength; k++)
                    {
                        writer.WriteValueSeparator();
                        SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0, k], ElementFormatter, nextNestingLimit);
                    }
                }

                writer.WriteEndArray();
                for (var i = 1; i < firstLength; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteBeginArray();
                    if (secondLength > 0)
                    {
                        SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i, 0], ElementFormatter, nextNestingLimit);
                        for (var k = 1; k < secondLength; k++)
                        {
                            writer.WriteValueSeparator();
                            SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i, k], ElementFormatter, nextNestingLimit);
                        }
                    }

                    writer.WriteEndArray();
                }
            }

            writer.WriteEndArray();
        }

        public T[,] Deserialize(ref JsonReader<TSymbol> reader)
        {
            T[] temp = null;
            T[,] result;
            try
            {
                reader.ReadBeginArrayOrThrow();
                var count = 0;
                while (!reader.TryReadIsEndArrayOrValueSeparator(ref count)) // count is already preincremented, as it counts the separators
                {

                }

                result = count == 0 ? Array.Empty<T>() : FormatterUtils.CopyArray(temp, count);
            }
            finally
            {
                if (temp != null)
                {
                    ArrayPool<T>.Shared.Return(temp);
                }
            }

            return result;
        }
    }
}
