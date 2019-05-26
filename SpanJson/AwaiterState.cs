using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanJson
{
    public struct AwaiterState
    {
        public AwaiterState(int state, ICriticalNotifyCompletion awaiter)
        {
            State = state;
            Awaiter = awaiter;
        }

        public int State { get; }
        public ICriticalNotifyCompletion Awaiter { get; }
    }
}
