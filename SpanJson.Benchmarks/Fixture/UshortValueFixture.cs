using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class UshortValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(ushort);

        public object Generate()
        {
            return (ushort) (_prng.Next() & 0xFFFF);
        }
    }
}