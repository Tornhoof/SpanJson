using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class UintValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(uint);

        public object Generate()
        {
            return (uint) _prng.Next();
        }
    }
}