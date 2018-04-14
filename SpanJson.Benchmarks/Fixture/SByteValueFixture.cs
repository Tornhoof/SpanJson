using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class SByteValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(sbyte);

        public object Generate()
        {
            return (sbyte) (_prng.Next() & 0xFF);
        }
    }
}