using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanJson
{
    public struct AwaiterState
    {
        public AwaiterState(int state)
        {
            State = state;
        }

        public int State;
    }
}
