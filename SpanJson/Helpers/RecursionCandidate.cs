using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace SpanJson.Helpers
{
    public abstract class RecursionCandidate
    {
        protected static readonly ConcurrentDictionary<Type, bool> RuntimeLookup = new ConcurrentDictionary<Type, bool>();

        public static bool LookupRecursionCandidate(Type type)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            return RuntimeLookup.GetOrAdd(type, t => BuildLookupFunctor(t));
            // ReSharper restore ConvertClosureToMethodGroup
        }

        private static bool BuildLookupFunctor(Type type)
        {
            var functor = Expression.Lambda<Func<bool>>(Expression.Field(null, typeof(RecursionCandidate<>).MakeGenericType(type),
                nameof(RecursionCandidate<object>.IsRecursionCandidate))).Compile();
            return functor();
        }
    }

    public sealed class RecursionCandidate<T> : RecursionCandidate
    {
        public static readonly bool IsRecursionCandidate = RuntimeHelpers.IsReferenceOrContainsReferences<T>() && CheckForRecursiveType(typeof(T));

        private static bool CheckForRecursiveType(Type type)
        {
            var alreadySeen = new HashSet<Type> {type};
            var queue = new Queue<Type>();
            queue.Enqueue(type);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var publicMembers = current.SerializableMembers();

                foreach (var memberInfo in publicMembers)
                {
                    var memberType = memberInfo is PropertyInfo pi ? pi.PropertyType : memberInfo is FieldInfo fi ? fi.FieldType : null;
                    if (memberType == null || memberInfo.GetCustomAttribute<IgnoreDataMemberAttribute>() != null)
                    {
                        continue;
                    }

                    memberType = Nullable.GetUnderlyingType(memberType) ?? memberType;

                    if (memberType.IsArray)
                    {
                        memberType = memberType.GetElementType();
                    }

                    if (memberType.TryGetTypeOfGenericInterface(typeof(IEnumerable<>), out var argumentTypes))
                    {
                        memberType = argumentTypes[0];
                        if (memberType.IsGenericType && typeof(KeyValuePair<,>).IsAssignableFrom(memberType.GetGenericTypeDefinition()))
                        {
                            memberType = memberType.GetGenericArguments()[1]; // keyvaluepair->value is important
                        }
                    }

                    if (!alreadySeen.Add(memberType))
                    {
                        RuntimeLookup.TryAdd(type, true);
                        return true;
                    }

                    if (LookupRecursionCandidate(memberType))
                    {
                        queue.Enqueue(memberType);
                    }
                }
            }

            RuntimeLookup.TryAdd(type, false);
            return false;
        }
    }
}