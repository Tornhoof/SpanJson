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
            if (type.IsInterface)
            {
                type = defaultType;
                if (type == null)
                {
                    return () => throw new NotSupportedException($"Can't create {defaultType.Name}");
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
        ///     I don't know why this is faster than SequenceEqual for this case
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static bool SymbolSequenceEquals<TSymbol>(in ReadOnlySpan<TSymbol> input, TSymbol[] comparison) where TSymbol : struct, IEquatable<TSymbol>
        {
            if (input.Length != comparison.Length)
            {
                return false;
            }

            for (var i = 0; i < input.Length; i++)
            {
                ref readonly var lhs = ref input[i];
                ref readonly var rhs = ref comparison[i];
                if (!lhs.Equals(rhs))
                {
                    return false;
                }
            }

            return true;
            //   return input.SequenceEqual(comparison);
        }

        protected static ConstantExpression GetConstantExpressionOfString<TSymbol>(string input)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return Expression.Constant(input.ToCharArray());
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return Expression.Constant(Encoding.UTF8.GetBytes(input));
            }

            throw new NotSupportedException();
        }

        /// <summary>
        ///     Couldn't get it working with Expression Trees,ref return lvalues do not work yet
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static TSymbol GetSymbol<TSymbol>(ReadOnlySpan<TSymbol> span, int index)
        {
            return span[index];
        }
    }
}