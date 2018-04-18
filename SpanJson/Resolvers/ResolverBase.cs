using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using SpanJson.Formatters;
using SpanJson.Helpers;

namespace SpanJson.Resolvers
{
    public abstract class ResolverBase<TResolver> : IJsonFormatterResolver<TResolver>
        where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        private readonly NamingConventions _namingConventions;
        private readonly NullOptions _nullOptions;

        protected ResolverBase(NullOptions nullOptions, NamingConventions namingConventions)
        {
            _nullOptions = nullOptions;
            _namingConventions = namingConventions;
        }

        public IJsonFormatter GetFormatter(Type type)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            return Formatters.GetOrAdd(type, x => BuildFormatter(x));
            // ReSharper restore ConvertClosureToMethodGroup
        }

        public JsonMemberInfo[] GetMemberInfos(Type type)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            return Members.GetOrAdd(type, x => BuildMembers(x));
            // ReSharper restore ConvertClosureToMethodGroup
        }

        public IJsonFormatter<T, TResolver> GetFormatter<T>()
        {
            return (IJsonFormatter<T, TResolver>) GetFormatter(typeof(T));
        }

        public JsonMemberInfo[] GetMemberInfos<T>()
        {
            return GetMemberInfos(typeof(T));
        }

        protected virtual JsonMemberInfo[] BuildMembers(Type type)
        {
            var publicMembers = type.GetFields().Cast<MemberInfo>().Concat(type.GetProperties());
            var result = new List<JsonMemberInfo>();
            foreach (var memberInfo in publicMembers)
            {
                if (!IsIgnored(memberInfo))
                {
                    bool canRead = true;
                    bool canWrite = true;
                    if (memberInfo is PropertyInfo propertyInfo)
                    {
                        canRead = propertyInfo.CanRead;
                        canWrite = propertyInfo.CanWrite;
                    }
                    var name = Escape(GetAttributeName(memberInfo) ?? memberInfo.Name);
                    if (_namingConventions == NamingConventions.CamelCase)
                    {
                        name = MakeCamelCase(name);
                    }

                    var shouldSerialize = type.GetMethod($"ShouldSerialize{memberInfo.Name}");
                    var memberType = memberInfo is FieldInfo fi ? fi.FieldType :
                        memberInfo is PropertyInfo pi ? pi.PropertyType : null;
                    result.Add(new JsonMemberInfo(memberInfo.Name, memberType, shouldSerialize, name,
                        _nullOptions == NullOptions.ExcludeNulls, canRead, canWrite));
                }
            }

            return result.ToArray();
        }

        private static string MakeCamelCase(string name)
        {
            if (char.IsLower(name[0]))
            {
                return name;
            }

            return string.Concat(char.ToLowerInvariant(name[0]), name.Substring(1));
        }

        private string Escape(string input)
        {
            return input; // todo later
        }

        private bool IsIgnored(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<IgnoreDataMemberAttribute>() != null;
        }

        private string GetAttributeName(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<DataMemberAttribute>()?.Name;
        }

        private static IJsonFormatter GetDefaultOrCreate(Type type)
        {
            return (IJsonFormatter) (type.GetField("Default", BindingFlags.Public | BindingFlags.Static)
                                         ?.GetValue(null) ?? Activator.CreateInstance(type));
        }

        private static IJsonFormatter BuildFormatter(Type type)
        {
            var integrated = GetIntegrated(type);
            if (integrated != null)
            {
                return integrated;
            }
            // todo: support for multidimensional array
            if (type.IsArray)
            {
                return GetIntegrated(type) ??
                       GetDefaultOrCreate(typeof(ArrayFormatter<,>).MakeGenericType(type.GetElementType(),
                           typeof(TResolver)));
            }

            if (type.TryGetDictionaryType(out var keyType, out var valueType))
            {
                if (keyType != typeof(string))
                {
                    throw new NotImplementedException($"{keyType} is not supported a Key for Dictionary.");
                }

                return GetIntegrated(type) ?? GetDefaultOrCreate(typeof(DictionaryFormatter<,,>).MakeGenericType(type, valueType, typeof(TResolver)));
            }

            if (type.TryGetListType(out var elementType))
            {
                return GetIntegrated(type) ??
                       GetDefaultOrCreate(
                           typeof(ListFormatter<,>).MakeGenericType(elementType, typeof(TResolver)));
            }

            if (type.IsEnum)
            {
                return GetDefaultOrCreate(typeof(EnumFormatter<,>).MakeGenericType(type, typeof(TResolver)));
            }

            if (type.TryGetNullableUnderlyingType(out var underlyingType))
            {
                return GetIntegrated(type) ??
                       GetDefaultOrCreate(typeof(NullableFormatter<,>).MakeGenericType(underlyingType,
                           typeof(TResolver)));
            }

            // no integrated type, let's build it
            if (type.IsValueType)
            {
                return GetDefaultOrCreate(
                    typeof(ComplexStructFormatter<,>).MakeGenericType(type, typeof(TResolver)));
            }

            return GetDefaultOrCreate(typeof(ComplexClassFormatter<,>).MakeGenericType(type, typeof(TResolver)));
        }

        private static IJsonFormatter GetIntegrated(Type type)
        {
            var allTypes = typeof(TResolver).Assembly.GetTypes();
            foreach (var allType in allTypes)
            {
                if (allType.IsGenericTypeDefinition && allType.ContainsGenericParameters && allType.IsGenericType)
                {
                    var genericArgs = allType.GetGenericArguments();
                    if (genericArgs.Length == 1 && typeof(IJsonFormatterResolver).IsAssignableFrom(genericArgs[0]))
                    {
                        var iface = typeof(IJsonFormatter<,>).MakeGenericType(type, genericArgs[0]);
                        if (iface.IsAssignableFrom(allType))
                        {
                            return GetDefaultOrCreate(allType.MakeGenericType(typeof(TResolver)));
                        }
                    }
                }
            }

            return null;
        }

        // ReSharper disable StaticMemberInGenericType
        private static readonly ConcurrentDictionary<Type, IJsonFormatter> Formatters =
            new ConcurrentDictionary<Type, IJsonFormatter>();

        private static readonly ConcurrentDictionary<Type, JsonMemberInfo[]> Members =
            new ConcurrentDictionary<Type, JsonMemberInfo[]>();
        // ReSharper restore StaticMemberInGenericType
    }
}