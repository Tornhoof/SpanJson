using System;

namespace SpanJson
{
    public class JsonFormatException : Exception
    {
        public enum FormatError
        {
            EndOfData,
            InvalidNumberFormat,
            InvalidSymbol,
            ExpectedDoubleQuote,
            ExpectedBeginArray,
            ExpectedEndArray,
            ExpectedBeginObject,
            ExpectedEndObject,
            ExpectedSeparator
        }

        public JsonFormatException(FormatError error, int position) : base($"Error reading JSON data: '{error}' at position: '{position}'.")
        {
            Error = error;
            Position = position;
        }

        public JsonFormatException(FormatError error, Type type, int position) : base(
            $"Error reading JSON data for type '{type.Name}': '{error}' at position: '{position}'.")
        {
            Type = type;
        }

        public Type Type { get; }
        public FormatError Error { get; }
        public int Position { get; }
    }
}