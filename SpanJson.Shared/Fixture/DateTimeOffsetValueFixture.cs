using System;

namespace SpanJson.Shared.Fixture
{
    public class DateTimeOffsetValueFixture : IValueFixture
    {
        private long _lastValue;
        public Type Type { get; } = typeof(DateTimeOffset);

        public object Generate()
        {
            _lastValue += TimeSpan.TicksPerMillisecond * 500;
            return DateTimeOffset.FromUnixTimeMilliseconds(_lastValue);
        }
    }
}