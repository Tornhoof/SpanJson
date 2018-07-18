using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using SpanJson.Resolvers;

namespace SpanJson.Helpers
{
    public static class MemberComparisonBuilder
    {
        private static int GetSymbolSize<TSymbol>() where TSymbol : struct
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return sizeof(char);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return sizeof(byte);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// This method builds a chain of if statements  of the following logic:
        /// if(length == x AND ReadInteger(span) == y AND ...) then assign value
        /// the ReadInteger comparisons are basically constant values of the encoded value
        /// UTF8 -> 8 chars -> 8 bytes
        /// UTF16 -> 4 chars -> 8 bytes
        /// etc.
        /// This is a very fast way to match the correct namespan for e.g assigning members
        /// as it's only a comparison of length (this excludes most of the values) and
        /// comparing the individual integer length parts of the name against constants
        /// This improves the performance in deserialization specifically for UTF8 as byte arrays
        /// are handled differently than strings for comparisons in expression trees
        /// The speed for both is practically the same with this method
        /// It also simplifies the code if the member name is actually multibyte utf8 (e.g. chinese)
        /// </summary>
        public static Expression Build<TSymbol>(List<JsonMemberInfo> memberInfos, int index, ParameterExpression lengthParameter,
            ParameterExpression nameSpanExpression, LabelTarget endOfBlockLabel,
            Func<JsonMemberInfo, Expression> matchExpressionFunctor) where TSymbol : struct
        {
            var symbolSize = GetSymbolSize<TSymbol>();
            var grouping = memberInfos.Where(a => a.CanRead && GetLength<TSymbol>(a.Name) >= index).GroupBy(a => CalculateKey<TSymbol>(a.Name, index))
                .OrderByDescending(a => a.Key.Key).ToList();
            if (!grouping.Any())
            {
                throw new InvalidOperationException(); // should never happen
            }

            var expressions = new List<Expression>();
            foreach (var group in grouping)
            {
                if (group.Count() == 1) // need to check remaining values too 
                {
                    var memberInfo = group.Single();
                    var length = index + group.Key.offset;
                    var matchExpression = matchExpressionFunctor(memberInfo);
                    matchExpression = Expression.Block(matchExpression, Expression.Goto(endOfBlockLabel));
                    Expression comparisonExpression = null;
                    if (group.Key.Key != 0 || group.Key.offset != 0)
                    {
                        comparisonExpression = Expression.Equal(GetReadMethod(nameSpanExpression, group.Key.intType, Expression.Constant(index * symbolSize)),
                            GetConstantExpressionForGroupKey(group.Key.Key, group.Key.intType));
                        var nameLength = GetLength<TSymbol>(memberInfo.Name);
                        while (length < nameLength)
                        {
                            var subKey = CalculateKey<TSymbol>(memberInfo.Name, length);
                            comparisonExpression = Expression.AndAlso(comparisonExpression,
                                Expression.Equal(GetReadMethod(nameSpanExpression, subKey.intType, Expression.Constant(length * symbolSize)),
                                    GetConstantExpressionForGroupKey(subKey.Key, subKey.intType)));
                            length += subKey.offset;
                        }
                    }

                    var lengthExpression = Expression.Equal(lengthParameter, Expression.Constant(length));
                    var ifExpression = comparisonExpression == null ? lengthExpression : Expression.AndAlso(lengthExpression, comparisonExpression);
                    expressions.Add(Expression.IfThen(ifExpression, matchExpression));
                }
                else
                {
                    var nextLength = index + group.Key.offset;
                    var ifExpression = Expression.AndAlso(Expression.GreaterThanOrEqual(lengthParameter, Expression.Constant(nextLength)),
                        Expression.Equal(GetReadMethod(nameSpanExpression, group.Key.intType, Expression.Constant(index * symbolSize)),
                            GetConstantExpressionForGroupKey(group.Key.Key, group.Key.intType)));
                    var subBlock = Build<TSymbol>(group.ToList(), nextLength, lengthParameter, nameSpanExpression,
                        endOfBlockLabel, matchExpressionFunctor);
                    expressions.Add(Expression.IfThen(ifExpression, subBlock));
                }
            }

            return Expression.Block(expressions);
        }

        private static int GetLength<TSymbol>(string name)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return name.Length;
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return Encoding.UTF8.GetByteCount(name);
            }

            throw new NotSupportedException();
        }

        private static Expression GetConstantExpressionForGroupKey(ulong key, Type intType)
        {
            if (intType == typeof(byte))
            {
                return Expression.Constant((byte) key);
            }

            if (intType == typeof(ushort))
            {
                return Expression.Constant((ushort) key);
            }

            if (intType == typeof(uint))
            {
                return Expression.Constant((uint) key);
            }

            if (intType == typeof(ulong))
            {
                return Expression.Constant(key);
            }

            throw new NotSupportedException();
        }

        private static Expression GetReadMethod(ParameterExpression nameSpanExpression, Type intType, Expression offsetParameter)
        {
            string methodName;
            if (intType == typeof(byte))
            {
                methodName = nameof(SpanHelper.ReadByte);
            }

            else if (intType == typeof(ushort))
            {
                methodName = nameof(SpanHelper.ReadUInt16);
            }

            else if (intType == typeof(uint))
            {
                methodName = nameof(SpanHelper.ReadUInt32);
            }

            else if (intType == typeof(ulong))
            {
                methodName = nameof(SpanHelper.ReadUInt64);
            }
            else
            {
                throw new NotSupportedException();
            }

            var methodInfo = typeof(SpanHelper).GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            return Expression.Call(methodInfo, nameSpanExpression, offsetParameter);
        }

        private static (ulong Key, Type intType, int offset) CalculateKey<TSymbol>(string memberName, int index)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return CalculateKeyUtf16(memberName, index);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return CalculateKeyUtf8(memberName, index);
            }

            throw new NotSupportedException();
        }

        private static (ulong Key, Type intType, int offset) CalculateKeyUtf8(string memberName, int index)
        {
            int remaining = GetLength<byte>(memberName) - index;
            if (remaining >= 8)
            {
                return (BitConverter.ToUInt64(Encoding.UTF8.GetBytes(memberName), index), typeof(ulong), 8);
            }

            if (remaining >= 4)
            {
                return (BitConverter.ToUInt32(Encoding.UTF8.GetBytes(memberName), index), typeof(uint), 4);
            }

            if (remaining >= 2)
            {
                return (BitConverter.ToUInt16(Encoding.UTF8.GetBytes(memberName), index), typeof(ushort), 2);
            }

            if (remaining >= 1)
            {
                return (Encoding.UTF8.GetBytes(memberName)[index], typeof(byte), 1);
            }

            return (0, typeof(uint), 0);
        }

        private static (ulong Key, Type intType, int offset) CalculateKeyUtf16(string memberName, int index)
        {
            int remaining = GetLength<char>(memberName) - index;
            if (remaining >= 4)
            {
                return (BitConverter.ToUInt64(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(ulong), 4);
            }

            if (remaining >= 2)
            {
                return (BitConverter.ToUInt32(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(uint), 2);
            }

            if (remaining >= 1)
            {
                return (BitConverter.ToUInt16(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(ushort), 1);
            }

            return (0, typeof(uint), 0);
        }
    }
}