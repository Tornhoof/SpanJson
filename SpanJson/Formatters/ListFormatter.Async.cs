using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public sealed partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>
    {
        public ValueTask SerializeAsync(AsyncJsonWriter<TSymbol> asyncWriter, TList value, CancellationToken cancellationToken = default)
        {
            // let it run sync until the serialized size hits ~32kb
            // if a single entity is
            // flushasync
            // new jsonwriter, start from pos 0

            var writer = asyncWriter.Create();
            if (value == null)
            {
                writer.WriteNull();
                return asyncWriter.FlushAsync(cancellationToken);
            }

            if (IsRecursionCandidate)
            {
                writer.IncrementDepth();
            }

            return default;
        }

        public ValueTask<TList> DeserializeAsync(AsyncJsonReader<TSymbol> asyncReader, CancellationToken cancellationToken = default)
        {
            // only problem is string, we need to find the end of the string and buffer until we hit it
            return default;
        }
    }
}


