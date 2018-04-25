using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class DateTimeValueFixture : IValueFixture
    {
        private long _increment = TimeSpan.TicksPerMillisecond * 500;
        private long _lastValue = new DateTime(1970, 1, 1, 0, 0, 0).Ticks;
        public Type Type { get; } = typeof(DateTime);

        public object Generate()
        {
            _lastValue += _increment;
            var dt = new DateTime(_lastValue, DateTimeKind.Utc);
            return dt;
        }

        public void Increment(long increment)
        {
            _increment = increment;
        }
    }
}