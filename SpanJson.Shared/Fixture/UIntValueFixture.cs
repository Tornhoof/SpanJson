using System;

namespace SpanJson.Shared.Fixture
{
    public class UintValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(uint);

        public UintValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }


        public object Generate()
        {
            return (uint) _prng.Next();
        }
    }
}