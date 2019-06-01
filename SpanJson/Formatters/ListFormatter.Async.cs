using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>
    {
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
                var flush1 = writer.FlushAsync(cancellationToken);
                if (!flush1.IsCompletedSuccessfully)
                {
                    return AwaitFlushAndContinue(flush1, asyncWriter, value, 1, cancellationToken);
                }
                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                    flush1 = writer.FlushAsync(cancellationToken);
                    if (!flush1.IsCompletedSuccessfully)
                    {
                        return AwaitFlushAndContinue(flush1, asyncWriter, value, i+1, cancellationToken);
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

        private async ValueTask AwaitFlushAndContinue(Task task, AsyncWriter<TSymbol> asyncWriter, TList value, int index, CancellationToken cancellationToken = default)
        {
            await task.ConfigureAwait(false);
            var valueLength = value.Count;
            for (; index < valueLength; index++)
            {
                index = await SerializeNextAsync(asyncWriter, value, index, cancellationToken).ConfigureAwait(false);
            }
        }

        private async ValueTask<int> AwaitNextFlushAndContinue(Task task, int index)
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
                var flush1 = writer.FlushAsync(cancellationToken);
                if (!flush1.IsCompletedSuccessfully)
                {
                    return AwaitNextFlushAndContinue(flush1, i + 1);
                }
            }

            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }

            writer.WriteEndArray();
            return default;
        }
    }
}
