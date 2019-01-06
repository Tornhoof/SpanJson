using System;

namespace SpanJson.Shared.Fixture
{
    public class DoubleValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(double);

        public DoubleValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return _prng.Next(int.MinValue, int.MaxValue) + 0.5d;
        }
    }
}