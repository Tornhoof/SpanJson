using System;
using System.Buffers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ArrayFormatter
    {
        protected static T[] Deserialize<T, TSymbol, TResolver>(ref JsonReader<TSymbol> reader, IJsonFormatter<T, TSymbol, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            T[] temp = null;
            T[] result;
            try
            {
                temp = ArrayPool<T>.Shared.Rent(4);
                reader.ReadUtf16BeginArrayOrThrow();
                var count = 0;
                while (!reader.TryReadUtf16IsEndArrayOrValueSeparator(ref count)) // count is already preincremented, as it counts the separators
                {
                    if (count == temp.Length)
                    {
                        Grow(ref temp);
                    }

                    temp[count - 1] = formatter.Deserialize(ref reader);
                }

                if (count == 0)
                {
                    result = Array.Empty<T>();
                }
                else
                {
                    result = new T[count];
                    Array.Copy(temp, result, count);
                }
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

        private static void Grow<T>(ref T[] array)
        {
            var backup = array;
            array = ArrayPool<T>.Shared.Rent(backup.Length * 2);
            backup.CopyTo(array, 0);
            ArrayPool<T>.Shared.Return(backup);
        }

        protected static void Serialize<T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer, T[] value, IJsonFormatter<T, TSymbol, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            var valueLength = value.Length;
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
    public sealed class ArrayFormatter<T, TSymbol, TResolver> : ArrayFormatter, IJsonFormatter<T[],TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly ArrayFormatter<T, TSymbol, TResolver> Default = new ArrayFormatter<T, TSymbol, TResolver>();

        private static readonly IJsonFormatter<T, TSymbol, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();
        //private static readonly Func<int, T[]> C

        public T[] Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserialize(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T[] value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}