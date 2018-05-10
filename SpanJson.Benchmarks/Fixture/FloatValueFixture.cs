using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class FloatValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(float);

        public object Generate()
        {
            return _prng.Next(short.MinValue, short.MaxValue) + 0.5f;
        }
    }
}