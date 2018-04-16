using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace SpanJson.Helpers
{
    public sealed class SpanJsonDynamicArray : DynamicObject, IReadOnlyList<object>
    {
        private readonly object[] _input;

        internal SpanJsonDynamicArray(object[] input)
        {
            _input = input;
        }


        public IEnumerator<object> GetEnumerator()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                yield return _input[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _input.Length;

        public object this[int index] => _input[index];
    }
}