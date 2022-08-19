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
            InvalidArrayFormat,
            InvalidEncoding,
            ExpectedDoubleQuote,
            ExpectedBeginArray,
            ExpectedEndArray,
            ExpectedBeginObject,
            ExpectedEndObject,
            ExpectedSeparator,
            NestingTooDeep,
        }

        public enum ValueType
        {
            Bool,
            DateTime,
            DateTimeOffset,
            TimeSpan,
            Guid,
            Array,
        }

        public JsonParserException(ParserError error, int position) : base($"Error Reading JSON data: '{error}' at position: '{position}'.")
        {
            Error = error;
            Position = position;
        }

        public JsonParserException(ParserError error, ValueType type, int position) : base(
            $"Error Reading JSON data for type '{type}': '{error}' at position: '{position}'.")
        {
            Type = type;
        }

        public ValueType Type { get; }
        public ParserError Error { get; }
        public int Position { get; }
    }
}