using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace SpanJson.Formatters.Dynamic
{
    public sealed class SpanJsonDynamicArray<TSymbol> : DynamicObject, ISpanJsonDynamicArray where TSymbol : struct
    {
        private static readonly ConcurrentDictionary<Type, Func<object[], ICountableEnumerable>> Enumerables =
            new ConcurrentDictionary<Type, Func<object[], ICountableEnumerable>>();

        private readonly object[] _input;

        internal SpanJsonDynamicArray(object[] input)
        {
            _input = input;
        }

        [IgnoreDataMember]
        public object this[int index] => _input[index];

        [IgnoreDataMember]
        public int Length => _input.Length;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Always works
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
            var returnType = binder.ReturnType;
            if (returnType.IsArray)
            {
                // ReSharper disable ConvertClosureToMethodGroup
                var functor = Enumerables.GetOrAdd(returnType.GetElementType(), x => CreateEnumerable(x));
                // ReSharper restore ConvertClosureToMethodGroup
                var enumerable = functor(_input);
                var array = Array.CreateInstance(returnType.GetElementType(), enumerable.Count);
                var index = 0;
                foreach (var value in enumerable)
                {
                    array.SetValue(value, index++);
                }

                result = array;
                return true;
            }

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                // ReSharper disable ConvertClosureToMethodGroup
                var enumerable = Enumerables.GetOrAdd(returnType.GetGenericArguments()[0], x => CreateEnumerable(x));
                // ReSharper restore ConvertClosureToMethodGroup
                result = enumerable(_input);
                return true;
            }

            result = default;
            return false;
        }

        public override string ToString()
        {
            return $"[{string.Join(",", _input.Select(a => a == null ? "null" : a.ToJsonValue()))}]";
        }

        public string ToJsonValue() => ToString();

        private static Func<object[], ICountableEnumerable> CreateEnumerable(Type type)
        {
            var ctor = typeof(Enumerable<>).MakeGenericType(typeof(TSymbol), type).GetConstructor(new[] {typeof(object[])});
            var paramExpression = Expression.Parameter(typeof(object[]), "input");
            var lambda =
                Expression.Lambda<Func<object[], ICountableEnumerable>>(
                    Expression.Convert(Expression.New(ctor, paramExpression), typeof(ICountableEnumerable)),
                    paramExpression);
            return lambda.Compile();
        }

        private readonly struct Enumerable<TOutput> : IReadOnlyCollection<TOutput>, ICountableEnumerable
        {
            private readonly object[] _input;

            public Enumerable(object[] input)
            {
                _input = input;
                Count = _input.Length;
            }

            public IEnumerator<TOutput> GetEnumerator()
            {
                if (typeof(TOutput) == typeof(bool) || typeof(TOutput) == typeof(bool?))
                {
                    return BoolEnumerator();
                }

                return EnumeratorFactory.Create<TOutput>(_input);
            }

            private IEnumerator<TOutput> BoolEnumerator()
            {
                for (var i = 0; i < _input.Length; i++)
                {
                    yield return (TOutput) _input[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count { get; }
        }

        private struct Enumerator<TConverter, TOutput> : IEnumerator<TOutput> where TConverter : TypeConverter
        {
            private readonly TConverter _converter;
            private readonly object[] _input;
            private readonly int _length;
            private int _index;

            public Enumerator(TConverter converter, object[] input)
            {
                _converter = converter;
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

                var value = _input[_index++];
                Current = (TOutput) _converter.ConvertTo(value, typeof(TOutput));
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

        private static class EnumeratorFactory
        {
            private static readonly SpanJsonDynamicNumber<TSymbol>.DynamicTypeConverter NumberTypeConverter =
                new SpanJsonDynamicNumber<TSymbol>.DynamicTypeConverter();

            private static readonly SpanJsonDynamicString<TSymbol>.DynamicTypeConverter StringTypeConverter =
                new SpanJsonDynamicString<TSymbol>.DynamicTypeConverter();

            public static IEnumerator<TOutput> Create<TOutput>(object[] input)
            {
                var type = typeof(TOutput);
                if (StringTypeConverter.IsSupported(type))
                {
                    return new Enumerator<SpanJsonDynamicString<TSymbol>.DynamicTypeConverter, TOutput>(StringTypeConverter, input);
                }

                if (NumberTypeConverter.IsSupported(type))
                {
                    return new Enumerator<SpanJsonDynamicNumber<TSymbol>.DynamicTypeConverter, TOutput>(NumberTypeConverter, input);
                }

                return null;
            }
        }

        private interface ICountableEnumerable : IEnumerable
        {
            int Count { get; }
        }
    }
}