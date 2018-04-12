using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SpanJson.Resolvers
{
    public class JsonMemberInfo
    {
        public MemberInfo MemberInfo { get; }
        public MethodInfo ShouldSerialize { get; }
        public string Name { get; }
        public bool ExcludeNull { get; }

        public JsonMemberInfo(MemberInfo memberInfo, MethodInfo shouldSerialize, string name, bool excludeNull)
        {
            MemberInfo = memberInfo;
            ShouldSerialize = shouldSerialize;
            Name = name;
            ExcludeNull = excludeNull;
        }
    }
}
