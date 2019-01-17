using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson.Helpers
{
    public static class SpanHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReadByte(in ReadOnlySpan<byte> span, int offset)
        {
            return Unsafe.Add(ref MemoryMarshal.GetReference(span), offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUInt16(in ReadOnlySpan<byte> span, int offset)
        {
            return Unsafe.ReadUnaligned<ushort>(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUInt32(in ReadOnlySpan<byte> span, int offset)
        {
            return Unsafe.ReadUnaligned<uint>(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadUInt64(in ReadOnlySpan<byte> span, int offset)
        {
            return Unsafe.ReadUnaligned<ulong>(ref Unsafe.Add(ref MemoryMarshal.GetReference(span), offset));
        }
    }
}