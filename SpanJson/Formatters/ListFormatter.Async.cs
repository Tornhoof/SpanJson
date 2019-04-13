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

            var valueLength = value.Count;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                var vTask = WriteElement(asyncWriter, ref writer, value[0], cancellationToken);
                if (!vTask.IsCompletedSuccessfully)
                {
                    return new ValueTask(vTask.AsTask().ContinueWith((Task t, object o) => WriteElements(asyncWriter, value, 1, cancellationToken).AsTask(),
                        cancellationToken, cancellationToken));
                }

                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                    if (writer.Position >= asyncWriter.MaxSafeWriteSize)
                    {
                        var i1 = i;
                        var flushTask = asyncWriter.FlushAsync(cancellationToken).AsTask().ContinueWith(
                            (Task t, object o) => WriteElements(asyncWriter, value, i1 + 1, cancellationToken).AsTask(), cancellationToken, cancellationToken);
                        return new ValueTask(flushTask);
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

        private static ValueTask WriteElement(AsyncJsonWriter<TSymbol> asyncWriter,  ref JsonWriter<TSymbol> writer, T value, CancellationToken cancellationToken = default)
        {
            SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value, ElementFormatter);
            if (writer.Position >= asyncWriter.MaxSafeWriteSize)
            {
                return asyncWriter.FlushAsync(cancellationToken);
            }

            return new ValueTask();
        }

        private static ValueTask WriteElements(AsyncJsonWriter<TSymbol> asyncWriter, TList value, int index, CancellationToken cancellationToken = default)
        {
            var writer = asyncWriter.Create();
            var valueLength = value.Count;
            for (var i = index; i < valueLength; i++)
            {
                writer.WriteValueSeparator();
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                if (writer.Position >= asyncWriter.MaxSafeWriteSize)
                {
                    var i1 = i;
                    var flushTask = asyncWriter.FlushAsync(cancellationToken).AsTask().ContinueWith((Task t, object  o) => WriteElements(asyncWriter, value, i1 + 1, cancellationToken).AsTask(), cancellationToken, cancellationToken);
                    return new ValueTask(flushTask);
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
            // only problem is string, we need to find the end of the string and buffer until we hit it
            return default;
        }
    }
}


