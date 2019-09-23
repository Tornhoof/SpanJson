using System;

namespace SpanJson
{
    public static partial class JsonSerializer
    {
        /// <summary>
        ///     Very primitive JSON pretty printer
        /// </summary>
        public static class PrettyPrinter
        {
            /// <summary>
            ///     Pretty prints a json input with 2 space indentation.
            /// </summary>
            /// <param name="input">Input</param>
            /// <returns>String</returns>
            public static string Print(in ReadOnlySpan<char> input)
            {
                var reader = new JsonReader<char>(input);
                var writer = new JsonWriter<char>(input.Length);
                try
                {
                    Print(ref reader, ref writer, 0);
                    return writer.ToString();
                }
                finally
                {
                    writer.Dispose();
                }
            }

            /// <summary>
            ///     Pretty prints a json input with 2 space indentation.
            /// </summary>
            /// <param name="input">Input</param>
            /// <returns>Byte array</returns>
            public static byte[] Print(in ReadOnlySpan<byte> input)
            {
                var reader = new JsonReader<byte>(input);
                var writer = new JsonWriter<byte>(input.Length);
                try
                {
                    Print(ref reader, ref writer, 0);
                    return writer.ToByteArray();
                }
                finally
                {
                    writer.Dispose();
                }
            }

            private static void Print<TSymbol>(ref JsonReader<TSymbol> reader, ref JsonWriter<TSymbol> writer, int indent) where TSymbol : struct
            {
                var token = reader.ReadNextToken();
                switch (token)
                {
                    case JsonToken.BeginObject:
                    {
                        reader.ReadBeginObjectOrThrow();
                        writer.WriteBeginObject();
                        writer.WriteNewLine();
                        var c = 0;
                        while (!reader.TryReadIsEndObjectOrValueSeparator(ref c))
                        {
                            if (c != 1)
                            {
                                writer.WriteValueSeparator();
                                writer.WriteNewLine();
                            }

                            writer.WriteIndentation(indent + 2);
                            writer.WriteVerbatimNameSpan(reader.ReadVerbatimNameSpan());
                            writer.WriteIndentation(1);
                            Print(ref reader, ref writer, indent + 2);
                        }

                        writer.WriteNewLine();
                        writer.WriteIndentation(indent);
                        writer.WriteEndObject();
                        break;
                    }
                    case JsonToken.BeginArray:
                    {
                        reader.ReadBeginArrayOrThrow();
                        writer.WriteBeginArray();
                        writer.WriteNewLine();
                        var c = 0;
                        while (!reader.TryReadIsEndArrayOrValueSeparator(ref c))
                        {
                            if (c != 1)
                            {
                                writer.WriteValueSeparator();
                                writer.WriteNewLine();
                            }

                            writer.WriteIndentation(indent + 2);
                            Print(ref reader, ref writer, indent + 2);
                        }

                        writer.WriteNewLine();
                        writer.WriteIndentation(indent);
                        writer.WriteEndArray();
                        break;
                    }
                    case JsonToken.Number:
                    {
                        var span = reader.ReadNumberSpan();
                        writer.WriteVerbatim(span);
                        break;
                    }
                    case JsonToken.String:
                    {
                        var span = reader.ReadVerbatimStringSpan();
                        writer.WriteDoubleQuote();
                        writer.WriteVerbatim(span);
                        writer.WriteDoubleQuote();
                        break;
                    }
                    case JsonToken.True:
                    case JsonToken.False:
                    {
                        var value = reader.ReadBoolean();
                        writer.WriteBoolean(value);
                        break;
                    }
                    case JsonToken.Null:
                    {
                        reader.ReadIsNull();
                        writer.WriteNull();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Minifies JSON
        /// </summary>
        public static class Minifier
        {
            /// <summary>
            ///     Minifies the input
            /// </summary>
            /// <param name="input">Input</param>
            /// <returns>String</returns>
            public static string Minify(in ReadOnlySpan<char> input)
            {
                var reader = new JsonReader<char>(input);
                var writer = new JsonWriter<char>(input.Length); // less but shouldn't matter here
                try
                {
                    Minify(ref reader, ref writer);
                    return writer.ToString();
                }
                finally
                {
                    writer.Dispose();
                }
            }

            /// <summary>
            ///     Minifies the input
            /// </summary>
            /// <param name="input">Input</param>
            /// <returns>Byte array</returns>
            public static byte[] Minify(in ReadOnlySpan<byte> input)
            {
                var reader = new JsonReader<byte>(input);
                var writer = new JsonWriter<byte>();
                try
                {
                    Minify(ref reader, ref writer);
                    return writer.ToByteArray();
                }
                finally
                {
                    writer.Dispose();
                }
            }

            private static void Minify<TSymbol>(ref JsonReader<TSymbol> reader, ref JsonWriter<TSymbol> writer) where TSymbol : struct
            {
                var token = reader.ReadNextToken();
                switch (token)
                {
                    case JsonToken.BeginObject:
                        {
                            reader.ReadBeginObjectOrThrow();
                            writer.WriteBeginObject();
                            var c = 0;
                            while (!reader.TryReadIsEndObjectOrValueSeparator(ref c))
                            {
                                if (c != 1)
                                {
                                    writer.WriteValueSeparator();
                                }

                                writer.WriteVerbatimNameSpan(reader.ReadVerbatimNameSpan());

                                Minify(ref reader, ref writer);
                            }
                            writer.WriteEndObject();
                            break;
                        }
                    case JsonToken.BeginArray:
                        {
                            reader.ReadBeginArrayOrThrow();
                            writer.WriteBeginArray();
                            var c = 0;
                            while (!reader.TryReadIsEndArrayOrValueSeparator(ref c))
                            {
                                if (c != 1)
                                {
                                    writer.WriteValueSeparator();
                                }

                                Minify(ref reader, ref writer);
                            }

                            writer.WriteEndArray();
                            break;
                        }
                    case JsonToken.Number:
                        {
                            var span = reader.ReadNumberSpan();
                            writer.WriteVerbatim(span);
                            break;
                        }
                    case JsonToken.String:
                        {
                            var span = reader.ReadVerbatimStringSpan();
                            writer.WriteDoubleQuote();
                            writer.WriteVerbatim(span);
                            writer.WriteDoubleQuote();
                            break;
                        }
                    case JsonToken.True:
                    case JsonToken.False:
                        {
                            var value = reader.ReadBoolean();
                            writer.WriteBoolean(value);
                            break;
                        }
                    case JsonToken.Null:
                        {
                            reader.ReadIsNull();
                            writer.WriteNull();
                            break;
                        }
                }
            }
        }
    }
}