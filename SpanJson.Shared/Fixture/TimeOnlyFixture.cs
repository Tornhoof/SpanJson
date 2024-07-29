using System;

namespace SpanJson.Shared.Fixture
{
#if NET6_0_OR_GREATER
    public class TimeOnlyFixture : IValueFixture
    {
        private long _lastValue;
        private const long StartValue = TimeSpan.TicksPerHour;
        public Type Type { get; } = typeof(TimeOnly);

        public object Generate()
        {
            _lastValue += TimeSpan.TicksPerMinute * 30 + TimeSpan.TicksPerMillisecond * 555;
            var next = StartValue + _lastValue;
            if (next > TimeOnly.MaxValue.Ticks)
            {
                _lastValue = StartValue;
                next = _lastValue;
            }
            return new TimeOnly(next);
        }
    }
#endif
}