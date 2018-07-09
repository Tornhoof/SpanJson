using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson
{
    public interface IJsonFormatter
    {
    }

    public interface ICustomJsonFormatter
    {

    }
    public interface ICustomJsonFormatter<T> : IJsonFormatter<T, byte>, IJsonFormatter<T, char>, ICustomJsonFormatter
    {

    }

    public interface IJsonFormatter<T, TSymbol> : IJsonFormatter where TSymbol : struct
    {
        void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit);
        T Deserialize(ref JsonReader<TSymbol> reader);
    }

    public interface IAsyncJsonFormatter<T, TSymbol> : IJsonFormatter where TSymbol : struct
    {
        ValueTask SerializeAsync(AsyncWriter<TSymbol> asyncWriter, T value, int nestingLimit, CancellationToken cancellationToken = default);
        ValueTask<T> DeserializeAsync(AsyncReader<TSymbol> asyncReader, CancellationToken cancellationToken = default);
    }
}