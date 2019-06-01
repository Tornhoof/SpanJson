using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpanJson.Formatters
{
    public sealed partial class ComplexClassFormatter<T, TSymbol, TResolver> : ComplexFormatter, IAsyncJsonFormatter<T, TSymbol>
        where T : class where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        private static readonly SerializeAsyncDelegate<T, TSymbol> AsyncSerializer = BuildSerializeAsyncDelegate<T, TSymbol, TResolver>();

        public ValueTask SerializeAsync(AsyncWriter<TSymbol> asyncWriter, T value, CancellationToken cancellationToken = default)
        {
            ComplexClassFormatterStateMachine ccfsm = default;
            ccfsm.CancellationToken = cancellationToken;
            ccfsm.AsyncWriter = asyncWriter;
            ccfsm.Value = value;
            ccfsm.Builder = AsyncValueTaskMethodBuilder.Create();
            ccfsm.Builder.Start(ref ccfsm);
            return ccfsm.Builder.Task;
        }

        private struct ComplexClassFormatterStateMachine : IAsyncStateMachine
        {
            public AsyncWriter<TSymbol> AsyncWriter;
            public T Value;
            public CancellationToken CancellationToken;
            public AsyncValueTaskMethodBuilder Builder;
            public ValueTaskAwaiter _awaiter;
            private bool _needsAwaiting;

            public void MoveNext()
            {
                var value = Value;
                var asyncWriter = AsyncWriter;
                var cancellationToken = CancellationToken;
                int state = -1;
                if (_needsAwaiting)
                {
                    state = _awaiter.GetResult();
                    _awaiter = default;
                    _needsAwaiting = false;
                }

                if (state == -1)
                {
                    if (value == null)
                    {
                        asyncWriter.WriteNull();
                        return;
                    }

                    asyncWriter.WriteBeginObject();
                }

                do
                {
                    var awaiter = AsyncSerializer(asyncWriter, ref state, value, cancellationToken).GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        _awaiter = awaiter;
                        _needsAwaiting = true;
                        Builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
                        return;
                    }

                    state = awaiter.GetResult();
                } while (state != -1);

                asyncWriter.WriteEndObject();
                Builder.SetResult();
            }

            public void SetStateMachine(IAsyncStateMachine stateMachine)
            {
                Builder.SetStateMachine(stateMachine);
            }
        }
    }
}
