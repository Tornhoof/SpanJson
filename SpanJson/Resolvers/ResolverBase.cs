using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SpanJson.Formatters;
using SpanJson.Helpers;
// ReSharper disable VirtualMemberNeverOverridden.Global

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

        public static IJsonFormatter GetDefaultOrCreate(Type type)
        {
            return (IJsonFormatter)(type.GetField("Default", BindingFlags.Public | BindingFlags.Static)
                                        ?.GetValue(null) ?? Activator.CreateInstance(type)); // leave the createinstance here, this helps with recursive types
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

        public virtual IJsonFormatter GetFormatter(Type type)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            return Formatters.GetOrAdd(type, x => BuildFormatter(x));
            // ReSharper restore ConvertClosureToMethodGroup
        }

        /// <summary>
        /// Override a formatter on global scale, additionally we might need to register array versions etc
        /// Only register primitive types here, no arrays etc. this creates weird problems.
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

        public virtual IJsonFormatter GetFormatter(JsonMemberInfo memberInfo, Type overrideMemberType = null)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            if (memberInfo.CustomSerializer != null)
            {
                var formatter = GetDefaultOrCreate(memberInfo.CustomSerializer);
                if (formatter is ICustomJsonFormatter csf && memberInfo.CustomSerializerArguments != null)
                {
                    csf.Arguments = memberInfo.CustomSerializerArguments;
                }
                return formatter;
            }

            var type = overrideMemberType ?? memberInfo.MemberType;
            return GetFormatter(type);
            // ReSharper restore ConvertClosureToMethodGroup
        }

        public virtual JsonObjectDescription GetDynamicObjectDescription(IDynamicMetaObjectProvider provider)
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
                    _spanJsonOptions.NullOption == NullOptions.ExcludeNulls, true, true, null, null));
            }

            return new JsonObjectDescription(null, null, result.ToArray(), null);
        }

        public virtual IJsonFormatter<T, TSymbol> GetFormatter<T>()
        {
            return (IJsonFormatter<T, TSymbol>) GetFormatter(typeof(T));
        }

        public virtual JsonObjectDescription GetObjectDescription<T>()
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
            var publicMembers = type.SerializableMembers();
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
                    var customSerializerAttr = memberInfo.GetCustomAttribute<JsonCustomSerializerAttribute>();
                    var shouldSerialize = type.GetMethod($"ShouldSerialize{memberInfo.Name}");
                    result.Add(new JsonMemberInfo(memberInfo.Name, memberType, shouldSerialize, name, excludeNulls, canRead, canWrite, customSerializerAttr?.Type, customSerializerAttr?.Arguments));
                }
            }

            TryGetAnnotatedAttributeConstructor(type, out var constructor, out var attribute);
            return new JsonObjectDescription(constructor, attribute, result.ToArray(), extensionMemberInfo);
        }

        protected virtual void TryGetAnnotatedAttributeConstructor(Type type, out ConstructorInfo constructor, out JsonConstructorAttribute attribute)
        {
            constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(a => a.GetCustomAttribute<JsonConstructorAttribute>() != null);
            if (constructor != null)
            {
                attribute = constructor.GetCustomAttribute<JsonConstructorAttribute>();
                return;
            }

            if (TryGetBaseClassJsonConstructorAttribute(type, out attribute) || type.GetMethod("<Clone>$") != null)
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

        protected virtual IJsonFormatter BuildFormatter(Type type)
        {
            if (type == typeof(byte[]) && _spanJsonOptions.ByteArrayOption == ByteArrayOptions.Base64)
            {
                return GetDefaultOrCreate(typeof(ByteArrayBase64Formatter<TSymbol, TResolver>));
            }

            var integrated = GetIntegrated(type);
            if (integrated != null)
            {
                return integrated;
            }

            JsonCustomSerializerAttribute attr;
            if ((attr = type.GetCustomAttribute<JsonCustomSerializerAttribute>()) != null)
            {
                var formatter = GetDefaultOrCreate(attr.Type);
                if (formatter is ICustomJsonFormatter csf && attr.Arguments != null)
                {
                    csf.Arguments = attr.Arguments;
                }
                return formatter;
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
                    {
                        if (type.GetCustomAttribute<FlagsAttribute>() != null)
                        {
                            var enumBaseType = Enum.GetUnderlyingType(type);
                            return GetDefaultOrCreate(typeof(EnumStringFlagsFormatter<,,,>).MakeGenericType(type, enumBaseType, typeof(TSymbol), typeof(TResolver)));
                        }

                        return GetDefaultOrCreate(typeof(EnumStringFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
                    }
                    case EnumOptions.Integer:
                        return GetDefaultOrCreate(typeof(EnumIntegerFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
                }
            }

            if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type))
            {
                return GetDefaultOrCreate(typeof(DynamicMetaObjectProviderFormatter<,,>).MakeGenericType(type, typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IDictionary<,>), out var dictArgumentTypes) && HasApplicableCtor(type))
            {
                var writableType = type.IsInterface ? GetFunctorFallBackType(type) : type;
                return GetDefaultOrCreate(typeof(DictionaryFormatter<,,,,,>).MakeGenericType(type, writableType, dictArgumentTypes[0], dictArgumentTypes[1],
                    typeof(TSymbol), typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IReadOnlyDictionary<,>), out var rodictArgumentTypes))
            {
                var writableType = typeof(Dictionary<,>).MakeGenericType(rodictArgumentTypes);
                return GetDefaultOrCreate(
                    typeof(DictionaryFormatter<,,,,,>).MakeGenericType(type, writableType, rodictArgumentTypes[0], rodictArgumentTypes[1], typeof(TSymbol),
                        typeof(TResolver)));
            }

            if (type.TryGetTypeOfGenericInterface(typeof(IList<>), out var listArgumentTypes) && HasApplicableCtor(type))
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

        /// <summary>
        /// Either standard ctor or ctor with constructor for proper values
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual bool HasApplicableCtor(Type type)
        {
            // ReadOnlyDictionary is kinda broken, it implements IDictionary<T> too, but without any standard ctor
            // Make sure this is using the ReadOnlyDictionaryFormatter
            // ReadOnlyCollection is kinda broken, it implements ICollection<T> too, but without any standard ctor
            // Make sure this is using the EnumerableFormatter
            if (type.IsInterface)
            {
                return true; // late checking with fallback
            }

            return type.GetConstructor(Type.EmptyTypes) != null;
        }

        private static IJsonFormatter GetIntegrated(Type type)
        {
            var allTypes = typeof(ResolverBase).Assembly.GetTypes();
            foreach (var candidate in allTypes.Where(a => a.IsPublic))
            {
                if (candidate.TryGetTypeOfGenericInterface(typeof(ICustomJsonFormatter<>), out _))
                {
                    continue; // if it's a custom formatter, we skip it
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

            if (type.TryGetTypeOfGenericInterface(typeof(IReadOnlyDictionary<,>), out var rodictArgumentTypes))
            {
                return typeof(Dictionary<,>).MakeGenericType(rodictArgumentTypes);
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
            if (typeof(T) == typeof(TConverted))
            {
                return arg => Unsafe.As<T, TConverted>(ref arg);
            }
            var paramExpression = Expression.Parameter(inputType, "input");
            if (convertedType.IsAssignableFrom(inputType))
            {
                return Expression.Lambda<Func<T, TConverted>>(Expression.Convert(paramExpression, convertedType), paramExpression).Compile();
            }

            if (IsUnsupportedEnumerable(convertedType))
            {
                return _ => throw new NotSupportedException($"{typeof(TConverted).Name} is not supported.");
            }

            // not a nice way, but I don't find another good way to solve this, without adding either a dependency to immutable collection
            // or another nuget package and plugin code.
            if (convertedType.Namespace == "System.Collections.Immutable")
            {
                var emptyField = convertedType.GetField("Empty", BindingFlags.Public | BindingFlags.Static);
                var addRangeMethod = convertedType.GetMethods(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(a =>
                    a.Name == "AddRange" && a.GetParameters().Length == 1 && a.GetParameters().Single().ParameterType.IsAssignableFrom(paramExpression.Type));
                if (emptyField == null || addRangeMethod == null)
                {
                    return _ => throw new NotSupportedException($"{typeof(TConverted).Name} has no supported Immutable Collections (Immutable.Empty.AddRange) pattern.");
                }

                return Expression.Lambda<Func<T, TConverted>>(Expression.Call(Expression.Field(null, emptyField), addRangeMethod, paramExpression),
                    paramExpression).Compile();
            }

            if (convertedType.IsInterface)
            {
                convertedType = GetFunctorFallBackType(convertedType);
                if (convertedType == null)
                {
                    return _ => throw new NotSupportedException($"Can't convert {typeof(T).Name} to {typeof(TConverted).Name}.");
                }
            }

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