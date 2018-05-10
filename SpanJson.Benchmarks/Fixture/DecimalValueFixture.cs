using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class DecimalValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(decimal);

        public object Generate()
        {
            return _prng.Next(int.MinValue, int.MaxValue) + 0.66m;
        }
    }
}