using System;

namespace SpanJson.Shared.Fixture
{
    public class UshortValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(ushort);

        public UshortValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return (ushort) (_prng.Next() & 0xFFFF);
        }
    }
}