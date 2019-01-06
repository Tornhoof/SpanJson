using System;

namespace SpanJson.Shared.Fixture
{
    public class EnumValueFixture : IValueFixture
    {
        private readonly Random _prng;
        private readonly string[] _values;

        public EnumValueFixture(Type type, int? seed = null)
        {
            Type = type;
            _values = Enum.GetNames(Type);
            _prng = seed != null ? new Random(seed.Value) : new Random();
        }

        public Type Type { get; }

        public object Generate()
        {
            return Enum.Parse(Type, _values[_prng.Next(_values.Length)]);
        }
    }
}