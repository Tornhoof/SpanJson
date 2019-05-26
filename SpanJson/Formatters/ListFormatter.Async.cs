using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol>
    {
        public ValueTask SerializeAsync(ref JsonWriter<TSymbol> writer, ref AwaiterState state, TList value, CancellationToken cancellationToken = default)
        {
            int start = state.State;
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
                var vTask = SerializeRuntimeDecisionInternalAsync(ref writer, ref state, value[0], ElementFormatter, cancellationToken).ConfigureAwait(false);

                for (var i = start; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                }
            }
            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }
            writer.WriteEndArray();
            return default;
        }

        public ValueTask<TList> DeserializeAsync(ref JsonReader<TSymbol> reader, ref AwaiterState state, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ValueTask SerializeRuntimeDecisionInternalAsync(ref JsonWriter<TSymbol> writer, ref AwaiterState state, T value, IJsonFormatter<T, TSymbol> formatter, CancellationToken cancellationToken = default)
        {
            if (state.Awaiter != null)
            {
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0], ElementFormatter);
            }
            return default;
        }
    }
}
