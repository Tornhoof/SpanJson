using System;

namespace SpanJson.Shared.Fixture
{
    public class SByteValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(sbyte);

        public SByteValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return (sbyte) _prng.Next(sbyte.MinValue, sbyte.MaxValue);
        }
    }
}