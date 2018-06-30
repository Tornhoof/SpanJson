using System;

namespace SpanJson.Shared.Fixture
{
    public class DoubleValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(double);

        public object Generate()
        {
            return _prng.Next(int.MinValue, int.MaxValue) + 0.5d;
        }
    }
}