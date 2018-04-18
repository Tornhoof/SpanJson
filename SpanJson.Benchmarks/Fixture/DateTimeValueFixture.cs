using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class DateTimeValueFixture : IValueFixture
    {
        private long _lastValue = new DateTime(1970, 1, 1, 0, 0, 0).Ticks;
        private long _increment = TimeSpan.TicksPerMillisecond * 500;
        public Type Type { get; } = typeof(DateTime);

        public void Increment(long increment)
        {
            _increment = increment;
        }

        public object Generate()
        {
            _lastValue += _increment;
            var dt = new DateTime(_lastValue, DateTimeKind.Utc);
            return dt;
        }
    }
}