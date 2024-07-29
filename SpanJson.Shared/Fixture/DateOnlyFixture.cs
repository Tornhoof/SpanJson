using System;

namespace SpanJson.Shared.Fixture
{
#if NET6_0_OR_GREATER
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
#endif
}