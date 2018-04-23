using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq.Expressions;

namespace SpanJson.Formatters.Dynamic
{
    public sealed class SpanJsonDynamicArray : DynamicObject, IReadOnlyList<object>
    {
        private static readonly ConcurrentDictionary<Type, Func<object[], IEnumerable>> Enumerables =
            new ConcurrentDictionary<Type, Func<object[], IEnumerable>>();

        private readonly object[] _input;

        internal SpanJsonDynamicArray(object[] input)
        {
            _input = input;
        }

        public int Count => _input.Length;

        public object this[int index] => _input[index];

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Always works
        /// </summary>
        public IEnumerator<object> GetEnumerator()
        {
            for (var i = 0; i < _input.Length; i++)
            {
                yield return _input[i];
            }
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            // ReSharper disable ConvertClosureToMethodGroup
            var enumerator = Enumerables.GetOrAdd(binder.ReturnType, x => CreateEnumerable(x));
            // ReSharper restore ConvertClosureToMethodGroup
            if (enumerator != null)
            {
                result = enumerator(_input);
                return true;
            }

            result = default;
            return false;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder.Name == "Length")
            {
                result = _input.Length;
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", _input)}]";
        }

        private static Func<object[], IEnumerable> CreateEnumerable(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var ctor = typeof(Enumerable<>).MakeGenericType(type.GetGenericArguments()[0]).GetConstructor(new[] {typeof(object[])});
                var paramExpression = Expression.Parameter(typeof(object[]), "input");
                var lambda =
                    Expression.Lambda<Func<object[], IEnumerable>>(
                        Expression.Convert(Expression.New(ctor, paramExpression), typeof(IEnumerable)),
                        paramExpression);
                return lambda.Compile();
            }

            return null;
        }

        private struct Enumerable<TOutput> : IEnumerable<TOutput>
        {
            private readonly object[] _input;

            public Enumerable(object[] input)
            {
                _input = input;
            }

            public IEnumerator<TOutput> GetEnumerator()
            {
                var stringConverter = new SpanJsonDynamicString.DynamicTypeConverter();
                if (stringConverter.IsSupported(typeof(TOutput)))
                {
                    return new Enumerator<SpanJsonDynamicString.DynamicTypeConverter, TOutput>(_input);
                }

                var numberConverter = new SpanJsonDynamicNumber.DynamicTypeConverter();
                if (numberConverter.IsSupported(typeof(TOutput)))
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

        private struct Enumerator<TConverter, TOutput> : IEnumerator<TOutput> where TConverter : TypeConverter, new()
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