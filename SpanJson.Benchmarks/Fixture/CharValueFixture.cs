using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class CharValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        public Type Type { get; } = typeof(char);

        public object Generate()
        {
            char codePoint;
            do
            {
                codePoint = (char) (_prng.Next() & 0xFFFF);
            } while (codePoint >= 0xD800 && codePoint <= 0xDFFF);

            return codePoint;
        }
    }
}