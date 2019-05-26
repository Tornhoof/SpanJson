using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol>
    {
        public ValueTask SerializeAsync(ref JsonWriter<TSymbol> writer, ref AwaiterState state, TList value, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TList> DeserializeAsync(ref JsonReader<TSymbol> reader, ref AwaiterState state, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
