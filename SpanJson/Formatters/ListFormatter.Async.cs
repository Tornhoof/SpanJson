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

            var valueLength = value.Count;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                var vTask = WriteElementAsync(asyncWriter, ref writer, value[0], cancellationToken);
                if (!vTask.IsCompletedSuccessfully)
                {
                    return AwaitTask(vTask, asyncWriter, value, 1, cancellationToken);
                }

                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    vTask = WriteElementAsync(asyncWriter, ref writer, value[i], cancellationToken);
                    if (!vTask.IsCompletedSuccessfully)
                    {
                        return AwaitTask(vTask, asyncWriter, value, i + 1, cancellationToken);
                    }
                }
            }

            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }

            writer.WriteEndArray();
            return asyncWriter.FlushAsync(cancellationToken);
        }

        private static async ValueTask AwaitTask(ValueTask vTask, AsyncJsonWriter<TSymbol> asyncWriter, TList value, int index,
            CancellationToken cancellationToken = default)
        {
            await vTask.ConfigureAwait(false);
            await WriteElementsAsync(asyncWriter, value, index + 1, cancellationToken).ConfigureAwait(false);
        }

        private static ValueTask WriteElementAsync(AsyncJsonWriter<TSymbol> asyncWriter, ref JsonWriter<TSymbol> writer, T value,
            CancellationToken cancellationToken = default)
        {
            SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value, ElementFormatter);
            if (writer.Position > asyncWriter.MaxSafeWriteSize)
            {
                return asyncWriter.FlushAsync(cancellationToken);
            }

            return new ValueTask();
        }

        private static ValueTask WriteElementsAsync(AsyncJsonWriter<TSymbol> asyncWriter, TList value, int index,
            CancellationToken cancellationToken = default)
        {
            var writer = asyncWriter.Create();
            var valueLength = value.Count;
            for (var i = index; i < valueLength; i++)
            {
                writer.WriteValueSeparator();
                var vTask = WriteElementAsync(asyncWriter, ref writer, value[i], cancellationToken);
                if (!vTask.IsCompletedSuccessfully)
                {
                    return AwaitTask(vTask, asyncWriter, value, i + 1, cancellationToken);
                }
            }

            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }

            writer.WriteEndArray();
            return asyncWriter.FlushAsync(cancellationToken);
        }

        public ValueTask<TList> DeserializeAsync(AsyncJsonReader<TSymbol> asyncReader, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}


