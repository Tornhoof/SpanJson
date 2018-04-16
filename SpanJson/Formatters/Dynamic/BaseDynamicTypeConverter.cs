using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace SpanJson.Formatters.Dynamic
{
    public abstract class BaseDynamicTypeConverter : TypeConverter
    {
        protected delegate object ConvertDelegate(in JsonReader reader);

        protected static Dictionary<Type, ConvertDelegate> BuildDelegates(Type[] allowedTypes)
        {
            var result = new Dictionary<Type, ConvertDelegate>();
            foreach (var allowedType in allowedTypes)
            {
                var method = typeof(JsonReader).GetMethod($"Read{allowedType.Name}");
                if (method != null)
                {
                    var parameter = Expression.Parameter(typeof(JsonReader).MakeByRefType(), "reader");
                    var lambda = Expression.Lambda<ConvertDelegate>(
                        Expression.Convert(Expression.Call(parameter, method), typeof(object)), parameter);
                    result.Add(allowedType, lambda.Compile());
                }
            }

            return result;
        }
    }
}
