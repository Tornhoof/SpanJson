using System;

namespace SpanJson.Shared.Fixture
{
    public class DecimalValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(decimal);

        public DecimalValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return _prng.Next(int.MinValue, int.MaxValue) + 0.66m;
        }
    }
}