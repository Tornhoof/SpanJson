using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class CharValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(char);

        public object Generate()
        {
            return (char)(_prng.Next() & 0xFFFF);
        }
    }
}