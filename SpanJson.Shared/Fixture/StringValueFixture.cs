using System;

namespace SpanJson.Shared.Fixture
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
            var chars = new char[length];
            for (var i = 0; i < chars.Length; i++)
            {
                int index = _prng.Next(symbols.Length);
                var c = symbols[index];
                if (char.IsHighSurrogate(c))
                {
                    if (i == chars.Length - 1)
                    {
                        i--; // try again, we need two chars for that
                        continue;
                    }

                    chars[i++] = c;
                    chars[i] = symbols[index + 1];
                }
                else if (char.IsLowSurrogate(c))
                {
                    if (i == chars.Length - 1)
                    {
                        i--; // try again, we need two chars for that
                        continue;
                    }

                    chars[i++] = symbols[index - 1];
                    chars[i] = c;
                }
                else
                {
                    chars[i] = c;
                }
            }

            return new string(chars);
        }
    }
}