using System;
using System.Collections.Generic;
using System.Text;

namespace SpanJson.Formatters
{
    public abstract class BaseEnumFormatter<T, TSymbol> : BaseFormatter where T : Enum where TSymbol : struct
    {

        protected delegate T DeserializeDelegate(ref JsonReader<TSymbol> reader);

        protected delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, T value);

        protected delegate T StreamingDeserializeDelegate(ref StreamingJsonReader<TSymbol> reader);

        protected delegate void StreamingSerializeDelegate(ref StreamingJsonWriter<TSymbol> writer, T value);
    }
}
