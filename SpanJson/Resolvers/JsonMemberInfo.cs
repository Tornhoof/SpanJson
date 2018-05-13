using System;
using System.Reflection;

namespace SpanJson.Resolvers
{
    public class JsonMemberInfo
    {
        public JsonMemberInfo(string memberName, Type memberType, MethodInfo shouldSerialize, string name, bool excludeNull, bool canRead, bool canWrite)
        {
            MemberName = memberName;
            MemberType = memberType;
            ShouldSerialize = shouldSerialize;
            Name = name;
            ExcludeNull = excludeNull;
            CanRead = canRead;
            CanWrite = canWrite;
        }

        public string MemberName { get; }
        public Type MemberType { get; }
        public MethodInfo ShouldSerialize { get; }
        public string Name { get; }
        public bool ExcludeNull { get; }

        public bool CanRead { get; }
        public bool CanWrite { get; set; }
    }
}