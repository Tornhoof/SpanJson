using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;

namespace SpanJson.Helpers
{
    public static class FormatterUtils
    {
        private static readonly ulong[] Powers10 = new[]
        {
            1UL,
            10UL,
            100UL,
            1000UL,
            10000UL,
            100000UL,
            1000000UL,
            10000000UL,
            100000000UL,
            1000000000UL,
            10000000000UL,
            100000000000UL,
            1000000000000UL,
            10000000000000UL,
            100000000000000UL,
            1000000000000000UL,
            10000000000000000UL,
            100000000000000000UL,
            1000000000000000000UL,
            10000000000000000000UL
            //   1234567890123456789
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int BitCount(ulong x)
        {
            x -= (x >> 1) & 0x5555555555555555UL; //put count of each 2 bits into those 2 bits
            x = (x & 0x3333333333333333UL) +
                ((x >> 2) & 0x3333333333333333UL); //put count of each 4 bits into those 4 bits 
            x = (x + (x >> 4)) & 0x0F0F0F0F0F0F0F0FUL; //put count of each 8 bits into those 8 bits 
            return
                (int) ((x * 0x0101010101010101UL) >> 56); //returns left 8 bits of x + (x<<8) + (x<<16) + (x<<24) + ... 
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LeadingZeroCountSlow(ulong x)
        {
            x |= x >> 32;
            x |= x >> 16;
            x |= x >> 8;
            x |= x >> 4;
            x |= x >> 2;
            x |= x >> 1;

            return sizeof(ulong) * 8 - BitCount(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountDigitsLzCnt(ulong x)
        {
            if (x == 0)
            {
                return 1;
            }

            int log2Value;
            if (!Lzcnt.IsSupported || !Environment.Is64BitProcess)
            {
                log2Value = LeadingZeroCountSlow(x);
            }
            else
            {
                log2Value = (int) Lzcnt.LeadingZeroCount(x);
            }

            log2Value = sizeof(ulong) * 8 - 1 - log2Value;
            var log10Value = ((log2Value + 1) * 77) >> 8;
            var digits = log10Value + (x >= Powers10[log10Value] ? 1 : 0);
            return digits;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CountDigits(ulong value)
        {
            int digits = 1;
            uint part;
            if (value >= 10000000)
            {
                if (value >= 100000000000000)
                {
                    part = (uint)(value / 100000000000000);
                    digits += 14;
                }
                else
                {
                    part = (uint)(value / 10000000);
                    digits += 7;
                }
            }
            else
            {
                part = (uint)value;
            }

            if (part < 10)
            {
                // no-op
            }
            else if (part < 100)
            {
                digits += 1;
            }
            else if (part < 1000)
            {
                digits += 2;
            }
            else if (part < 10000)
            {
                digits += 3;
            }
            else if (part < 100000)
            {
                digits += 4;
            }
            else if (part < 1000000)
            {
                digits += 5;
            }
            else
            {
                Debug.Assert(part < 10000000);
                digits += 6;
            }

            return digits;
        }
    }
}
