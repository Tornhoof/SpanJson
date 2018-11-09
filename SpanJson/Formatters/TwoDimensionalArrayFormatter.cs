using System.Collections.Generic;
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

        public T[,] Deserialize(ref JsonReader<TSymbol> reader)
        {
            var values = new List<T[]>(4);
            reader.ReadBeginArrayOrThrow();
            var count = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref count)) // count is already preincremented, as it counts the separators
            {
                values.Add(ArrayFormatter<T, TSymbol, TResolver>.Default.Deserialize(ref reader));
            }

            if (values.Count == 0)
            {
                return new T[0, 0];
            }

            var length = values[0].Length;
            if (!values.TrueForAll(a => a.Length == length))
            {
                throw new JsonParserException(JsonParserException.ParserError.InvalidArrayFormat, reader.Position);
            }

            var result = new T[values.Count, length];
            for (var i = 0; i < values.Count; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    result[i, j] = values[i][j];
                }
            }

            return result;
        }

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
    }
}