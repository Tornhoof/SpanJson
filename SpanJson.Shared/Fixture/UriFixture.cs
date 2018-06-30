using System;

namespace SpanJson.Shared.Fixture
{
    public class UriFixture : IValueFixture
    {
        private readonly StringValueFixture _stringValueFixture = new StringValueFixture();
        public Type Type { get; } = typeof(Uri);

        public object Generate()
        {
            return new Uri($"http://{_stringValueFixture.GenerateAlphaNumeric()}.com/{_stringValueFixture.GenerateAlphaNumeric()}");
        }
    }
}