using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using SpanJson.Formatters;
using SpanJson.Helpers;

namespace SpanJson.Resolvers
{
    public abstract class ResolverBase
    {
        protected static readonly ParameterExpression DynamicMetaObjectParameterExpression = Expression.Parameter(typeof(object));
    }
    public abstract class ResolverBase<TSymbol, TResolver> : ResolverBase, IJsonFormatterResolver<TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
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

        public JsonObjectDescription GetObjectDescription<T>()
        {
            return Members.GetOrAdd(typeof(T), x => BuildMembers(x));
        }

        /// <summary>
        /// TODO Extend with attributes and ShouldSerialize
        /// </summary>
        public JsonObjectDescription GetDynamicObjectDescription(IDynamicMetaObjectProvider provider)
        {
            var metaObject = provider.GetMetaObject(DynamicMetaObjectParameterExpression);
            var members = metaObject.GetDynamicMemberNames();
            var result = new List<JsonMemberInfo>();
            foreach (var memberInfoName in members)
            {
                var name = Escape(memberInfoName);
                if (_namingConventions == NamingConventions.CamelCase)
                {
                    name = MakeCamelCase(name);
                }

                result.Add(new JsonMemberInfo(memberInfoName, typeof(object), null, name,
                    _nullOptions == NullOptions.ExcludeNulls, true, true));
            }

            return new JsonObjectDescription(null, result.ToArray());
        }

        public IJsonFormatter<T, TSymbol, TResolver> GetFormatter<T>()
        {
            return (IJsonFormatter<T, TSymbol, TResolver>) GetFormatter(typeof(T));
        }

        public static string MakeCamelCase(string name)
        {
            if (char.IsLower(name[0]))
            {
                return name;
            }

            return string.Concat(char.ToLowerInvariant(name[0]), name.Substring(1));
        }

        protected virtual JsonObjectDescription BuildMembers(Type type)
        {
            var publicMembers = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(a => !a.IsLiteral).Cast<MemberInfo>().Concat(
                    type.GetProperties(BindingFlags.Public | BindingFlags.Instance));
            var jsonConstructor = type.GetConstructors().FirstOrDefault(a => a.GetCustomAttribute<JsonConstructorAttribute>() != null);
            var result = new List<JsonMemberInfo>();
            foreach (var memberInfo in publicMembers)
            {
                if (!IsIgnored(memberInfo))
                {
                    var canRead = true;
                    var canWrite = true;
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

            return new JsonObjectDescription(jsonConstructor, result.ToArray());
        }

        private static string Escape(string input)
        {
            return input; // TODO: Find out if necessary
        }

        private static bool IsIgnored(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttribute<IgnoreDataMemberAttribute>() != null;
        }

        private static string GetAttributeName(MemberInfo memberInfo)
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

            if (type.IsArray)
            {
                return GetDefaultOrCreate(typeof(ArrayFormatter<,,>).MakeGenericType(type.GetElementType(),
                    typeof(TSymbol), typeof(TResolver)));
            }

            if (type.IsEnum)
            {
                return GetDefaultOrCreate(typeof(EnumFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IDictionary<,>), out var dictArgumentTypes))
            {
                if (dictArgumentTypes.Length != 2 || dictArgumentTypes[0] != typeof(string))
                {
                    throw new NotImplementedException($"{dictArgumentTypes[0]} is not supported a Key for Dictionary.");
                }

                return GetDefaultOrCreate(typeof(DictionaryFormatter<,,,>).MakeGenericType(type, dictArgumentTypes[1], typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IList<>), out var listArgumentTypes))
            {
                return GetDefaultOrCreate(typeof(ListFormatter<,,,>).MakeGenericType(type, listArgumentTypes.Single(), typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IEnumerable<>), out var enumArgumentTypes))
            {
                return GetDefaultOrCreate(
                    typeof(EnumerableFormatter<,,,>).MakeGenericType(type, enumArgumentTypes.Single(), typeof(TSymbol), typeof(TResolver)));
            }

            if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type))
            {
                return GetDefaultOrCreate(typeof(DynamicMetaObjectProviderFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetNullableUnderlyingType(out var underlyingType))
            {
                return GetDefaultOrCreate(typeof(NullableFormatter<,,>).MakeGenericType(underlyingType,
                    typeof(TSymbol), typeof(TResolver)));
            }

            // no integrated type, let's build it
            if (type.IsValueType)
            {
                return GetDefaultOrCreate(
                    typeof(ComplexStructFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
            }

            return GetDefaultOrCreate(typeof(ComplexClassFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
        }

        private static IJsonFormatter GetIntegrated(Type type)
        {
            var allTypes = typeof(TResolver).Assembly.GetTypes();
            foreach (var candidate in allTypes.Where(a => a.IsPublic))
            {
                if (candidate.IsGenericTypeDefinition && candidate.ContainsGenericParameters && candidate.IsGenericType && candidate.IsClass &&
                    !candidate.IsAbstract)
                {
                    var genArgs = candidate.GetGenericArguments();
                    if (genArgs.Length == 1)
                    {
                        var genericArg = genArgs.Single();
                        if (typeof(IJsonFormatterResolver).IsAssignableFrom(genericArg))
                        {
                            var constraint = genericArg.GetGenericParameterConstraints()[0];
                            var firstConstraintArg = constraint.GetGenericArguments()[0];
                            if (firstConstraintArg == typeof(TSymbol)) // make sure it's the proper one
                            {
                                var iface = typeof(IJsonFormatter<,,>).MakeGenericType(type, typeof(TSymbol), genericArg);
                                if (iface.IsAssignableFrom(candidate))
                                {
                                    return GetDefaultOrCreate(candidate.MakeGenericType(typeof(TResolver)));
                                }
                            }
                        }
                    }
                    else if (genArgs.Length == 2)
                    {
                        var resolverGenArg = genArgs[1];
                        if (typeof(IJsonFormatterResolver).IsAssignableFrom(resolverGenArg))
                        {
                            var constraint = resolverGenArg.GetGenericParameterConstraints()[0];
                            var firstConstraintArg = constraint.GetGenericArguments()[0];
                            if (firstConstraintArg.IsValueType) // make sure it's the proper one
                            {
                                var iface = typeof(IJsonFormatter<,,>).MakeGenericType(type, genArgs[0], genArgs[1]);
                                if (iface.IsAssignableFrom(candidate))
                                {
                                    return GetDefaultOrCreate(candidate.MakeGenericType(typeof(TSymbol), typeof(TResolver)));
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        // ReSharper disable StaticMemberInGenericType
        private static readonly ConcurrentDictionary<Type, IJsonFormatter> Formatters =
            new ConcurrentDictionary<Type, IJsonFormatter>();

        private static readonly ConcurrentDictionary<Type, JsonObjectDescription> Members =
            new ConcurrentDictionary<Type, JsonObjectDescription>();
        // ReSharper restore StaticMemberInGenericType
    }
}