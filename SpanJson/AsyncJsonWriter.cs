using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson
{
    public sealed class AsyncJsonWriter<TSymbol> : IDisposable where TSymbol : struct
    {
        private readonly PipeWriter _pipeWriter;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AsyncJsonWriter(PipeWriter pipeWriter)
        {
            _pipeWriter = pipeWriter;
        }

        public JsonWriter<TSymbol> Create()
        {
            var data = _pipeWriter.GetMemory(4000);
            var memory = Unsafe.As<Memory<byte>, Memory<TSymbol>>(ref data);
            return new JsonWriter<TSymbol>(memory.Span, 0, 0);
        }

        public void Dispose()
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueTask<bool> FlushAsync(int length, CancellationToken cancellationToken = default)
        {
            if (length > MaxSafeWriteSize) // we only flush if we have written enough data
            {
                _pipeWriter.Advance(length);
                var flushResult =_pipeWriter.FlushAsync(cancellationToken);
                if (flushResult.IsCompletedSuccessfully)
                {
                    if (flushResult.Result.IsCanceled || flushResult.Result.IsCompleted)
                    {                     
                    }
                    return new ValueTask<bool>(true);
                }

                return AwaitFlushAsync(flushResult);
            }

            return new ValueTask<bool>(false);
        }

        private async ValueTask<bool> AwaitFlushAsync(ValueTask<FlushResult> flushResult)
        {
            var result = await flushResult.ConfigureAwait(false);
            if (result.IsCanceled || flushResult.IsCompleted)
            {               
            }

            return true;
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        private const int MaxSafeWriteSize = 4000 - 128;
    }

    public class AsyncJsonReader<TSymbol> where TSymbol : struct
    {

    }
}
