﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpanJson.Shared
{
    public class GenericEqualityComparer<T> : IEqualityComparer<T> where T : class, IGenericEquality<T>
    {
        public static readonly GenericEqualityComparer<T> Default = new GenericEqualityComparer<T>();

        public bool Equals(T x, T y)
        {
            return x.TrueEquals(y);
        }

        public int GetHashCode(T obj)
        {
            return 0; // not fast, but not really important here
        }
    }

    public static class ExtensionMethods
    {
        public static bool TrueEqualsString(this IEnumerable<string> a, IEnumerable<string> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            return a.SequenceEqual(b);
        }

        public static bool TrueEqualsString(this string a, string b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            return a == b;
        }


        public static bool TrueEquals<T>(this T? a, T? b)
            where T : struct
        {
            if (a is null)
            {
                return b is null;
            }

            if (b is null)
            {
                return false;
            }

            return a.Value.Equals(b.Value);
        }

        public static bool TrueEquals<T>(this T a, T b)
            where T : class, IGenericEquality<T>
        {
            if (a is null)
            {
                return b is null;
            }

            if (b is null)
            {
                return false;
            }

            return ReferenceEquals(a, b) || a.Equals(b);
        }

        public static bool TrueEqualsList<T>(this IEnumerable<T> a, IEnumerable<T> b)
            where T : class, IGenericEquality<T>
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            return a.SequenceEqual(b, GenericEqualityComparer<T>.Default);
        }

        public static bool TrueEqualsListDynamic<T>(this IEnumerable<T> a, IEnumerable<dynamic> b)
            where T : class, IGenericEquality<T>
        {
            if (a is null)
            {
                return b is null;
            }

            if (b is null)
            {
                return false;
            }

            if (ReferenceEquals(a, b))
            {
                return true;
            }

            using (var e1 = a.GetEnumerator())
            {
                using (var e2 = b.GetEnumerator())
                {
                    while (true)
                    {
                        var e1Next = e1.MoveNext();
                        var e2Next = e2.MoveNext();
                        if (e1Next != e2Next)
                        {
                            return false;
                        }

                        if (!e1Next && !e2Next)
                        {
                            break;
                        }

                        var c1 = e1.Current;
                        var c2 = e2.Current;

                        if (c1 is null && !(c2 is null))
                        {
                            return false;
                        }

                        if (c2 is null && !(c1 is null))
                        {
                            return false;
                        }

                        if (!c1.EqualsDynamic(c2))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }

        public static bool IsTypedList(this Type type)
        {
            return
                type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IList<>) ||
                type.GetTypeInfo().GetInterfaces().Any(i =>
                    i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IList<>));
        }
    }
}