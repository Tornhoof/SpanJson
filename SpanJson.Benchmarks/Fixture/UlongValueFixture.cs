using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class UlongValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(ulong);

        public object Generate()
        {
            return ((ulong) _prng.Next() << 32) | (uint) _prng.Next();
        }
    }
}