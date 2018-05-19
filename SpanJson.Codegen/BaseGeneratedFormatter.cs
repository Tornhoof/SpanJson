using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanJson.Codegen
{
    public abstract class BaseGeneratedFormatter<T, TSymbol, TResolver> where T : class where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ushort ReadByte(ref byte b, int offset)
        {
            return Unsafe.Add(ref b, offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ushort ReadUInt16(ref byte b, int offset)
        {
            return Unsafe.ReadUnaligned<ushort>(ref Unsafe.Add(ref b, offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static uint ReadUInt32(ref byte b, int offset)
        {
            return Unsafe.ReadUnaligned<uint>(ref Unsafe.Add(ref b, offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ulong ReadUInt64(ref byte b, int offset)
        {
            return Unsafe.ReadUnaligned<ulong>(ref Unsafe.Add(ref b, offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ushort ReadUInt16(ref char c, int offset)
        {
            return Unsafe.ReadUnaligned<ushort>(ref Unsafe.As<char, byte>(ref Unsafe.Add(ref c, offset)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static uint ReadUInt32(ref char c, int offset)
        {
            return Unsafe.ReadUnaligned<uint>(ref Unsafe.As<char, byte>(ref Unsafe.Add(ref c, offset)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ulong ReadUInt64(ref char c, int offset)
        {
            return Unsafe.ReadUnaligned<ulong>(ref Unsafe.As<char, byte>(ref Unsafe.Add(ref c, offset)));
        }
    }
}
