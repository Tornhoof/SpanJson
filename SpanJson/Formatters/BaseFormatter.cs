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
        /// Faster than SequenceEqual
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
        /// Faster than SequenceEqual, this needs to be a byte array and not a string otherwise we might run into problems with non ascii property names
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

    }
}