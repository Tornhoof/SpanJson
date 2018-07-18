using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanJson.Formatters
{
    public abstract class BaseFormatter
    {
        protected static Func<T> BuildCreateFunctor<T>(Type defaultType)
        {
            var type = typeof(T);
            var ci = type.GetConstructor(Type.EmptyTypes);
            if (type.IsInterface || ci == null)
            {
                type = defaultType;
                if (type == null)
                {
                    return () => throw new NotSupportedException($"Can't create {typeof(T).Name}.");
                }
            }

            return Expression.Lambda<Func<T>>(Expression.New(type)).Compile();
        }

        protected static Func<T, TReadOnly> BuildConvertFunctor<T, TReadOnly>()
        {
            if (typeof(TReadOnly).IsAssignableFrom(typeof(T)))
            {
                var paramExpression = Expression.Parameter(typeof(T), "input");
                var lambda = Expression.Lambda<Func<T, TReadOnly>>(Expression.Convert(paramExpression, typeof(TReadOnly)), paramExpression);
                return lambda.Compile();
            }
            else if (typeof(TReadOnly).IsInterface)
            {
                return _ => throw new NotSupportedException($"Can't convert {typeof(T).Name} to {typeof(TReadOnly).Name}.");
            }
            else
            {
                var ci = typeof(TReadOnly).GetConstructors().FirstOrDefault(a =>
                    a.GetParameters().Length == 1 && a.GetParameters().Single().ParameterType.IsAssignableFrom(typeof(T)));
                if (ci == null)
                {
                    return _ => throw new NotSupportedException($"No constructor of {typeof(TReadOnly).Name} accepts {typeof(T).Name}.");
                }

                var paramExpression = Expression.Parameter(typeof(T), "input");
                var lambda = Expression.Lambda<Func<T, TReadOnly>>(Expression.New(ci, paramExpression), paramExpression);
                return lambda.Compile();
            }
        }

        protected static MethodInfo FindPublicInstanceMethod(Type type, string name, params Type[] args)
        {
            return args?.Length > 0 ? type.GetMethod(name, args) : type.GetMethod(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref JsonWriter<TSymbol> writer,
            T value, IJsonFormatter<T, TSymbol> formatter,
            int nextNestingLimit)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            // The first check is get around the runtime check for primitive types an structs without references, i.e most of the blc types from the bclformatter.tt
            // Then we specifically check for string as it is very common
            // a null value can be serialized by both (doesn't matter, but we need to handle null for the runtime type check)
            // Checking for things like IsValueType and/or IsSealed is actually several times more expensive than the type comparison
            if (!RuntimeHelpers.IsReferenceOrContainsReferences<T>() || typeof(T) == typeof(string) || value == null || value.GetType() == typeof(T))
            {
                formatter.Serialize(ref writer, value, nextNestingLimit);
            }
            else
            {
                RuntimeFormatter<TSymbol, TResolver>.Default.Serialize(ref writer, value, nextNestingLimit);
            }
        }
    }
}