using System;
using System.Reflection;

namespace SpanJson.Resolvers
{
    public class JsonMemberInfo
    {
        public JsonMemberInfo(string memberName, Type memberType, MethodInfo shouldSerialize, string name, bool excludeNull, bool canRead, bool canWrite,
            Type customSerializer, object customSerializerArguments)
        {
            MemberName = memberName;
            MemberType = memberType;
            ShouldSerialize = shouldSerialize;
            Name = name;
            ExcludeNull = excludeNull;
            CanRead = canRead;
            CanWrite = canWrite;
            CustomSerializer = customSerializer;
            CustomSerializerArguments = customSerializerArguments;
        }

        public string MemberName { get; }
        public Type MemberType { get; }
        public MethodInfo ShouldSerialize { get; }
        public string Name { get; }
        public bool ExcludeNull { get; }

        public Type CustomSerializer { get; }
        public object CustomSerializerArguments { get; }

        public bool CanRead { get; }
        public bool CanWrite { get; set; }
    }
}