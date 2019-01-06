using System;

namespace SpanJson.Shared.Fixture
{
    public class ByteValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(byte);

        public ByteValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public object Generate()
        {
            return (byte) (_prng.Next() & 0xFF);
        }
    }
}