using System;
using System.Collections.Generic;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ListFormatter : BaseFormatter
    {
        protected static TList Deserialize<TList, T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader, IJsonFormatter<T, TSymbol, TResolver> formatter, Func<TList> createFunctor)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            reader.ReadUtf16BeginArrayOrThrow();
            var list = createFunctor();
            var count = 0;
            while (!reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader));
            }

            return list;
        }

        protected static void Serialize<TList, T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, TList value,
            IJsonFormatter<T, TSymbol, TResolver> formatter) where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            var valueLength = value.Count;
            writer.WriteUtf16BeginArray();
            if (valueLength > 0)
            {
                formatter.Serialize(ref writer, value[0]);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteUtf16ValueSeparator();
                    formatter.Serialize(ref writer, value[i]);
                }
            }

            writer.WriteUtf16EndArray();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ListFormatter<TList, T, TSymbol, TResolver> : ListFormatter, IJsonFormatter<TList, TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>

    {
        public static readonly ListFormatter<TList, T, TSymbol, TResolver> Default = new ListFormatter<TList, T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

        private static readonly Func<TList> CreateFunctor = BuildCreateFunctor<TList>(typeof(List<T>));

        public TList Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize(ref reader, DefaultFormatter, CreateFunctor);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, TList value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}