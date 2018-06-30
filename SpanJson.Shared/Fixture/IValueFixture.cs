using System;

namespace SpanJson.Shared.Fixture
{
    public interface IValueFixture
    {
        Type Type { get; }
        object Generate();
    }
}