using System;

namespace SpanJson.Shared.Fixture
{
    public class TimespanFixture : IValueFixture
    {
        private long _lastValue;
        private const long StartValue = -6 * TimeSpan.TicksPerHour;
        public Type Type { get; } = typeof(TimeSpan);

        public object Generate()
        {
            _lastValue += TimeSpan.TicksPerMinute * 30 + TimeSpan.TicksPerMillisecond * 555;
            return TimeSpan.FromTicks(StartValue + _lastValue);
        }
    }
}