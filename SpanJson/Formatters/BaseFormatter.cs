using System;
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

        protected static MethodInfo FindPublicInstanceMethod(Type type, string name, params Type[] args)
        {
            return args?.Length > 0 ? type.GetMethod(name, args) : type.GetMethod(name);
        }


        protected static MethodInfo FindHelperMethod(string name, params Type[] args)
        {
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static;
            return args?.Length > 0
                ? typeof(BaseFormatter).GetMethod(name, flags, null, CallingConventions.Any, args, null)
                : typeof(BaseFormatter).GetMethod(name, flags);
        }


        /// <summary>
        ///     Faster than SequenceEqual for some unknown reason
        ///     Using SequenceEqual makes the deserialization 10-15% slower, but in a standalone benchmark sequenceEqual is way
        ///     faster
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool StringEquals(ReadOnlySpan<char> span, int offset, string comparison)
        {
            if (span.Length - offset != comparison.Length)
            {
                return false;
            }

            for (var i = 0; i < comparison.Length; i++)
            {
                ref readonly var left = ref span[offset + i];
                if (comparison[i] != left)
                {
                    return false;
                }
            }

            return true;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool SwitchStringEquals(ReadOnlySpan<char> span, string comparison)
        {
            return StringEquals(span, 0, comparison);
        }

        /// <summary>
        ///     Faster than SequenceEqual for some unknown reason, this needs to be a byte array and not a string otherwise we
        ///     might run into problems with non ascii property names
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool ByteEquals(ReadOnlySpan<byte> span, int offset, byte[] comparison)
        {
            if (span.Length - offset != comparison.Length)
            {
                return false;
            }

            for (var i = 0; i < comparison.Length; i++)
            {
                ref readonly var left = ref span[offset + i];
                if (comparison[i] != left)
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool SwitchByteEquals(ReadOnlySpan<byte> span, byte[] comparison)
        {
            return ByteEquals(span, 0, comparison);
        }

        protected static ConstantExpression GetConstantExpressionOfString<TSymbol>(string input)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return Expression.Constant(input);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return Expression.Constant(Encoding.UTF8.GetBytes(input));
            }

            throw new NotSupportedException();
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