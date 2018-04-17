using System;

namespace SpanJson
{
    public class JsonParserException : Exception
    {
        public enum ParserError
        {
            EndOfData,
            InvalidNumberFormat,
            InvalidSymbol,
            ExpectedDoubleQuote,
            ExpectedBeginArray,
            ExpectedEndArray,
            ExpectedBeginObject,
            ExpectedEndObject,
            ExpectedSeparator,
            NestingTooDeep,
        }

        public JsonParserException(ParserError error, int position) : base($"Error reading JSON data: '{error}' at position: '{position}'.")
        {
            Error = error;
            Position = position;
        }

        public JsonParserException(ParserError error, Type type, int position) : base(
            $"Error reading JSON data for type '{type.Name}': '{error}' at position: '{position}'.")
        {
            Type = type;
        }

        public Type Type { get; }
        public ParserError Error { get; }
        public int Position { get; }
    }
}