using System;

namespace SpanJson.Shared.Fixture
{
    public class VersionFixture : IValueFixture
    {
        private int _build;
        private int _majorVersion;
        private int _minorVersion;
        private int _revision;
        public Type Type { get; } = typeof(Version);

        public object Generate()
        {
            _majorVersion += 1000;
            _minorVersion += 333;
            _build += 777;
            _revision += 111;
            return new Version(_majorVersion, _minorVersion, _build, _revision);
        }
    }
}