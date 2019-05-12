using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpanJson.Tests
{
    public static class ReadOnlySequenceFactory<T> where T : struct
    {
        public static ReadOnlySequence<T> CreateSegments(params T[][] inputs) => CreateSegments(inputs.Select(input => (ReadOnlyMemory<T>) input.AsMemory()));

        public static ReadOnlySequence<T> CreateSegments(IEnumerable<ReadOnlyMemory<T>> inputs)
        {
            if (inputs == null || inputs.Count() == 0)
            {
                throw new InvalidOperationException();
            }

            BufferSegment<T> last = null;
            BufferSegment<T> first = null;
            foreach (ReadOnlyMemory<T> input in inputs)
            {
                int length = input.Length;
                int dataOffset = length / 2;

                Memory<T> memory = new Memory<T>(new T[length * 2], dataOffset, length);
                input.CopyTo(memory);

                if (first == null)
                {
                    first = new BufferSegment<T>(memory);
                    last = first;
                }
                else
                {
                    last = last.Append(memory);
                }
            }

            return new ReadOnlySequence<T>(first, 0, last, last.Memory.Length);

        }
    }

    internal class BufferSegment<T> : ReadOnlySequenceSegment<T>
    {
        public BufferSegment(ReadOnlyMemory<T> memory)
        {
            Memory = memory;
        }

        public BufferSegment<T> Append(ReadOnlyMemory<T> memory)
        {
            var segment = new BufferSegment<T>(memory)
            {
                RunningIndex = RunningIndex + Memory.Length
            };
            Next = segment;
            return segment;
        }
    }
}
