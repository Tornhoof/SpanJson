using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class DateTimeValueFixture : IValueFixture
    {
        private static readonly long Offset = new DateTime(1970, 1, 1, 0, 0, 0).ToFileTime();
        private long _lastValue;
        public Type Type { get; } = typeof(DateTime);

        public object Generate()
        {
            _lastValue += 1000;
            var dt = DateTime.FromFileTime(_lastValue + Offset);
            return dt;
        }
    }
}