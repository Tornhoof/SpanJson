using System;

namespace SpanJson.Shared.Fixture
{
    public class UlongValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(ulong);


        public UlongValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return ((ulong) _prng.Next() << 32) | (uint) _prng.Next();
        }
    }
}