using System;

namespace SpanJson.Benchmarks.Fixture
{
    public class StringValueFixture : IValueFixture
    {
        private readonly Random _prng = new Random();
        private const string AlphaNumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string MixedSymbols = AlphaNumeric + " \t칱칳칶칹칼캠츧";
        private const string Multiplane = MixedSymbols + "💩🎼😁😷";

        public Type Type { get; } = typeof(string);

        public object Generate()
        {
            return Generate(8, Multiplane);
        }

        internal string GenerateAlphaNumeric()
        {
            return Generate(8, AlphaNumeric);
        }

        private string Generate(int length, string symbols)
        {
            var result =  string.Create(length, symbols, (span, random) =>
            {
                for (var i = 0; i < span.Length; i++)
                {
                    int index = _prng.Next(symbols.Length);
                    var c = symbols[index];
                    if (char.IsHighSurrogate(c))
                    {
                        if (i == span.Length - 1)
                        {
                            i--; // try again, we need two chars for that
                            continue;
                        }

                        span[i++] = c;
                        span[i] = symbols[index + 1];
                    }
                    else if (char.IsLowSurrogate(c))
                    {
                        if (i == span.Length - 1)
                        {
                            i--; // try again, we need two chars for that
                            continue;
                        }

                        span[i++] = symbols[index - 1];
                        span[i] = c;
                    }
                    else
                    {
                        span[i] = c;
                    }
                }
            });
            return result;
        }
    }
}