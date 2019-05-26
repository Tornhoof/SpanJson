using System.Threading;
using System.Threading.Tasks;

namespace SpanJson
{
    public interface IJsonFormatter
    {
    }

    public interface ICustomJsonFormatter
    {
        object Arguments { get; set; }
    }

    public interface ICustomJsonFormatter<T> : IJsonFormatter<T, byte>, IJsonFormatter<T, char>, ICustomJsonFormatter
    {
    }

    public interface IJsonFormatter<T, TSymbol> : IJsonFormatter where TSymbol : struct
    {
        void Serialize(ref JsonWriter<TSymbol> writer, T value);
        T Deserialize(ref JsonReader<TSymbol> reader);
    }

    public interface IAsyncJsonFormatter<T, TSymbol> : IJsonFormatter<T, TSymbol> where TSymbol : struct
    {
        ValueTask SerializeAsync(ref JsonWriter<TSymbol> writer, ref AwaiterState state,  T value, CancellationToken cancellationToken = default);
        ValueTask<T> DeserializeAsync(ref JsonReader<TSymbol> reader, ref AwaiterState state, CancellationToken cancellationToken = default);
    }
}