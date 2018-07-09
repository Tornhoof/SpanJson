using SpanJson.Helpers;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol>
    {
        /// <summary>
        /// Writes the list elements
        /// Idea:
        /// - Write everything synchronously until our buffer is full (enough)
        /// - Flush it and either continue synchronously, if the flush is completed already, or asynchronously with the remaining elements
        /// </summary>
        public async ValueTask SerializeAsync(AsyncWriter<TSymbol> asyncWriter, TList value, int nestingLimit, CancellationToken cancellationToken = default)
        {
            if (value == null)
            {
                asyncWriter.WriteNull();
                await asyncWriter.FlushAsync(cancellationToken);
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var valueLength = value.Count;
            asyncWriter.WriteBeginArray();
            if (valueLength > 0)
            {
                asyncWriter.Position = WriteElementSync(asyncWriter, value[0], nextNestingLimit);
                for (var i = 1; i < valueLength; i++)
                {
                    asyncWriter.WriteValueSeparator();
                    asyncWriter.Position = WriteElementSync(asyncWriter, value[i], nextNestingLimit);
                    if (asyncWriter.Position > asyncWriter.MaxSafeWriteSize)
                    {
                        await asyncWriter.FlushAsync(cancellationToken);
                    }
                }

                asyncWriter.WriteEndArray();
            }

            await asyncWriter.FlushAsync(cancellationToken);
        }

        private int WriteElementSync(AsyncWriter<TSymbol> asyncWriter, T value, int nextNestingLimit)
        {
            var writer = asyncWriter.Create();
            SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value, ElementFormatter, nextNestingLimit);
            return writer.Position;
        }

        // TODO
        public ValueTask<TList> DeserializeAsync(AsyncReader<TSymbol> asyncReader, CancellationToken cancellationToken = default)
        {
            return new ValueTask<TList>((TList) default);
        }
    }
}
