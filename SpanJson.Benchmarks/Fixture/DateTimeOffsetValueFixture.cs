using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class DateTimeOffsetValueFixture : IValueFixture
    {
        private long _lastValue;
        public Type Type { get; } = typeof(DateTimeOffset);

        public object Generate()
        {
            _lastValue += TimeSpan.TicksPerSecond;
            return DateTimeOffset.FromUnixTimeMilliseconds(_lastValue);
        }
    }
}