using System;
using System.Collections.Generic;
using System.Text;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public class EnumFormatter<T> : IJsonFormatter<T> where T : struct
    {
        private static readonly IReadOnlyDictionary<T, string> EnumDictionary = BuildEnumDictionary();

        private static Dictionary<T, string> BuildEnumDictionary()
        {
            var result = new Dictionary<T, string>();
            var values = Enum.GetValues(typeof(T));
            foreach (T value in values)
            {
                result.Add(value, value.ToString());
            }

            return result;
        }

        public static readonly EnumFormatter<T> Default = new EnumFormatter<T>();

        public T DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (EnumDictionary.TryGetValue(value, out var stringValue))
            {
                writer.WriteString(stringValue);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
