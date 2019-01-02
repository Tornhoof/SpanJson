using System;

namespace SpanJson.Resolvers
{
    public class JsonExtensionMemberInfo
    {
        public JsonExtensionMemberInfo(string memberName, Type memberType, NamingConventions namingConvention, bool excludeNulls)
        {
            MemberName = memberName;
            MemberType = memberType;
            NamingConvention = namingConvention;
            ExcludeNulls = excludeNulls;
        }

        public string MemberName { get; }
        public Type MemberType { get; }
        public NamingConventions NamingConvention { get; }
        public bool ExcludeNulls { get; }
    }
}
