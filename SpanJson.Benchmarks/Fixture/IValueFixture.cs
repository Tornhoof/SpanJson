using System;

namespace SpanJson.Benchmarks.Fixture
{
    public interface IValueFixture
    {
        Type Type { get; }
        object Generate();
    }
}