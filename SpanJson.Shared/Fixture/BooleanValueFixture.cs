using System;

namespace SpanJson.Shared.Fixture
{
    public class BooleanValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(bool);

        public BooleanValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return _prng.Next() % 2 == 1;
        }
    }
}