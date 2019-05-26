using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol>
    {
        public ValueTask SerializeAsync(ref JsonWriter<TSymbol> writer, ref AwaiterState state, TList value, CancellationToken cancellationToken = default)
        {
            if (state.State == -1)
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return default;
                }

                if (IsRecursionCandidate)
                {
                    writer.IncrementDepth();
                }

                writer.WriteBeginArray();
                state.State = 0;
            }

            var valueLength = value.Count;

            if (valueLength > 0)
            {
                if (state.State == 0)
                {
                    var vTask = SerializeRuntimeDecisionInternalAsync(ref writer, ref state, value[0], 0)
                        .ConfigureAwait(false);
                    var awaiter = vTask.GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        return BuildStateMachine(ref writer, ref state, value, cancellationToken);
                    }

                    state.State = 1;
                }

                ref var i = ref state.State;
                for (; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    var vTask = SerializeRuntimeDecisionInternalAsync(ref writer, ref state, value[i], i)
                        .ConfigureAwait(false);
                    var awaiter = vTask.GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        return BuildStateMachine(ref writer, ref state, value, cancellationToken);
                    }
                }
            }

            if (IsRecursionCandidate)
            {
                writer.DecrementDepth();
            }

            writer.WriteEndArray();
            state.State = -1;
            return default;
        }

        private ValueTask BuildStateMachine(ref JsonWriter<TSymbol> writer, ref AwaiterState state, TList value, CancellationToken cancellationToken = default)
        {
            writer.Dispose();
            ListFormatterStateMachine stateMachine = default;
            stateMachine.State = state;
            stateMachine.Value = value;
            stateMachine.CancellationToken = cancellationToken;
            stateMachine.Builder = AsyncValueTaskMethodBuilder.Create();
            stateMachine.Builder.Start(ref stateMachine);
            return stateMachine.Builder.Task;
        }

        public ValueTask<TList> DeserializeAsync(ref JsonReader<TSymbol> reader, ref AwaiterState state, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ValueTask SerializeRuntimeDecisionInternalAsync(ref JsonWriter<TSymbol> writer, ref AwaiterState state, T value, int counter)
        {
            if (state.Awaiter != null) // this is flush
            {
                ((ValueTaskAwaiter)state.Awaiter).GetResult();
                state.Awaiter = null;
            }

            if (counter % 1000 == 0)
            {
                return new ValueTask(Yield());
            }


            SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value, ElementFormatter);
            return default;
        }

        private async Task Yield()
        {
            await Task.Yield();
        }

        private struct ListFormatterStateMachine : IAsyncStateMachine
        {
            public AsyncValueTaskMethodBuilder Builder;
            public AwaiterState State;
            public CancellationToken CancellationToken;
            public TList Value;

            public void MoveNext()
            {
                var writer = new JsonWriter<TSymbol>(5000);
                var state = State;
                var cancellationToken = CancellationToken;
                var value = Value;
                Default.SerializeAsync(ref writer, ref state, value, cancellationToken);
            }

            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                Builder.SetStateMachine(stateMachine);
            }
        }
    }
}
