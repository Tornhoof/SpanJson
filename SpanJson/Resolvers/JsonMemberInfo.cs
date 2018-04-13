using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SpanJson.Resolvers
{
    public class JsonMemberInfo
    {
        public string MemberName { get; }
        public Type MemberType { get;  }
        public MethodInfo ShouldSerialize { get; }
        public string Name { get; }
        public bool ExcludeNull { get; }

        public JsonMemberInfo(string memberName, Type memberType, MethodInfo shouldSerialize, string name, bool excludeNull)
        {
            MemberName = memberName;
            MemberType = memberType;
            ShouldSerialize = shouldSerialize;
            Name = name;
            ExcludeNull = excludeNull;
            if (MemberType.IsValueType)
            {
                ExcludeNull = false; // value types are not null
            }
        }
    }
}
