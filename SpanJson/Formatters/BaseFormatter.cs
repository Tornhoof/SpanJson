using System;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace SpanJson.Formatters
{
    public abstract class BaseFormatter
    {

        protected static Func<T> BuildCreateFunctor<T>(Type defaultType)
        {
            var type = typeof(T);
            if (type.IsInterface)
            {
                type = defaultType;
                if (type == null)
                {
                    return () => throw new NotSupportedException($"Can't create {defaultType.Name}");
                }
            }
            return Expression.Lambda<Func<T>>(Expression.New(type)).Compile();
        }
    }
}