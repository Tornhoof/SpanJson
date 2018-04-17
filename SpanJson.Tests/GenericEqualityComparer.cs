using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpanJson.Tests
{
    public sealed class GenericEqualityComparer : IEqualityComparer<object>
    {
        private static readonly ConcurrentDictionary<Type, TypedEqualityComparerDelegate> Comparers =
            new ConcurrentDictionary<Type, TypedEqualityComparerDelegate>();

        public static readonly GenericEqualityComparer Default = new GenericEqualityComparer();

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x == null && y != null) return false;

            if (x != null && y == null) return false;

            var xType = x.GetType();
            var yType = y.GetType();
            if (!ReferenceEquals(xType, yType)) return false;

            // ReSharper disable ConvertClosureToMethodGroup
            var typedComparer = Comparers.GetOrAdd(xType, t => BuildTypedComparer(t));
            // ReSharper restore ConvertClosureToMethodGroup
            return typedComparer(x, y);
        }

        public int GetHashCode(object obj)
        {
            return 0;
        }

        private TypedEqualityComparerDelegate BuildTypedComparer(Type type)
        {
            var xParameter = Expression.Parameter(typeof(object), "x");
            var yParameter = Expression.Parameter(typeof(object), "y");
            var typedXParameter = Expression.Convert(xParameter, type);
            var typedYParameter = Expression.Convert(yParameter, type);

            return Expression.Lambda<TypedEqualityComparerDelegate>(
                Expression.Call(typedXParameter, type.GetMethod("Equals", new[] {type}), typedYParameter),
                xParameter, yParameter).Compile();
        }

        private delegate bool TypedEqualityComparerDelegate(object x, object y);
    }

    public sealed class DynamicEqualityComparer : IEqualityComparer<object>
    {
        private static readonly ConcurrentDictionary<Type, DynamicEqualityComparerDelegate> Comparers =
            new ConcurrentDictionary<Type, DynamicEqualityComparerDelegate>();

        public static readonly DynamicEqualityComparer Default = new DynamicEqualityComparer();

        public new bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x == null && y != null) return false;

            if (x != null && y == null) return false;

            var xType = x.GetType();

            // ReSharper disable ConvertClosureToMethodGroup
            var typedComparer = Comparers.GetOrAdd(xType, t => BuildTypedComparer(t));
            // ReSharper restore ConvertClosureToMethodGroup
            return typedComparer(x, y);
        }

        public int GetHashCode(object obj)
        {
            return 0;
        }

        private DynamicEqualityComparerDelegate BuildTypedComparer(Type type)
        {
            var xParameter = Expression.Parameter(typeof(object), "x");
            var yParameter = Expression.Parameter(typeof(object), "y");
            var typedXParameter = Expression.Convert(xParameter, type);

            return Expression.Lambda<DynamicEqualityComparerDelegate>(
                Expression.Call(typedXParameter, type.GetMethod("EqualsDynamic"), yParameter),
                xParameter, yParameter).Compile();
        }

        private delegate bool DynamicEqualityComparerDelegate(object x, dynamic y);
    }
}