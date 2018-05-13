using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SpanJson.Resolvers
{
    public class JsonObjectDescription : IReadOnlyList<JsonMemberInfo>
    {
        public ConstructorInfo Constructor { get; }
        public JsonConstructorAttribute Attribute { get; }
        public JsonMemberInfo[] Members { get; }
        public IReadOnlyDictionary<string, (Type Type, int Index)> ConstructorMapping { get; }

        public JsonObjectDescription(ConstructorInfo constructor, JsonConstructorAttribute attribute, JsonMemberInfo[] members)
        {
            Members = members;
            Constructor = constructor;
            Attribute = attribute;
            if (Constructor != null)
            {
                ConstructorMapping = BuildMapping();
            }
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

        private Dictionary<string, (Type Type, int Index)> BuildMapping()
        {
            var memberInfoDictionary = new Dictionary<string, JsonMemberInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (var jsonMemberInfo in Members)
            {
                memberInfoDictionary.Add(jsonMemberInfo.MemberName, jsonMemberInfo);
            }

            var constructorValueIndexDictionary = new Dictionary<string, (Type Type, int Index)>(StringComparer.OrdinalIgnoreCase);
            var constructorParameters = Constructor.GetParameters();
            var index = 0;
            foreach (var constructorParameter in constructorParameters)
            {
                JsonMemberInfo memberInfo;
                if (memberInfoDictionary.TryGetValue(constructorParameter.Name, out memberInfo) ||
                    (Attribute.ParameterNames != null && index < Attribute.ParameterNames.Length &&
                     memberInfoDictionary.TryGetValue(Attribute.ParameterNames[index], out memberInfo)))
                {
                    constructorValueIndexDictionary[memberInfo.MemberName] = (memberInfo.MemberType, index++);
                    memberInfo.CanWrite = true; // it's writeable now
                }
                else
                {
                    throw new InvalidOperationException($"Can't map constructor parameter {constructorParameter.Name} to any member.");
                }
            }

            return constructorValueIndexDictionary;
        }
    }
}