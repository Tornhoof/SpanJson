using System;

namespace SpanJson.Shared.Fixture
{
    public class LongValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(long);

        public LongValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return ((long) _prng.Next(int.MinValue, int.MaxValue) << 32) | (uint) _prng.Next();
        }
    }
}