using System;
using System.Collections.Generic;
using System.Text;

namespace SpanJson
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class JsonConstructorAttribute : Attribute
    {
        public string[] ParameterNames { get; }

        public JsonConstructorAttribute()
        {
            
        }

        public JsonConstructorAttribute(params string[] parameterNames)
        {
            ParameterNames = parameterNames;
        }
    }
}
