using System;

namespace SpanJson
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class JsonConstructorAttribute : Attribute
    {
        public JsonConstructorAttribute()
        {
        }

        public JsonConstructorAttribute(params string[] parameterNames)
        {
            ParameterNames = parameterNames;
        }

        public string[] ParameterNames { get; }
    }
}