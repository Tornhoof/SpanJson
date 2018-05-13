using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SpanJson.Resolvers
{
    public class JsonObjectDescription : IReadOnlyList<JsonMemberInfo>
    {
        public ConstructorInfo Constructor { get; }
        public JsonMemberInfo[] Members { get; }

        public JsonObjectDescription(ConstructorInfo constructor, JsonMemberInfo[] members)
        {
            Constructor = constructor;
            Members = members;
        }

        public JsonObjectDescription(JsonMemberInfo[] members)
        {
            Members = members;
        }

        public IEnumerator<JsonMemberInfo> GetEnumerator()
        {
            for (int i = 0; i < Members.Length; i++)
            {
                yield return Members[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => Members.Length;

        public JsonMemberInfo this[int index] => Members[index];
    }
}