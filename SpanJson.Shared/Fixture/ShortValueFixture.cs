using System;

namespace SpanJson.Shared.Fixture
{
    public class ShortValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(short);

        public object Generate()
        {
            return (short) _prng.Next(short.MinValue, short.MaxValue);
        }
    }
}