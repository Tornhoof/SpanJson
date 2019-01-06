using System;

namespace SpanJson.Shared.Fixture
{
    public class ShortValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(short);

        public ShortValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return (short) _prng.Next(short.MinValue, short.MaxValue);
        }
    }
}