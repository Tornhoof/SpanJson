using System;
using System.Linq.Expressions;

namespace SpanJson.Formatters
{
    public abstract class BaseFormatter
    {
        protected static Func<T> BuildCreateFunctor<T>() where T : new()
        {
            return Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }
    }
}