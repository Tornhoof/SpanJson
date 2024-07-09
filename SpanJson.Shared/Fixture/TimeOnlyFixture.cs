using System;

namespace SpanJson.Shared.Fixture
{
    public class TimeOnlyFixture : IValueFixture
    {
        private long _lastValue;
        private const long StartValue = -6 * TimeSpan.TicksPerHour;
        public Type Type { get; } = typeof(TimeOnly);

        public object Generate()
        {
            _lastValue += TimeSpan.TicksPerMinute * 30 + TimeSpan.TicksPerMillisecond * 555;
            return new TimeOnly(StartValue + _lastValue);
        }
    }
}