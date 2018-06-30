using System;

namespace SpanJson.Shared.Fixture
{
    public class SByteValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(sbyte);

        public object Generate()
        {
            return (sbyte) _prng.Next(sbyte.MinValue, sbyte.MaxValue);
        }
    }
}