using System;
using System.Runtime.CompilerServices;

namespace SpanJson.Helpers
{
    public static class EnumFlagHelpers
    {
        /// <summary>
        /// This is OR
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEnumBase Combine<TEnumBase>(TEnumBase a, TEnumBase b) where TEnumBase : struct
        {
            if (typeof(TEnumBase) == typeof(sbyte))
            {
                var aConv = Unsafe.As<TEnumBase, sbyte>(ref a);
                var bConv = Unsafe.As<TEnumBase, sbyte>(ref b);
                var result = (sbyte)(aConv | bConv);
                return Unsafe.As<sbyte, TEnumBase>(ref result);
            }
            if (typeof(TEnumBase) == typeof(byte))
            {
                var aConv = Unsafe.As<TEnumBase, byte>(ref a);
                var bConv = Unsafe.As<TEnumBase, byte>(ref b);
                var result = (byte)(aConv | bConv);
                return Unsafe.As<byte, TEnumBase>(ref result);
            }
            if (typeof(TEnumBase) == typeof(short))
            {
                var aConv = Unsafe.As<TEnumBase, short>(ref a);
                var bConv = Unsafe.As<TEnumBase, short>(ref b);
                var result = (short)(aConv | bConv);
                return Unsafe.As<short, TEnumBase>(ref result);
            }
            if (typeof(TEnumBase) == typeof(ushort))
            {
                var aConv = Unsafe.As<TEnumBase, ushort>(ref a);
                var bConv = Unsafe.As<TEnumBase, ushort>(ref b);
                var result = (ushort)(aConv | bConv);
                return Unsafe.As<ushort, TEnumBase>(ref result);
            }
            if (typeof(TEnumBase) == typeof(int))
            {
                var aConv = Unsafe.As<TEnumBase, int>(ref a);
                var bConv = Unsafe.As<TEnumBase, int>(ref b);
                var result = (int)(aConv | bConv);
                return Unsafe.As<int, TEnumBase>(ref result);
            }
            if (typeof(TEnumBase) == typeof(uint))
            {
                var aConv = Unsafe.As<TEnumBase, uint>(ref a);
                var bConv = Unsafe.As<TEnumBase, uint>(ref b);
                var result = (uint)(aConv | bConv);
                return Unsafe.As<uint, TEnumBase>(ref result);
            }
            if (typeof(TEnumBase) == typeof(long))
            {
                var aConv = Unsafe.As<TEnumBase, long>(ref a);
                var bConv = Unsafe.As<TEnumBase, long>(ref b);
                var result = (long)(aConv | bConv);
                return Unsafe.As<long, TEnumBase>(ref result);
            }
            if (typeof(TEnumBase) == typeof(ulong))
            {
                var aConv = Unsafe.As<TEnumBase, ulong>(ref a);
                var bConv = Unsafe.As<TEnumBase, ulong>(ref b);
                var result = (ulong)(aConv | bConv);
                return Unsafe.As<ulong, TEnumBase>(ref result);
            }

            return default;
        }

        /// <summary>
        /// This is AND
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFlag<TEnum, TEnumBase>(TEnum a, TEnum b) where TEnumBase : struct
        {
            if (typeof(TEnumBase) == typeof(sbyte))
            {
                var aConv = Unsafe.As<TEnum, sbyte>(ref a);
                var bConv = Unsafe.As<TEnum, sbyte>(ref b);
                return (aConv & bConv) == bConv;
            }
            if (typeof(TEnumBase) == typeof(byte))
            {
                var aConv = Unsafe.As<TEnum, byte>(ref a);
                var bConv = Unsafe.As<TEnum, byte>(ref b);
                return (aConv & bConv) == bConv;
            }
            if (typeof(TEnumBase) == typeof(short))
            {
                var aConv = Unsafe.As<TEnum, short>(ref a);
                var bConv = Unsafe.As<TEnum, short>(ref b);
                return (aConv & bConv) == bConv;
            }
            if (typeof(TEnumBase) == typeof(ushort))
            {
                var aConv = Unsafe.As<TEnum, ushort>(ref a);
                var bConv = Unsafe.As<TEnum, ushort>(ref b);
                return (aConv & bConv) == bConv;
            }
            if (typeof(TEnumBase) == typeof(int))
            {
                var aConv = Unsafe.As<TEnum, int>(ref a);
                var bConv = Unsafe.As<TEnum, int>(ref b);
                return (aConv & bConv) == bConv;
            }
            if (typeof(TEnumBase) == typeof(uint))
            {
                var aConv = Unsafe.As<TEnum, uint>(ref a);
                var bConv = Unsafe.As<TEnum, uint>(ref b);
                return (aConv & bConv) == bConv;
            }
            if (typeof(TEnumBase) == typeof(long))
            {
                var aConv = Unsafe.As<TEnum, long>(ref a);
                var bConv = Unsafe.As<TEnum, long>(ref b);
                return (aConv & bConv) == bConv;
            }
            if (typeof(TEnumBase) == typeof(ulong))
            {
                var aConv = Unsafe.As<TEnum, ulong>(ref a);
                var bConv = Unsafe.As<TEnum, ulong>(ref b);
                return (aConv & bConv) == bConv;
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }
    }
}
