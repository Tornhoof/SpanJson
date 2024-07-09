using System;

namespace SpanJson.Shared.Fixture
{
    public class DateOnlyFixture : IValueFixture
    {
        private DateOnly _lastValue = DateOnly.MinValue;
        public Type Type { get; } = typeof(DateOnly);

        public object Generate()
        {
            _lastValue = _lastValue.AddDays(1);
            return _lastValue;
        }
    }
}