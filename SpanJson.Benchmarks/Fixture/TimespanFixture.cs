using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class TimespanFixture : IValueFixture
    {
        private long _lastValue;
        public Type Type { get; } = typeof(TimeSpan);

        public object Generate()
        {
            _lastValue += 1000;
            return TimeSpan.FromTicks(_lastValue);
        }
    }
}