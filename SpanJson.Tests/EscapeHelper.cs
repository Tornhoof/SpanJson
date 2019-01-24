using System;
using System.Collections.Generic;
using System.Text;

namespace SpanJson.Tests
{
    public static class EscapeHelper
    {
        public static string FullyEscape(string serialized)
        {
            StringBuilder sb = new StringBuilder();
            int from = 0;
            int index = 0;
            while (index < serialized.Length)
            {
                var c = serialized[index++];
                if (c > 0x7F)
                {
                    sb.Append(@"\u");
                    sb.Append(((uint)c).ToString("X4"));
                }
                else
                {
                    sb.Append(serialized.AsSpan(from, index - from));
                }
                from = index;
            }

            return sb.ToString();
        }
    }
}
