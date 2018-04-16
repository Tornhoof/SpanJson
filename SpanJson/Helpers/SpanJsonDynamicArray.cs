using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace SpanJson.Helpers
{
    public sealed class SpanJsonDynamicArray : DynamicObject, IReadOnlyList<object>
    {
        private readonly object[] _input;
        private static ConcurrentDictionary<Type, Func<object[], IEnumerable>> Enumerables = new ConcurrentDictionary<Type, Func<object[], IEnumerable>>();
        internal SpanJsonDynamicArray(object[] input)
        {
            _input = input;
        }

        /// <summary>
        ///  Always works
        /// </summary>
        public IEnumerator<object> GetEnumerator()
        {
            for (int i = 0; i < _input.Length; i++)
            {
                yield return _input[i];
            }
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var enumerator = GetOrAddEnumerable(binder.ReturnType);
            if (enumerator != null)
            {
                result = enumerator(_input);
                return true;
            }

            result = default;
            return false;
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", _input)}]";
        }

        private static Func<object[], IEnumerable> GetOrAddEnumerable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var ctor = typeof(Enumerable<>).MakeGenericType(type.GetGenericArguments()[0]).GetConstructor(new Type[] {typeof(object[])});
                var paramExpression = Expression.Parameter(typeof(object[]), "input");
                var lambda =
                    Expression.Lambda<Func<object[], IEnumerable>>(
                        Expression.Convert(Expression.New(ctor, paramExpression), typeof(IEnumerable)),
                        paramExpression);
                return lambda.Compile();
            }

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _input.Length;

        public object this[int index] => _input[index];

        struct Enumerable<TOutput> : IEnumerable<TOutput>
        {
            private readonly object[] _input;

            public Enumerable(object[] input)
            {
                _input = input;
            }
            public IEnumerator<TOutput> GetEnumerator()
            {
                if (SpanJsonDynamicString.DynamicTypeConverter.IsSupported(typeof(TOutput)))
                {
                    return new Enumerator<SpanJsonDynamicString.DynamicTypeConverter, TOutput>(_input);
                }
                if (SpanJsonDynamicNumber.DynamicTypeConverter.IsSupported(typeof(TOutput)))
                {
                    return new Enumerator<SpanJsonDynamicNumber.DynamicTypeConverter, TOutput>(_input);
                }

                return null;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        struct Enumerator<TConverter, TOutput> : IEnumerator<TOutput> where TConverter : TypeConverter, new()
        {
            private static readonly TConverter Converter = new TConverter();
            private readonly object[] _input;
            private readonly int _length;
            private int _index;

            public Enumerator(object[] input)
            {
                _input = input;
                _length = input.Length;
                _index = 0;
                Current = default;
            }

            public bool MoveNext()
            {
                if (_index >= _length)
                {
                    return false;
                }

                Current = (TOutput) Converter.ConvertTo(_input[_index++], typeof(TOutput));
                return true;
            }

            public void Reset()
            {
                _index = 0;
            }

            public TOutput Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }
        }
    }
}