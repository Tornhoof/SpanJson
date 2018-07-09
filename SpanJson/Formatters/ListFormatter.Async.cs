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
        public ValueTask SerializeAsync(AsyncWriter<TSymbol> asyncWriter, TList value, int nestingLimit, CancellationToken cancellationToken = default)
        {
            var maxSafeWriteSize = asyncWriter.MaxSafeWriteSize;
            var writer = asyncWriter.Create();
            if (value == null)
            {
                writer.WriteNull();
                return new ValueTask(asyncWriter.FlushAsync(writer.Position, cancellationToken));
            }

            var nextNestingLimit = RecursionCandidate<T>.IsRecursionCandidate ? nestingLimit + 1 : nestingLimit;
            var valueLength = value.Count;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                // For now assume that this doesn't overflow the buffer, the same sync/async approach will apply here
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0], ElementFormatter, nextNestingLimit);
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter, nextNestingLimit);

                    if (writer.Position > maxSafeWriteSize)
                    {
                        var task = asyncWriter.FlushAsync(writer.Position, cancellationToken);
                        if (task.IsCompleted) // if it is sync we reset the writer position and continue synchronously with the next value
                        {
                            writer.Reset();
                        }
                        else
                        {
                            // It's async, we need to call the async wrapper method and continue from there
                            // this stackoverflows for larget sizes
                            return SerializeArrayElementsAsync(asyncWriter, task, value, i + 1, nextNestingLimit, cancellationToken);
                        }
                    }
                }
            }

            writer.WriteEndArray();
            return new ValueTask(asyncWriter.FlushAsync(writer.Position, cancellationToken));
        }

        /// <summary>
        /// awaits the async task and continues writing the remaining list elements
        /// </summary>
        private async ValueTask SerializeArrayElementsAsync(AsyncWriter<TSymbol> asyncWriter, Task task, TList value, int nextIndex, int nestingLimit,
            CancellationToken cancellationToken = default)
        {
            await task.ConfigureAwait(false);
            await SerializeArrayElementsAsync(asyncWriter, value, nextIndex, nestingLimit, cancellationToken).ConfigureAwait(false); // call the 
        }
        
        /// <summary>
        /// Writes the remaining list elements
        /// </summary>
        private ValueTask SerializeArrayElementsAsync(AsyncWriter<TSymbol> asyncWriter, TList value, int nextIndex, int nestingLimit,
            CancellationToken cancellationToken = default)
        {
            var maxSafeWriteSize = asyncWriter.MaxSafeWriteSize;
            var writer = asyncWriter.Create();
            var valueLength = value.Count;
            for (var i = nextIndex; i < valueLength; i++)
            {
                writer.WriteValueSeparator();
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter, nestingLimit);

                if (writer.Position > maxSafeWriteSize)
                {
                    var task = asyncWriter.FlushAsync(writer.Position, cancellationToken);
                    if (task.IsCompleted) // if it is sync we reset the writer position and continue synchronously with the next value
                    {
                        writer.Reset();
                    }
                    else
                    {
                        // It's async, we need to call the async wrapper method and continue from there
                        // this stackoverflows for large sizes
                        return SerializeArrayElementsAsync(asyncWriter, task, value, i + 1, nestingLimit, cancellationToken);
                    }
                }
            }

            writer.WriteEndArray();
            return new ValueTask(asyncWriter.FlushAsync(writer.Position, cancellationToken));
        }

        // TODO
        public ValueTask<TList> DeserializeAsync(AsyncReader<TSymbol> asyncReader, CancellationToken cancellationToken = default)
        {
            return new ValueTask<TList>((TList) default);
        }
    }
}
