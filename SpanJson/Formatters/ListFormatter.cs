using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ListFormatter : BaseFormatter
    {
        protected static TList Deserialize<TList, T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader, IJsonFormatter<T, TSymbol, TResolver> formatter,
            Func<TList> createFunctor)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            reader.ReadBeginArrayOrThrow();
            var list = createFunctor();
            var count = 0;
            while (!reader.TryReadIsEndArrayOrValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader));
            }

            return list;
        }

        protected static void Serialize<TList, T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, TList value,
            IJsonFormatter<T, TSymbol, TResolver> formatter, int nestingLimit) where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
            where TSymbol : struct
            where TList : class, IList<T>
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
                SerializeInternal(ref writer, formatter, value[0], nextNestingLimit);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeInternal(ref writer, formatter, value[i], nextNestingLimit);
                }
            }

            writer.WriteEndArray();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ListFormatter<TList, T, TSymbol, TResolver> : ListFormatter, IJsonFormatter<TList, TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>

    {
        private static readonly Func<TList> CreateFunctor = BuildCreateFunctor<TList>(typeof(List<T>));
        public static readonly ListFormatter<TList, T, TSymbol, TResolver> Default = new ListFormatter<TList, T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        public TList Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize(ref reader, DefaultFormatter, CreateFunctor);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TList value, int nestingLimit)
        {

            Serialize(ref writer, value, DefaultFormatter, nestingLimit);
        }
    }
}