using System;

namespace SpanJson.Shared.Fixture
{
    public class IntValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(int);

        public IntValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return _prng.Next(int.MinValue, int.MaxValue);
        }
    }
}