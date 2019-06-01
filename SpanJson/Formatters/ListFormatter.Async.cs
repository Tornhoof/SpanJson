using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>
    {
        /// <summary>
        /// For this we assume that a rather large portion of the data can be written synchronously
        /// </summary>
        public ValueTask SerializeAsync(AsyncWriter<TSymbol> asyncWriter, TList value, CancellationToken cancellationToken = default)
        {
            var writer = asyncWriter.Create();
            if (value == null)
            {
                writer.WriteNull();
                return default;
            }

            if (IsRecursionCandidate)
            {
                writer.IncrementDepth();
            }

            var valueLength = value.Count;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0], ElementFormatter);
                var task = writer.FlushAsync(cancellationToken);
                if (!task.IsCompletedSuccessfully)
                {
                    return AwaitFlushAndContinue(task, asyncWriter, value, 1, cancellationToken);
                }
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                    task = writer.FlushAsync(cancellationToken);
                    if (!task.IsCompletedSuccessfully)
                    {
                        return AwaitFlushAndContinue(task, asyncWriter, value, i + 1, cancellationToken);
                    }
                }
            }

            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }

            writer.WriteEndArray();
            return default;
        }

        /// <summary>
        /// This is necessary to prevent a stackoverflow from too many nested calls
        /// </summary>
        private async ValueTask AwaitFlushAndContinue(ValueTask task, AsyncWriter<TSymbol> asyncWriter, TList value, int index, CancellationToken cancellationToken = default)
        {
            await task.ConfigureAwait(false);
            var valueLength = value.Count;
            for (; index < valueLength; index++)
            {
                index = await SerializeNextAsync(asyncWriter, value, index, cancellationToken).ConfigureAwait(false);
            }
        }

        private async ValueTask<int> AwaitNextFlushAndContinue(ValueTask task, int index)
        {
            await task.ConfigureAwait(false);
            return index;
        }

        private ValueTask<int> SerializeNextAsync(AsyncWriter<TSymbol> asyncWriter, TList value, int index, CancellationToken cancellationToken = default)
        {
            var valueLength = value.Count;
            var writer = asyncWriter.Create();
            for (var i = index; i < valueLength; i++)
            {
                writer.WriteValueSeparator();
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                var task = writer.FlushAsync(cancellationToken);
                if (!task.IsCompletedSuccessfully)
                {
                    return AwaitNextFlushAndContinue(task, i);
                }
            }

            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }

            writer.WriteEndArray();
            return new ValueTask<int>(valueLength);
        }
    }
}
