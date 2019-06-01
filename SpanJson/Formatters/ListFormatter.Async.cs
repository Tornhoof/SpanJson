using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public partial class ListFormatter<TList, T, TSymbol, TResolver> : IAsyncJsonFormatter<TList, TSymbol>
    {
        public ValueTask SerializeAsync(ref JsonWriter<TSymbol> writer, ref AwaiterState state, TList value,
            CancellationToken cancellationToken = default)
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

            var valueLength = value.Count;
            writer.WriteBeginArray();
            if (valueLength > 0)
            {
                SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[0], ElementFormatter);
                var awaiter = writer.FlushAsync().GetAwaiter();
                if (!awaiter.IsCompleted)
                {
                    return BuildStateMachine(1, value, writer.Data);
                }

                for (var i = 1; i < valueLength; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                    awaiter = writer.FlushAsync().GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        return BuildStateMachine(i, value, writer.Data);
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

        private static ValueTask BuildStateMachine(int index, TList value, TSymbol[] data)
        {
            ListFormatterStateMachine stateMachine = default;
            stateMachine.Index = index;
            stateMachine.Value = value;
            stateMachine.Data = data;
            stateMachine.Builder = AsyncValueTaskMethodBuilder.Create();
            stateMachine.Builder.Start(ref stateMachine);
            return stateMachine.Builder.Task;
        }

        private struct ListFormatterStateMachine : IAsyncStateMachine
        {
            public AsyncValueTaskMethodBuilder Builder;
            public TaskAwaiter Awaiter;
            public bool NeedsAwaiting;
            public int Index;
            public TList Value;
            public TSymbol[] Data;

            public void MoveNext()
            {
                if(NeedsAwaiting)
                {
                    Awaiter.GetResult();
                    Awaiter = default;
                    NeedsAwaiting = false;
                }
                var writer = new JsonWriter<TSymbol>(Data);
                var value = Value;
                var index = Index;
                for (var i = index; i < Value.Count; i++)
                {
                    writer.WriteValueSeparator();
                    SerializeRuntimeDecisionInternal<T, TSymbol, TResolver>(ref writer, value[i], ElementFormatter);
                    var awaiter = writer.FlushAsync().GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        Index = i;
                        Awaiter = awaiter;
                        NeedsAwaiting = true;
                        Builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                        return;
                    }
                }

                if (IsRecursionCandidate)
                {
                    writer.DecrementDepth();
                }

                writer.WriteEndArray();
                Builder.SetResult();
            }

            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                Builder.SetStateMachine(stateMachine);
            }
        }


        public ValueTask<TList> DeserializeAsync(ref JsonReader<TSymbol> reader, ref AwaiterState state, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
