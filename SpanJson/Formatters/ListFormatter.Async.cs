using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public sealed partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct where TList : class, IList<T>
    {
        public async ValueTask SerializeAsync(AsyncJsonWriter<TSymbol> asyncWriter, TList value, CancellationToken cancellationToken = default)
        {
            // let it run sync until the serialized size hits ~32kb
            // if a single entity is
            // flushasync
            // new jsonwriter, start from pos 0
            if (value == null)
            {
                await WriteNull(asyncWriter, cancellationToken).ConfigureAwait(false);
                return;
            }

            int index = 0;
            var valueLength = value.Count;
            do
            {

                var (task, nextIndex) = WriteElements(asyncWriter, value, index, cancellationToken);
                if (!task.IsCompletedSuccessfully)
                {
                    await task.ConfigureAwait(false);
                }

                index = nextIndex;
            } while (index < valueLength);

            // ReSharper disable VariableHidesOuterVariable
            static ValueTask WriteNull(AsyncJsonWriter<TSymbol> asyncWriter, CancellationToken cancellationToken = default)
            {
                var writer = asyncWriter.Create();
                writer.WriteNull();
                return asyncWriter.FlushAsync(writer.Position, cancellationToken);
            }
        }

        private static ValueTask WriteElement(AsyncJsonWriter<TSymbol> asyncWriter, ref JsonWriter<TSymbol> writer, T value,
            CancellationToken cancellationToken = default)
        {
            SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value, ElementFormatter);
            if (writer.Position > asyncWriter.MaxSafeWriteSize)
            {
                var task = asyncWriter.FlushAsync(writer.Position, cancellationToken);
                if (task.IsCompletedSuccessfully)
                {
                    writer.Reset();
                }

                return task;
            }
            return new ValueTask();
        }

        private static (ValueTask, int) WriteElements(AsyncJsonWriter<TSymbol> asyncWriter, TList value, int index, CancellationToken cancellationToken = default)
        {
            var writer = asyncWriter.Create();
            var valueLength = value.Count;
            if (index == 0)
            {
                writer.IncrementDepth(); // this is not correct, needs to be done in asyncwriter
                writer.WriteBeginArray();
            }

            for (var i = index; i < valueLength; i++)
            {
                writer.WriteValueSeparator();
                var task = WriteElement(asyncWriter, ref writer, value[i], cancellationToken);
                if (!task.IsCompletedSuccessfully)
                {
                    return (task, i + 1);
                }
            }

            writer.DecrementDepth();
            writer.WriteEndArray();
            return (asyncWriter.FlushAsync(writer.Position, cancellationToken), valueLength);
        }

        public ValueTask<TList> DeserializeAsync(AsyncJsonReader<TSymbol> asyncReader, CancellationToken cancellationToken = default)
        {
            // only problem is string, we need to find the end of the string and buffer until we hit it
            return default;
        }
    }
}


