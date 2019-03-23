using System;
using System.Text;

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
        }

        public JsonParserException(ParserError error, int position) : base($"Error Reading JSON data: '{error}' at position: '{position}'.")
        {
            Error = error;
            Position = position;
        }

        public JsonParserException(ParserError error, in ReadOnlySpan<byte> value) : base(
            $"Error Reading JSON data: '{error}' at value: '{Encoding.UTF8.GetString(value)}'.")
        {
            Error = error;
        }

        public JsonParserException(ParserError error, in ReadOnlySpan<char> value) : base($"Error Reading JSON data: '{error}' at value: '{value.ToString()}'.")
        {
            Error = error;
        }

        public JsonParserException(ParserError error, ValueType type, int position) : base(
            $"Error Reading JSON data for type '{type}': '{error}' at position: '{position}'.")
        {
            Type = type;
        }


        public JsonParserException(ParserError error, ValueType type, in ReadOnlySpan<byte> value) : base(
            $"Error Reading JSON data for type '{type}': '{error}' at value: '{Encoding.UTF8.GetString(value)}'.")
        {
            Type = type;
        }

        public JsonParserException(ParserError error, ValueType type, in ReadOnlySpan<char> value) : base(
            $"Error Reading JSON data for type '{type}': '{error}' at value: '{value.ToString()}'.")
        {
            Type = type;
        }

        public ValueType Type { get; }
        public ParserError Error { get; }
        public int Position { get; }
    }
}