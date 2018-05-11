using System;

namespace SpanJson
{
    public static partial class JsonSerializer
    {
        /// <summary>
        /// Very primitive JSON pretty printer
        /// </summary>
        public static class PrettyPrinter
        {
            /// <summary>
            /// Pretty prints a json input with 2 space indentation.
            /// </summary>
            /// <param name="input">Input</param>
            /// <returns>String</returns>
            public static string Print(ReadOnlySpan<char> input)
            {
                var reader = new JsonReader<char>(input);
                var writer = new JsonWriter<char>();
                Print(ref reader, ref writer, 0);
                return writer.ToString();
            }
            /// <summary>
            /// Pretty prints a json input with 2 space indentation.
            /// </summary>
            /// <param name="input">Input</param>
            /// <returns>Byte array</returns>
            public static byte[] Print(ReadOnlySpan<byte> input)
            {
                var reader = new JsonReader<byte>(input);
                var writer = new JsonWriter<byte>();
                Print(ref reader, ref writer, 0);
                return writer.ToByteArray();
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
                            writer.WriteNameSpan(reader.ReadNameSpan());
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
                        var span = reader.ReadStringSpan();
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
