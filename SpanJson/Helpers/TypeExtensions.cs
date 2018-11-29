﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpanJson.Helpers
{
    public static class TypeExtensions
    {
        public static bool TryGetNullableUnderlyingType(this Type type, out Type underlingType)
        {
            if (type.IsValueType)
            {
                underlingType = Nullable.GetUnderlyingType(type);
                return underlingType != null;
            }

            underlingType = default;
            return false;
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsClass || Nullable.GetUnderlyingType(type) != null;
        }

        public static bool TryGetTypeOfGenericInterface(this Type type, Type interfaceType, out Type[] argumentTypes)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"{interfaceType} is not an interface.");
            }

            if (type.IsGenericType && interfaceType.IsAssignableFrom(type.GetGenericTypeDefinition()))
            {
                argumentTypes = type.GetGenericArguments();
                return true;
            }

            var interfaces = type.GetInterfaces();
            var match = interfaces.FirstOrDefault(a => a.IsGenericType && interfaceType.IsAssignableFrom(a.GetGenericTypeDefinition()));
            if (match != null)
            {
                argumentTypes = match.GetGenericArguments();
                return true;
            }

            argumentTypes = default;
            return false;
        }

        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            if (type.IsInterface)
            {
                var propertyInfos = new List<PropertyInfo>();

                var considered = new List<Type>();
                var queue = new Queue<Type>();
                considered.Add(type);
                queue.Enqueue(type);
                while (queue.Count > 0)
                {
                    var subType = queue.Dequeue();
                    foreach (var subInterface in subType.GetInterfaces())
                    {
                        if (considered.Contains(subInterface)) continue;

                        considered.Add(subInterface);
                        queue.Enqueue(subInterface);
                    }

                    var typeProperties = subType.GetProperties(
                        BindingFlags.FlattenHierarchy
                        | BindingFlags.Public
                        | BindingFlags.Instance);

                    var newPropertyInfos = typeProperties
                        .Where(x => !propertyInfos.Contains(x));

                    propertyInfos.InsertRange(0, newPropertyInfos);
                }

                return propertyInfos.ToArray();
            }

            return type.GetProperties(BindingFlags.FlattenHierarchy
                                      | BindingFlags.Public | BindingFlags.Instance);
        }
    }
}