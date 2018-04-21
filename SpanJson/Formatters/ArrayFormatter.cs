using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ArrayFormatter
    {


        protected static T[] Deserialize<T, TResolver>(ref JsonReader reader, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            T[] temp = null;
            T[] result;
            try
            {
                temp = ArrayPool<T>.Shared.Rent(4);
                reader.ReadBeginArrayOrThrow();
                var count = 0;
                while (!reader.TryReadIsEndArrayOrValueSeparator(ref count)) // count is already preincremented, as it counts the separators
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

        protected static void Serialize<T, TResolver>(ref JsonWriter writer, T[] value, IJsonFormatter<T, TResolver> formatter)
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var valueLength = value.Length;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                formatter.Serialize(ref writer, value[0]);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    formatter.Serialize(ref writer, value[i]);
                }
            }

            writer.WriteEndArray();
        }
    }

    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ArrayFormatter<T, TResolver> : ArrayFormatter, IJsonFormatter<T[], TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        public static readonly ArrayFormatter<T, TResolver> Default = new ArrayFormatter<T, TResolver>();

        private static readonly IJsonFormatter<T, TResolver> DefaultFormatter =
            StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

        public T[] Deserialize(ref JsonReader reader)
        {
            return Deserialize(ref reader, DefaultFormatter);
        }

        public void Serialize(ref JsonWriter writer, T[] value)
        {
            Serialize(ref writer, value, DefaultFormatter);
        }
    }
}