using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SpanJson.Resolvers
{
    public class JsonObjectDescription : IReadOnlyList<JsonMemberInfo>
    {
        public JsonObjectDescription(ConstructorInfo constructor, JsonConstructorAttribute attribute, JsonMemberInfo[] members, JsonExtensionMemberInfo extensionMemberInfo)
        {
            Members = members;
            ExtensionMemberInfo = extensionMemberInfo;
            Constructor = constructor;
            Attribute = attribute;
            if (Constructor != null)
            {
                ConstructorMapping = BuildMapping();
                // we need to sort all the members which are not assigned in the ctor after the ctor assigment, otherwise the object is not ctor'd.
                Array.Sort(Members, (x, y) =>
                {
                    if (ReferenceEquals(x, y))
                    {
                        return 0;
                    }

                    var xIsCtorMapping = ConstructorMapping.TryGetValue(x.MemberName, out var xElement);
                    var yIsCtorMapping = ConstructorMapping.TryGetValue(y.MemberName, out var yElement); 
                    if (!xIsCtorMapping && !yIsCtorMapping) // both are not in, it doesn't matter
                    {
                        return StringComparer.Ordinal.Compare(x.MemberName, y.MemberName);
                    }
                    if (xIsCtorMapping && !yIsCtorMapping) // x is in ctor and y not, move x up
                    {
                        return -1;
                    }

                    if (!xIsCtorMapping && yIsCtorMapping) // x is not in ctor and y is, move x down
                    {
                        return 1;
                    }

                    return xElement.Index.CompareTo(yElement.Index);
                });
            }
        }

        public ConstructorInfo Constructor { get; }
        public JsonConstructorAttribute Attribute { get; }
        public JsonMemberInfo[] Members { get; }
        public JsonExtensionMemberInfo ExtensionMemberInfo { get; }
        public IReadOnlyDictionary<string, (Type Type, int Index)> ConstructorMapping { get; }

        public int Count => Members.Length;

        public JsonMemberInfo this[int index] => Members[index];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<JsonMemberInfo> GetEnumerator()
        {
            for (var i = 0; i < Members.Length; i++)
            {
                yield return Members[i];
            }
        }

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
                if (memberInfoDictionary.TryGetValue(constructorParameter.Name, out var memberInfo) ||
                    Attribute.ParameterNames != null && index < Attribute.ParameterNames.Length &&
                    memberInfoDictionary.TryGetValue(Attribute.ParameterNames[index], out memberInfo))
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