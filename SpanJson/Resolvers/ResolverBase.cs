using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static readonly IReadOnlyDictionary<Type, JsonConstructorAttribute> BaseClassJsonConstructorMap = BuildMap();

        protected static readonly ParameterExpression DynamicMetaObjectParameterExpression = Expression.Parameter(typeof(object));

        protected static bool TryGetBaseClassJsonConstructorAttribute(Type type, out JsonConstructorAttribute attribute)
        {
            if (BaseClassJsonConstructorMap.TryGetValue(type, out attribute))
            {
                return true;
            }

            if (type.IsGenericType && BaseClassJsonConstructorMap.TryGetValue(type.GetGenericTypeDefinition(), out attribute))
            {
                return true;
            }

            return false;
        }

        private static Dictionary<Type, JsonConstructorAttribute> BuildMap()
        {
            // TODO: what to do with the 8 args constructor with TRest?
            var result = new Dictionary<Type, JsonConstructorAttribute>
            {
                {typeof(KeyValuePair<,>), new JsonConstructorAttribute()},
                {typeof(Tuple<,>), new JsonConstructorAttribute()},
                {typeof(Tuple<,,>), new JsonConstructorAttribute()},
                {typeof(Tuple<,,,>), new JsonConstructorAttribute()},
                {typeof(Tuple<,,,,>), new JsonConstructorAttribute()},
                {typeof(Tuple<,,,,,>), new JsonConstructorAttribute()},
                {typeof(Tuple<,,,,,,>), new JsonConstructorAttribute()},
                {typeof(ValueTuple<,>), new JsonConstructorAttribute()},
                {typeof(ValueTuple<,,>), new JsonConstructorAttribute()},
                {typeof(ValueTuple<,,,>), new JsonConstructorAttribute()},
                {typeof(ValueTuple<,,,,>), new JsonConstructorAttribute()},
                {typeof(ValueTuple<,,,,,>), new JsonConstructorAttribute()},
                {typeof(ValueTuple<,,,,,,>), new JsonConstructorAttribute()}
            };

            return result;
        }
    }

    public abstract class ResolverBase<TSymbol, TResolver> : ResolverBase, IJsonFormatterResolver<TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        private readonly SpanJsonOptions _spanJsonOptions;

        // ReSharper disable StaticMemberInGenericType
        private static readonly ConcurrentDictionary<Type, IJsonFormatter> Formatters =
            new ConcurrentDictionary<Type, IJsonFormatter>();
        // ReSharper restore StaticMemberInGenericType


        protected ResolverBase(SpanJsonOptions spanJsonOptions)
        {
            _spanJsonOptions = spanJsonOptions;
        }

        public IJsonFormatter GetFormatter(Type type)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            return Formatters.GetOrAdd(type, x => BuildFormatter(x));
            // ReSharper restore ConvertClosureToMethodGroup
        }

        /// <summary>
        /// Override a formatter on global scale, additionally we might need to register array versions etc
        /// Only register primitive types here, no arrays etc. this create weird problems.
        /// </summary>
        protected void RegisterGlobalCustomFormatter<T, TFormatter>() where TFormatter : ICustomJsonFormatter<T>
        {
            var type = typeof(T);
            var formatterType = typeof(TFormatter);
            var staticDefaultField = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
            if (staticDefaultField == null)
            {
                throw new InvalidOperationException($"{formatterType.FullName} must have a public static field 'Default' returning an instance of it.");
            }

            Formatters.AddOrUpdate(type, GetDefaultOrCreate(formatterType), (t, formatter) => GetDefaultOrCreate(formatterType));
        }

        public IJsonFormatter GetFormatter(JsonMemberInfo memberInfo, Type overrideMemberType = null)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            if (memberInfo.CustomSerializer != null)
            {
                return GetDefaultOrCreate(memberInfo.CustomSerializer);
            }

            var type = overrideMemberType ?? memberInfo.MemberType;
            return GetFormatter(type);
            // ReSharper restore ConvertClosureToMethodGroup
        }

        public JsonObjectDescription GetDynamicObjectDescription(IDynamicMetaObjectProvider provider)
        {
            var metaObject = provider.GetMetaObject(DynamicMetaObjectParameterExpression);
            var members = metaObject.GetDynamicMemberNames();
            var result = new List<JsonMemberInfo>();
            foreach (var memberInfoName in members)
            {
                var name = Escape(memberInfoName);
                if (_spanJsonOptions.NamingConvention == NamingConventions.CamelCase)
                {
                    name = MakeCamelCase(name);
                }

                result.Add(new JsonMemberInfo(memberInfoName, typeof(object), null, name,
                    _spanJsonOptions.NullOption == NullOptions.ExcludeNulls, true, true, null));
            }

            return new JsonObjectDescription(null, null, result.ToArray(), null);
        }

        public IJsonFormatter<T, TSymbol> GetFormatter<T>()
        {
            return (IJsonFormatter<T, TSymbol>) GetFormatter(typeof(T));
        }

        public JsonObjectDescription GetObjectDescription<T>()
        {
            return BuildMembers(typeof(T)); // no need to cache that
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
            var result = new List<JsonMemberInfo>();
            JsonExtensionMemberInfo extensionMemberInfo = null;
            var excludeNulls = _spanJsonOptions.NullOption == NullOptions.ExcludeNulls;
            foreach (var memberInfo in publicMembers)
            {
                var memberType = memberInfo is FieldInfo fi ? fi.FieldType :
                    memberInfo is PropertyInfo pi ? pi.PropertyType : null;
                var name = Escape(GetAttributeName(memberInfo) ?? memberInfo.Name);
                if (_spanJsonOptions.NamingConvention == NamingConventions.CamelCase)
                {
                    name = MakeCamelCase(name);
                }

                var canRead = true;
                var canWrite = true;
                if (memberInfo is PropertyInfo propertyInfo)
                {
                    canRead = propertyInfo.CanRead;
                    canWrite = propertyInfo.CanWrite;
                }

                if (memberInfo.GetCustomAttribute<JsonExtensionDataAttribute>() != null && typeof(IDictionary<string, object>).IsAssignableFrom(memberType) && canRead && canWrite)
                {
                    extensionMemberInfo = new JsonExtensionMemberInfo(memberInfo.Name, memberType, _spanJsonOptions.NamingConvention, excludeNulls);
                }
                else if (!IsIgnored(memberInfo))
                {

                    var customSerializer = memberInfo.GetCustomAttribute<JsonCustomSerializerAttribute>()?.Type;
                    var shouldSerialize = type.GetMethod($"ShouldSerialize{memberInfo.Name}");
                    result.Add(new JsonMemberInfo(memberInfo.Name, memberType, shouldSerialize, name, excludeNulls, canRead, canWrite, customSerializer));
                }
            }

            TryGetAnnotedAttributeConstructor(type, out var constructor, out var attribute);
            return new JsonObjectDescription(constructor, attribute, result.ToArray(), extensionMemberInfo);
        }

        private void TryGetAnnotedAttributeConstructor(Type type, out ConstructorInfo constructor, out JsonConstructorAttribute attribute)
        {
            constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(a => a.GetCustomAttribute<JsonConstructorAttribute>() != null);
            if (constructor != null)
            {
                attribute = constructor.GetCustomAttribute<JsonConstructorAttribute>();
                return;
            }

            if (TryGetBaseClassJsonConstructorAttribute(type, out attribute))
            {
                // We basically take the one with the most parameters, this needs to match the dictionary // TODO find better method
                constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).OrderByDescending(a => a.GetParameters().Length)
                    .FirstOrDefault();
                return;
            }

            constructor = default;
            attribute = default;
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
                                         ?.GetValue(null) ?? Activator.CreateInstance(type)); // leave the createinstance here, this helps with recursive types
        }

        private IJsonFormatter BuildFormatter(Type type)
        {
            var integrated = GetIntegrated(type);
            if (integrated != null)
            {
                return integrated;
            }

            if (type == typeof(object))
            {
                return GetDefaultOrCreate(typeof(RuntimeFormatter<TSymbol, TResolver>));
            }

            if (type.IsArray)
            {
                var rank = type.GetArrayRank();
                switch (rank)
                {
                    case 1:
                        return GetDefaultOrCreate(typeof(ArrayFormatter<,,>).MakeGenericType(type.GetElementType(),
                            typeof(TSymbol), typeof(TResolver)));
                    case 2:
                        return GetDefaultOrCreate(typeof(TwoDimensionalArrayFormatter<,,>).MakeGenericType(type.GetElementType(),
                            typeof(TSymbol), typeof(TResolver)));
                    default:
                        throw new NotSupportedException("Only One- and Two-dimensional arrrays are supported.");
                }


            }

            if (type.IsEnum)
            {
                switch (_spanJsonOptions.EnumOption)
                {
                    case EnumOptions.String:
                        return GetDefaultOrCreate(typeof(EnumStringFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
                    case EnumOptions.Integer:
                        return GetDefaultOrCreate(typeof(EnumIntegerFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
                }
            }

            if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type))
            {
                return GetDefaultOrCreate(typeof(DynamicMetaObjectProviderFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IDictionary<,>), out var dictArgumentTypes) && !IsBadDictionary(type))
            {
                if (dictArgumentTypes.Length != 2 || dictArgumentTypes[0] != typeof(string))
                {
                    throw new NotImplementedException($"{dictArgumentTypes[0]} is not supported a Key for Dictionary.");
                }

                return GetDefaultOrCreate(typeof(DictionaryFormatter<,,,>).MakeGenericType(type, dictArgumentTypes[1], typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IReadOnlyDictionary<,>), out var rodictArgumentTypes))
            {
                if (rodictArgumentTypes.Length != 2 || rodictArgumentTypes[0] != typeof(string))
                {
                    throw new NotImplementedException($"{rodictArgumentTypes[0]} is not supported a Key for Dictionary.");
                }

                return GetDefaultOrCreate(
                    typeof(ReadOnlyDictionaryFormatter<,,,>).MakeGenericType(type, rodictArgumentTypes[1], typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IList<>), out var listArgumentTypes) && !IsBadList(type))
            {
                return GetDefaultOrCreate(typeof(ListFormatter<,,,>).MakeGenericType(type, listArgumentTypes.Single(), typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IEnumerable<>), out var enumArgumentTypes))
            {
                return GetDefaultOrCreate(
                    typeof(EnumerableFormatter<,,,>).MakeGenericType(type, enumArgumentTypes.Single(), typeof(TSymbol), typeof(TResolver)));
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

        protected virtual bool IsBadDictionary(Type type)
        {
            // ReadOnlyDictionary is kinda broken, it implements IDictionary<T> too, but without any standard ctor
            // Make sure this is using the ReadOnlyDictionaryFormatter
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ReadOnlyDictionary<,>))
            {
                return true;
            }

            return false;
        }

        protected virtual bool IsBadList(Type type)
        {
            // ReadOnlyCollection is kinda broken, it implements IList<T> too, but without any standard ctor
            // Make sure this is using the EnumerableFormatter
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>))
            {
                return true;
            }

            return false;
        }

        private static IJsonFormatter GetIntegrated(Type type)
        {
            var allTypes = typeof(ResolverBase).Assembly.GetTypes();
            foreach (var candidate in allTypes.Where(a => a.IsPublic))
            {
                if (candidate.TryGetTypeOfGenericInterface(typeof(ICustomJsonFormatter<>), out _))
                {
                    continue;
                }

                if (candidate.TryGetTypeOfGenericInterface(typeof(IJsonFormatter<,>), out var argumentTypes) && argumentTypes.Length == 2)
                {
                    if (argumentTypes[0] == type && argumentTypes[1] == typeof(TSymbol))
                    {
                        // if it has a custom formatter for a base type (i.e. nullable base type, array element, list element)
                        // we need to ignore the integrated types for this
                        if (HasCustomFormatterForRelatedType(type))
                        {
                            continue;
                        }

                        return GetDefaultOrCreate(candidate);
                    }
                }
            }

            return null;
        }

        private static bool HasCustomFormatterForRelatedType(Type type)
        {
            Type relatedType = Nullable.GetUnderlyingType(type);
            if (relatedType == null && type.IsArray)
            {
                relatedType = type.GetElementType();
            }

            if (relatedType == null && type.TryGetTypeOfGenericInterface(typeof(IList<>), out var argumentTypes) && argumentTypes.Length == 1)
            {
                relatedType = argumentTypes.Single();
            }

            if (relatedType != null)
            {
                if (Formatters.TryGetValue(relatedType, out var formatter) && formatter is ICustomJsonFormatter)
                {
                    return true;
                }

                if (Nullable.GetUnderlyingType(relatedType) != null)
                {
                    return HasCustomFormatterForRelatedType(relatedType); // we need to recurse if the related type is again nullable
                }
            }

            return false;
        }

        public virtual Func<T> GetCreateFunctor<T>()
        {
            var type = typeof(T);
            var ci = type.GetConstructor(Type.EmptyTypes);
            if (type.IsInterface || ci == null)
            {
                type = GetFunctorFallBackType(type);
                if (type == null)
                {
                    return () => throw new NotSupportedException($"Can't create {typeof(T).Name}.");
                }
            }

            return Expression.Lambda<Func<T>>(Expression.New(type)).Compile();
        }

        protected virtual Type GetFunctorFallBackType(Type type)
        {
            if (type.TryGetTypeOfGenericInterface(typeof(IDictionary<,>), out var dictArgumentTypes))
            {
                return typeof(Dictionary<,>).MakeGenericType(dictArgumentTypes);
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IList<>), out var listArgumentTypes))
            {
                return typeof(List<>).MakeGenericType(listArgumentTypes);
            }

            if (type.TryGetTypeOfGenericInterface(typeof(ISet<>), out var setArgumentTypes))
            {
                return typeof(HashSet<>).MakeGenericType(setArgumentTypes);
            }

            return null;
        }


        public virtual Func<T, TConverted> GetEnumerableConvertFunctor<T, TConverted>()
        {
            var inputType = typeof(T);
            var convertedType = typeof(TConverted);

            if (convertedType.IsAssignableFrom(inputType))
            {
                var pExpression = Expression.Parameter(inputType, "input");
                return Expression.Lambda<Func<T, TConverted>>(Expression.Convert(pExpression, convertedType), pExpression).Compile();
            }

            if (IsUnsupportedEnumerable(convertedType))
            {
                return _ => throw new NotSupportedException($"{typeof(TConverted).Name} is not supported.");
            }

            if (convertedType.IsInterface)
            {
                convertedType = GetFunctorFallBackType(convertedType);
                if (convertedType == null)
                {
                    return _ => throw new NotSupportedException($"Can't convert {typeof(T).Name} to {typeof(TConverted).Name}.");
                }
            }

            var paramExpression = Expression.Parameter(inputType, "input");
            var ci = convertedType.GetConstructors().FirstOrDefault(a =>
                a.GetParameters().Length == 1 && a.GetParameters().Single().ParameterType.IsAssignableFrom(paramExpression.Type));
            if (ci == null)
            {
                return _ => throw new NotSupportedException($"No constructor of {convertedType.Name} accepts {paramExpression.Type.Name}.");
            }

            var lambda = Expression.Lambda<Func<T, TConverted>>(Expression.New(ci, paramExpression), paramExpression);
            return lambda.Compile();
        }

        /// <summary>
        /// Some types are just bad to be deserialized for enumerables
        /// </summary>
        protected virtual bool IsUnsupportedEnumerable(Type type)
        {
            // TODO: Stack/ConcurrentStack require that the order of the elements is reversed on deserialization, block it for now
            if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Stack<>) || type.GetGenericTypeDefinition() == typeof(ConcurrentStack<>)))
            {
                return true;
            }

            return false;
        }
    }
}