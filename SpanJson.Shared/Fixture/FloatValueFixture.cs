using System;

namespace SpanJson.Shared.Fixture
{
    public class FloatValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(float);

        public FloatValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return _prng.Next(short.MinValue, short.MaxValue) + 0.5f;
        }
    }
}