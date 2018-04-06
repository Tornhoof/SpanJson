using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpanJson.Tests
{
    public sealed class UntypedEqualityComparer : IEqualityComparer<object>
    {
        public static readonly UntypedEqualityComparer Default = new UntypedEqualityComparer();
        private delegate bool TypedEqualityComparer(object x, object y);
        private static readonly ConcurrentDictionary<Type, TypedEqualityComparer> Comparers = new ConcurrentDictionary<Type, TypedEqualityComparer>();

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null && y != null)
            {
                return false;
            }

            if (x != null && y == null)
            {
                return false;
            }

            var xType = x.GetType();
            var yType = y.GetType();
            if (!ReferenceEquals(xType, yType))
            {
                return false;
            }

            // ReSharper disable ConvertClosureToMethodGroup
            var typedComparer = Comparers.GetOrAdd(xType, t => BuildTypedComparer(t));
            // ReSharper restore ConvertClosureToMethodGroup
            return typedComparer(x, y);
        }

        private TypedEqualityComparer BuildTypedComparer(Type type)
        {
            var xParameter = Expression.Parameter(typeof(object), "x");
            var yParameter = Expression.Parameter(typeof(object), "y");
            var typedXParameter = Expression.Convert(xParameter, type);
            var typedYParameter = Expression.Convert(yParameter, type);

            return Expression.Lambda<TypedEqualityComparer>(
                Expression.Call(typedXParameter, type.GetMethod("Equals", new Type[] {type}), typedYParameter),
                xParameter, yParameter).Compile();
        }

        public int GetHashCode(object obj)
        {
            return 0;
        }
    }
}