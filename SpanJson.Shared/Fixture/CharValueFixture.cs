using System;

namespace SpanJson.Shared.Fixture
{
    public class CharValueFixture : IValueFixture
    {
        private readonly Random _prng;
        public Type Type { get; } = typeof(char);

        public CharValueFixture(int? seed = null)
        {
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

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